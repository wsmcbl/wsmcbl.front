#!/bin/bash

help: ## Show this help message
	@echo 'usage: make [target]'
	@echo
	@echo 'targets:'
	@egrep '^(.+)\:\ ##\ (.+)' ${MAKEFILE_LIST} | column -t -c 2 -s ':#'

b = unknown

mer-b2dev: ## Merge branch (b) into develop
	git checkout develop
	git merge --no-ff -m "Merge $(b) into develop" $(b)
	git checkout $(b)

mer-dev2b: ## Merge develop into branch (b)
	git checkout $(b)
	git merge --no-ff -m "Merge develop into $(b)" develop

mer-cur2dev: ## Merge current-branch into develop
	$(MAKE) mer-b2dev b=$(shell git rev-parse --abbrev-ref HEAD)

mer-cur2b: ## Merge current-branch into branch (b)
	$(MAKE) mer-cur2dev
	$(MAKE) mer-dev2b

gpc: ## git push origin current brach
	git push origin $(shell git rev-parse --abbrev-ref HEAD)



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

