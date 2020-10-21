FROM tensorflow/tensorflow:2.3.1
COPY Neural/service /home/service
RUN pip3 install pika
WORKDIR /home/service
ENTRYPOINT [ "python3", "main.py" ]