Resources
=========

O servidor de autenticação (pode ter o nome melhorado para refletir toda a ideia) permite a extensão de suas funcionalidades através da disponibilização de recursos.

Existem dois tipos de recursos, __Recursos Globais__ e __Recursos da Aplicação__. Os recursos são providenciados por um _plugin_ de recurso, que atende as requisições em um __end point__ específico, e só é acionado pelo próprio servidor, que por sua vez garante que o acesso somente é permitido após a aplicação estar devidamente autenticada.

## Algumas notas

O servidor só autentica aplicações, e só permite o acesso a recursos para aplicações autenticadas. O servidor não tem interface gráfica de usuário (e não deve ter mesmo, é uma premissa).

#### E como o servidor faz pra autenticar um usuário sem a interface gráfica?

__Não faz__, o servidor não exibe interface para autenticar usuários.

#### Então o servidor não presta!

Calma! O servidor provê toda a API pra autenticação de usuários, só não dá a interface para o usuário se autenticar. Isso deve ser feito por uma aplicação autêntica.

## Voltando aos recursos

Existe um recurso __"/session"__. Qualquer tentativa de acesso a este recurso, resultará em um __401 Unauthorized__ se a aplicação não estiver previamente autenticada no servidor.

Além de autenticada, a aplicação também deve ter permissões no recurso específico.

```json
Resource {
  "Id": "USER-SESSION",
  "Name": "User Session Management",
  "EndPoint": "/session"
}

AppResources{
  "AppId": "as8df79a8s7df9s7adf98",
  "Resource": "USER-SESSION",
  "Grant": ["CREATE", "READ", "UPDATE", "DELETE"]
}
```

Uma simples tela de login, é na verdade uma aplicação que tem acesso a esses dois recursos (de forma básica).

Assim, é simples extender o servidor para provê a tela de login para o usuário.

Uma aplicação apresenta a tela para obter nome de usuário e senha (estamos desconsiderando multiplos fatores, mas eles são providos pelo servidor), e aciona os recursos.

1. __GET "/user"__ para obter o ID do usuário;
2. Se existe, __PUT "/session"__ passando as informações de usuário;
3. Retorna o token da sessão criada.

Pronto, usuário LOGADO.

Quando uma aplicação precisar de dados do usuário, ela solicita ao servidor para vincular o token do usuário, com o seu token através de um redirect.

1. Cria um callback

Request
```http
POST /app-session/callback HTTP/1.1
Host: auth.site.com
X-Auth-AppInstanceID: XXXXXXXXXX
X-Auth-SealedAccessToken: XXXXXXXXXX
X-Auth-CNonce: SHA(ID-ORDER-X-VALUE)
X-Auth-CallbackUrl: http://my.url

```

Response
```http
HTTP/1.1 201 cREATED
X-Auth-Callback: {returnet-callback-token}

```

2. Redireciona para o vínculo

```http
GET /session/link?cbk={returnet-callback-token} HTTP/1.1
Host: auth.site.com

```

O servidor obtem o callback cadastrado através do token e assim já sabe a url, aplicação, tokenId, etc.

Faz o vínculo e redirecona para "http://my.url".
