#!/bin/bash

docker exec -u www-data wsmcblfront-nextcloud-1 php occ config:system:set default_language --value=es
docker exec -u www-data wsmcblfront-nextcloud-1 php occ config:system:set theme --value=light
docker exec -u www-data wsmcblfront-nextcloud-1 php occ config:system:set maintenance_window_start --value=1
docker exec -u www-data wsmcblfront-nextcloud-1 php occ config:system:set overwriteprotocol --value=https
docker exec -u www-data wsmcblfront-nextcloud-1 php occ config:system:set overwrite.cli.url --value=https://nextcloud.cbl-edu.com