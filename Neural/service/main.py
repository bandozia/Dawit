import pika
import os
from multiprocessing import Pool
from pika.adapters.blocking_connection import BlockingChannel
from train.trainworker import TrainWorker
import time
import json

channel: BlockingChannel = None
pool: Pool = None

def connect(host) -> BlockingChannel:
    credentials = pika.PlainCredentials('dawit', 'brokerpass')
    conn = pika.BlockingConnection(
        pika.ConnectionParameters(host=host, credentials=credentials))
    
    return conn.channel()


def start_train(ch, method, props, body):
    jobData = json.loads(body)
    job = TrainWorker(jobData, train_progress, train_complete, method.delivery_tag)
    pool.apply_async(job.train)


def train_progress(metrics):
    print(metrics)
    channel.basic_publish('', 'nn_train_progress', json.dumps(metrics))
    

def train_complete(jobResult, deliveryTag):
    print("treino completo")    
    channel.basic_ack(deliveryTag)    
    channel.basic_publish('', 'nn_train_complete', json.dumps(jobResult))
    

def run():
    channel.queue_declare(
        'nn_start_train', exclusive=False, durable=True)
    channel.queue_declare('nn_train_progress', exclusive=False)
    channel.queue_declare('nn_train_complete', exclusive=False, durable=True)

    channel.basic_consume(queue='nn_start_train',
                                on_message_callback=start_train)

    print("fila de trabalhos inicializada")
    channel.basic_qos(prefetch_count=1)
    channel.start_consuming()
    

if __name__ == "__main__":
    
    success = False
    for i in range(0, 5):
        try:
            print("iniciando conexao com o broker...")
            time.sleep(7)
            channel = connect("localhost" if 'LOCAL_TEST' in os.environ else 'broker')
            success = True
            print("sucesso")
        except:
            print("Falha na conexao[%d]. Tentando novamente em 5 segundos..." % (i+1))
            
    
    if success:
        mw = os.environ.get('MAX_WORKERS')        
        pool = Pool(2 if mw is None else int(mw))            
        run()
    else:
        print("Nao foi possivel conectar-se ao message broker.")

    