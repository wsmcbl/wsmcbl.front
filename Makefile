#!/bin/bash

help: ## Show this help message
	@echo 'usage: make [target]'
	@echo
	@echo 'targets:'
	@egrep '^(.+)\:\ ##\ (.+)' ${MAKEFILE_LIST} | column -t -c 2 -s ':#'

build: ## Rebuilds all the containers
	docker-compose build

run: ## Start the containers
	docker-compose up -d

stop: ## Stop the containers
	docker-compose stop

restart: ## Restart the containers
	$(MAKE) stop && $(MAKE) run

remake: ## Stop, build and run the containers
	$(MAKE) stop && $(MAKE) build && $(MAKE) run

logs: ## Show all logs
	docker-compose logs	

delete-containers: ## Remove all containers 
	docker-compose down

delete-all-services: ## Remove all containers and volumes (***CAUTION***)
	docker-compose down --volumes --remove-orphans
	docker system prune
	docker network create app-network || true

