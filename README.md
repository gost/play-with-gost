# play-with-gost

Website that works like <a href="http://www.play-with-docker.com">play-with-docker</a>.

Requirements:

- Button 'Create new GOST instance'

- Expose new unique endpoint for new GOST instance

- Shut down after a period

## Docker

- Step 1: Run NginxProxy

```
$ docker run -d -p 80:80 --name nginx-proxy -v /var/run/docker.sock:/tmp/docker.sock:ro jwilder/nginx-proxy
```

