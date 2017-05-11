"# MassTransitDemo" <br />
First follow these instructions to install RabbitMQ https://www.rabbitmq.com/install-windows.html <br />

You may experince an error where RabbitMQ tries to install on your network drive. To resolve edit the file
rabbitmq-defaults.bat in the derictory of C:\Program Files\RabbitMQ Server\rabbitmq_server-3.6.6\sbin, and change this value to:  <br />
set RABBITMQ_BASE=C:\RabbitMQ.<br /> The service may require reinstalling via cmd:  <br /> rabbitmq-service uninstall<br /> then<br /> rabbitmq-service install <br />

RESTART!!! RESTART COMPUTER!!!!!!!!
<br/>

<br />
Set startup projects as multiple and select TestPublisher, TestSubcriber and Consumer to Start
<br />

![alt tag](https://cloud.githubusercontent.com/assets/19776368/24191680/67f21b90-0ee4-11e7-8b46-b7e0afe13a9a.png)

https://www.lucidchart.com/invitations/accept/1aecde98-c202-48b6-aa56-19046e277723

