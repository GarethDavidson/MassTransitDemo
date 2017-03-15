"# MassTransitDemo" <br />
First follow these instructions to install RabbitMQ https://www.rabbitmq.com/install-windows.html <br />

You may experince an error where rabbit mq tries to install on your network drive. To resolve edit the file
rabbitmq-defaults.bat in the derictory of C:\Program Files\RabbitMQ Server\rabbitmq_server-3.6.6\sbin, and change this value to 
set RABBITMQ_BASE=C:\RabbitMQ. The service may require a reinstalling via cmd >> rabbitmq-service uninstall.... then rabbitmq-service install

![alt tag](https://cloud.githubusercontent.com/assets/19776368/23866499/f209b818-0810-11e7-888e-1fa776fc8827.png)

https://www.lucidchart.com/invitations/accept/1aecde98-c202-48b6-aa56-19046e277723

