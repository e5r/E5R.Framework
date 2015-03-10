# Authentication Server Notes

## Flux

### Request 1
```http
POST /session HTTP/1.1
Host: auth.site.com
X-Auth-AppInstanceID: XXXXXXXXXX
X-Auth-Seal: SHA(AppID:AppPrivateKey:AppInstanceHost/IP)

```

### Response 1
```http
HTTP/1.1 201 Created
X-Auth-AccessToken: XXXXXXXXXX
X-Auth-Nonce: XXXXXXXXXX

```

### Request 2
```http
PUT /session HTTP/1.1
Host: auth.site.com
X-Auth-AppInstanceID: XXXXXXXXXX
X-Auth-AccessToken: XXXXXXXXXX
X-Auth-CNonce: SHA(AppID:AppPrivateKey:AppInstanceHost/IP:X-Auth-Nonce)

```
> `X-Auth-CNonce`: Confirms Nonce

### Response 2
```http
HTTP/1.1 202 Accepted
X-Auth-SealedAccessToken: XXXXXXXXXX
X-Auth-Nonce: XXXXXXXXXX
X-Auth-OCNonce: XXXXXXXXXX

```
> `X-Auth-Nonce` is new. `X-Auth-OCNonce`: Order of CNonce

#### Note(pt-br):

Aqui em `X-Auth-OCNonce` é informada a ordem de concatenação que o servidor espera
receber no campo `X-Auth-CNonce` para as próximas requisições.
Esse campo é um HASH de combinações possíveis, onde o cliente precisará __deduzí-lo__ através da técnica de tentativa e erro,
Ex:
  * SHA(AppID:AppPrivateKey:AppInstanceHost/IP:ID-ORDER-1), ou
  * SHA(AppID:AppPrivateKey:AppInstanceHost/IP:ID-ORDER-2), ou
  * SHA(AppID:AppPrivateKey:AppInstanceHost/IP:ID-ORDER-X).

Quando encontrar o HASH correspondente o cliente saberá qual a ordem esperada. Ex: ID-ORDER-2

Esses IDs de ordem têm uma configuração protocolada entre o cliente e servidor (pode ser global: para todos os clientes do servidor, ou específica: onde cada cliente ao ser cadastrado recebe sua configuração).

Ex:
  * ID-ORDER-1: SHA(AppID:AppPrivateKey:AppInstanceHost/IP:X-Auth-SealedAccessToken:X-Auth-Nonce)
  * ID-ORDER-2: SHA(AppInstanceHost/IP:AppID:X-Auth-SealedAccessToken:AppPrivateKey:X-Auth-Nonce)
  * ID-ORDER-X: SHA(X-Auth-Nonce:AppID:AppPrivateKey:X-Auth-SealedAccessToken:AppInstanceHost/IP)

### Every next requests
```http
GET /resource HTTP/1.1
Host: auth.site.com
X-Auth-AppInstanceID: XXXXXXXXXX
X-Auth-AccessToken: XXXXXXXXXX
X-Auth-CNonce: SHA(ID-ORDER-X-VALUE)

```
> `X-Auth-AccessToken` is a `X-Auth-SealedAccessToken` from Response 2

## Data model

```json
{
    "App": {
        "Id":               "type.string",
        "Name":             "type.string",
        "PrivateKey":       "type.string"
    },

    "AppInstance": {
        "Id":               "type.string",
        "App":              "type.ref(App.Id)",
        "Host":             "type.string"
    },

    "AppNonceOrder": {
        "Id":               "type.string",
        "App":              "type.ref(App.Id)",
        "Template":         "type.string"
    },

    "AccessToken": {
        "Token":            "type.string",
        "AppInstance":      "type.ref(AppInstance.Id)",
        "Nonce":            "type.string",
        "NonceConfirmed":   "type.bool"
    }
}
```
