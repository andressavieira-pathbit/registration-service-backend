# registration-service-backend

## RegistrationServiceAPI e Docker

Primeiraimente, rode o comando "docker-compose up -d" no terminal dentro da aplicação RegistrationServiceAPI para subir o Docker com o MongoDB e com o RabbitMQ.

### Acompanhando o RabbitMQ

Navegue para `localhost:15672` para acompanhar a fila "cadastro.em.analise.email" na interface de administrador do RabbitMQ.

Preencha com essas informações:

* Login: guest
* Senha: guest

## Worker 

Após o preenchimento do formulário na aplicação web, rode o Worker e acompanhe o consumo da mensagem e envio do e-mail.

### Utilizando o NoSQLBooster for MongoDB

Para verificar as informações no banco, baixe o app "NoSQLBooster for MongoDB" e entre com as informações dadas no arquivo "docker-compose.yml".
