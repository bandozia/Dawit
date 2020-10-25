import pika
import os
import time
import json
from pika.adapters.blocking_connection import BlockingChannel
from train.trainworker import TrainWorker
from concurrent.futures import ThreadPoolExecutor

channel: BlockingChannel = None
pool: ThreadPoolExecutor = None

def connect(host) -> BlockingChannel:
    credentials = pika.PlainCredentials('dawit', 'brokerpass')
    conn = pika.BlockingConnection(
        pika.ConnectionParameters(host=host, credentials=credentials))
    
    return conn.channel()


def start_train(ch, method, props, body):          
    jobData = json.loads(body)    
    job = TrainWorker(jobData, train_progress, train_complete, method.delivery_tag)
    pool.submit(job.train)
    

def train_progress(metric):  
    print(metric)      
    channel.basic_publish('', 'nn_train_progress', json.dumps(metric))
    

def train_complete(jobResult, deliveryTag):            
    channel.basic_ack(deliveryTag)    
    channel.basic_publish('', 'nn_train_complete', json.dumps(jobResult))        
    print("treino completo")    
    

def run():
    channel.queue_declare(
        'nn_start_train', exclusive=False, durable=True, auto_delete=False)
    channel.queue_declare('nn_train_progress', exclusive=False, auto_delete=True)
    channel.queue_declare('nn_train_complete', exclusive=False, durable=True, auto_delete=False)

    channel.basic_consume(queue='nn_start_train',
                                on_message_callback=start_train)

    print("fila de trabalhos inicializada")
    channel.basic_qos(prefetch_count=1)
    channel.start_consuming()
    

if __name__ == "__main__":
    
    print("iniciando conexao com o broker...")
    success = False
    attempts = 5
    c = 0
    
    while (success is not True and c < attempts):        
        try:
            channel = connect("localhost" if 'LOCAL_TEST' in os.environ else 'broker')
            success = True
            print("sucesso")
        except:
            print("Falha na conexao[%d]. Tentando novamente em 5 segundos..." % (c+1))
            c += 1
            time.sleep(5)
    
    if success:
        mw = os.environ.get('MAX_WORKERS')        
        pool = ThreadPoolExecutor(2 if mw is None else int(mw))           
        run()
    else:
        print("Nao foi possivel conectar-se ao message broker.")

    