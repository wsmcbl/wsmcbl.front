#!/bin/bash

DOCKER_FRONT = wsmcbl-front
UID = $(shell id -u)

help: ## Show this help message
	@echo 'usage: make [target]'
	@echo
	@echo 'targets:'
	@egrep '^(.+)\:\ ##\ (.+)' ${MAKEFILE_LIST} | column -t -c 2 -s ':#'

start: ## Start the containers
	docker network create wsmcbl-front-network || true
	U_ID=${UID} docker-compose up -d

stop: ## Stop the containers
	U_ID=${UID} docker-compose stop

restart: ## Restart the containers
	$(MAKE) stop
	$(MAKE) start

build: ## Rebuilds all the containers
	U_ID=${UID} docker-compose build

ssh-fe: ## ssh's into the be container
	U_ID=${UID} docker exec -it --user ${UID} ${DOCKER_FRONT} bash


# Dotnet commands
dn-pub: ## Dotnet publish command
	U_ID=${UID} docker exec -it --user ${UID} ${DOCKER_FRONT} rm -r bin publish
	U_ID=${UID} docker exec -it --user ${UID} ${DOCKER_FRONT} dotnet publish "wsmcbl.front.csproj" -c Releae -o publish 