import pika

def callback(c,m,p,b):
    print("recebido: %r" % b)
    c.basic_ack(m.delivery_tag)
    


def main():
    credentials = pika.PlainCredentials('dawit','brokerpass')
    conn = pika.BlockingConnection(pika.ConnectionParameters(host="broker", credentials=credentials))
    channel = conn.channel()
    channel.queue_declare('nn_start_train', exclusive=False, durable=True)

    channel.basic_consume(queue='nn_start_train', on_message_callback=callback)
    
    print ("aguardando msgs")
    channel.basic_qos(prefetch_count=1)
    channel.start_consuming()


if __name__ == "__main__":
    #TODO: pingar porta 5672 do broker antes de segui com entrypoint
    main()
