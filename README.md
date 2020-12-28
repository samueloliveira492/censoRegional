# CensoRegional
API utilizando ASP.Net Core 2.2 - NEO4J - Mediatr - RabbitMQ - SignalR - Angular

Para executar é necessário executar o comando na raiz do projeto docker-compose up

A api de consumo está na url http://localhost:20000/swagger/index.html

A api de comandos está na url http://localhost:20001/swagger/index.html

O banco de dados está em http://localhost:7474/browser/ com usuário neo4j e senha neo

O rabbitMQ está na url http://localhost:15672/ com usuario desafioCenso e senha desafioCenso

Acesso ao azure devops com a automação da pipeline https://dev.azure.com/samueloliveira457/CensoRegional

Acesso ao sonarcloud com os codesmells do projeto https://sonarcloud.io/dashboard?branch=development&id=samueloliveira457_CensoRegional

Para o projeto web não foi configurado para ser executado via docker-compose sendo necessário executá-lo de forma separada
