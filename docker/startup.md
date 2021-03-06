# Docker stuff

This document describes the commands used to start multiple GOST projects. 
In this example we're starting 2 projects on a local machine: 

1. gost_prod, external uri: http://gost_prod.lvh.me

2. gost_test, external uri: http://gost_test.lvh.me

The site lvh.me is a handy site to test the subdomains: it redirects to localhost machine.

Steps:

- Start jwilder/nginx-proxy 

```
$ docker run -d -p 80:80 --name nginx-proxy -v /var/run/docker.sock:/tmp/docker.sock:ro jwilder/nginx-proxy
```

1] Create Gost_prod

- Create Docker network for this application

```
$ docker network create gost_prod.lvh.me
```

- Join nginx proxy and new gost-network

```
$ docker network connect gost_prod.lvh.me nginx-proxy
```

- Set environment variable VIRTUAL_HOST

```
$ export VIRTUAL_HOST=gost_prod.lvh.me
```

- Run docker-compose for project 'gost_prod'

```
$ docker-compose -p gost_prod up -d
```

- Test!

Go to http://gost_prod.lvh.me and the GOST dashboard should be visible.

Insert something (Location) 

- Start another project (gost_test.lvh.me)

```
$ export VIRTUAL_HOST=gost_test.lvh.me
$ docker network create gost_test.lvh.me
$ docker network connect gost_test.lvh.me nginx-proxy
$ docker-compose -p gost_test up -d
```

and this project should be visible on http://gost_test.lvh.me

To shut down the projects do:

```
$ docker-compose -p gost_prod down
$ docker-compose -p gost_test down
```

## Dashboard changes

The docker-compose file uses a slightly modified dashboard (bertt/gost-dashboard). 

Changes: 

- dashboard nginx internal port is 80 instead of 8080
- https code is removed

todo: find out how to prevent this.
