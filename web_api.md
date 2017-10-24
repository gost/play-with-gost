# Web API

Web Api for creating, deleting and listing projects

## list all projects

```
$ curl http://localhost:50117/api/gost_instance
```

## Create project

```
$ curl -X POST -H "content-type:application/json" -d '{"Name":"xyz","Tld":"lvh.me"}' http://localhost:50117/api/gost_instance
```

## Delete project

```
$ curl -X DELETE http://localhost:50117/api/gost_instance?name=xyz&tld=topleveldomain
```
