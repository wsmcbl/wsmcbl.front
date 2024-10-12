#!/bin/bash

# Instalar netcat-openbsd
apt-get update && apt-get install -y netcat-openbsd

# Esperar a que la base de datos esté disponible
until nc -z db_nextcloud 3306; do
  echo "Esperando a que la base de datos esté lista..."
  sleep 5
done

# Ejecutar comandos php occ directamente, ya que se ejecutarán como www-data
php occ config:system:set default_language --value=es
php occ config:system:set theme --value=light
php occ config:system:set maintenance_window_start --value=1
php occ config:system:set overwriteprotocol --value=https
php occ config:system:set overwrite.cli.url --value=https://nextcloud.cbl-edu.com
