#!/bin/bash

# Instalar netcat-openbsd
apt-get update && apt-get install -y netcat-openbsd

# Esperar a que la base de datos esté disponible
until nc -z db_nextcloud 3306; do
  echo "Esperando a que la base de datos esté lista..."
  sleep 5
done

# Ejecutar comandos php occ como el usuario correcto
sudo -u "#33" php occ config:system:set default_language --value=es
sudo -u "#33" php occ config:system:set theme --value=light
sudo -u "#33" php occ config:system:set maintenance_window_start --value=1
sudo -u "#33" php occ config:system:set overwriteprotocol --value=https
sudo -u "#33" php occ config:system:set overwrite.cli.url --value=https://nextcloud.cbl-edu.com
