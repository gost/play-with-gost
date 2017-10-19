# play-with-gost

Website that works like <a href="http://www.play-with-docker.com">play-with-docker</a>.

Requirements:

- Button 'Create new GOST instance'

- Expose new unique endpoint for new GOST instance

- Shut down after a period

## Web API

Web Api for creating, deleting and listing projects

- list all projects

```
$ curl http://localhost:50117/api/gost_instance
```

- Create project

```
$ curl -X POST -H "content-type:application/json" -d '{"Name":"xyz","Tld":"lvh.me"}' http://localhost:50117/api/gost_instance
```

- Delete project

```
$ curl -X DELETE http://localhost:50117/api/gost_instance?name=xyz
```

## Docker

See <a href="./docker/startup.md">docker startup</a>

