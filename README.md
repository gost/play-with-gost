# play-with-gost

Site to start easily multiple instance of GOST. 

For example: Create production.mygostserver.com and test.mygostserver.com to create production and test 
environments on 1 machine.

Architecture:

- One NGINX proxy to handle subdomains (https://github.com/jwilder/nginx-proxy)

- Html website with buttons for creating, delete and list GOST instances

- Server-side application (.NET Core) to fire Docker commands

- GOST instances run in completely separate Docker networks, using the <a href="https://docs.docker.com/engine/userguide/networking/">Docker networking</a> technique.  

## Getting started

See <a href="./getting_started.md">getting started</a>

## Web API

See <a href="./web_api.md">web api</a>

## Docker

See used <a href="./docker/startup.md">docker commands</a>.

