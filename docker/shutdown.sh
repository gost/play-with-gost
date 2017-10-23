export GOST_NAME=bert1
export GOST_TLD=lvh.me
export VIRTUAL_HOST=$GOST_NAME.$GOST_TLD

docker network disconnect $VIRTUAL_HOST nginx-proxy
docker-compose -p $GOST_NAME down -v
docker network rm  $VIRTUAL_HOST


