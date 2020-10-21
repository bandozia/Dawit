import pika

def callback(c,m,p,b):
    print("recebido: %r" % b)


def main():
    credentials = pika.PlainCredentials('dawit','brokerpass')
    conn = pika.BlockingConnection(pika.ConnectionParameters(host="broker", credentials=credentials))
    channel = conn.channel()
    channel.queue_declare('nn_start_train', exclusive=False)

    channel.basic_consume(queue='nn_start_train', on_message_callback=callback, auto_ack=True)
    
    print ("aguardando msgs")
    channel.start_consuming()

if __name__ == "__main__":
    main()
