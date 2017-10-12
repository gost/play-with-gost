# Docker stuff

This document describes the commands used to start multiple GOST projects. 
In this example we're starting 2 projects: 

1. gost-prod on port 8080, external uri: http://gost_prod.lvh.me

2. gost-test on port 8081, external uri: http://gost_test.lvh.me

Steps:

- Start jwilder/nginx-proxy 

```
$ docker run -d -p 80:80 --name nginx-proxy -v /var/run/docker.sock:/tmp/docker.sock:ro jwilder/nginx-proxy
```

- Create GOST network

```
$ docker network create gost-network
```

- Join nginx proxy and gost-network

```
$ docker network connect gost-network nginx-proxy
```

- Set environment variables PORT and VIRTUAL_HOST

```
$ export PORT=8080
$ export VIRTUAL_HOST=gost_prod.lvh.me
```

- Run docker-compose for project 'gost_prod'

```
$ docker-compose -p gost_prod up -d
```

- Test!

Go to http://gost_prod.lvh.me and the GOST dashboard should be visible.

- Start another project (gost_test.lvh.me, 8081)

```
$ export PORT=8081
$ export VIRTUAL_HOST=gost_test.lvh.me
$ docker-compose -p gost_test up -d
```

and this project should be visible on http://gost_test.lvh.me

To shut down the projects do:

```
$ docker-compose -p gost_prod down
$ docker-compose -p gost_test down
```

## Dashboard changes

The docker-compose file uses a minor modified dashboard (bertt/gost-dashboard). 

Changes: 

- dashboard nginx internal port is 80 instead of 8080
- https code is removed

todo: find out how to prevent this.
