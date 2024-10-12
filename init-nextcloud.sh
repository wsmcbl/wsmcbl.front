#!/bin/bash

# Instalar netcat-openbsd
apt-get update && apt-get install -y netcat-openbsd

# Esperar a que la base de datos esté disponible
until nc -z db_nextcloud 3306; do
  echo "Esperando a que la base de datos esté lista..."
  sleep 5
done

# Cambiar el idioma por defecto a español
php occ config:system:set default_language --value=es
# Establecer el tema por defecto
php occ config:system:set theme --value=light
# Establecer la ventana de mantenimiento
php occ config:system:set maintenance_window_start --value=1
# Configurar el protocolo de sobreescritura.
php occ config:system:set overwriteprotocol --value=https
# Configurar la URL de sobreescritura
php occ config:system:set overwrite.cli.url --value=https://nextcloud.cbl-edu.com
