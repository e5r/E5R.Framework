# Authentication Server Notes

### Request 1
```http
POST /session HTTP/1.1
Host: auth.site.com
X-Auth-AppToken: XXXXXXXXXX

```

### Response 1
```http
HTTP/1.1 201 Session Created
X-Auth-SessionToken: XXXXXXXXXX
X-Auth-Nonce: XXXXXXXXXX

```

### Request 2
```http
PUT /session HTTP/1.1
Host: auth.site.com
X-Auth-AppToken: XXXXXXXXXX
X-Auth-Nonce: XXXXXXXXXX

```

### Response 2
```http
HTTP/1.1 201 Session Created
X-Auth-SessionToken: XXXXXXXXXX
X-Auth-Nonce: XXXXXXXXXX

```
