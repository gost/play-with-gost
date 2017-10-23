export GOST_NAME=bert1
export GOST_TLD=lvh.me
export VIRTUAL_HOST=$GOST_NAME.$GOST_TLD
docker network create $VIRTUAL_HOST
docker network connect $VIRTUAL_HOST nginx-proxy
docker-compose -p $GOST_NAME up -d
