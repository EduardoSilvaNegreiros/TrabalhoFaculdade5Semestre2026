# Modelagem Redis

O Redis foi escolhido para dados de acesso frequente e baixa durabilidade, especialmente carrinho, recuperacao de carrinho abandonado e rankings simples para recomendacao.

## 1. Carrinho rapido

- **Chave:** `cart:{usuarioId}`
- **Tipo:** `Hash`
- **TTL sugerido:** 30 minutos, renovado a cada interacao do usuario.
- **Caso de uso:** manter o carrinho responsivo sem consultar o banco relacional a cada alteracao.

### Campos

```redis
HSET cart:8f55bfc2 produto:101:quantidade 2
HSET cart:8f55bfc2 produto:101:lojistaId 2
HSET cart:8f55bfc2 produto:101:preco 43.30
HSET cart:8f55bfc2 produto:101:nome "Gel de Limpeza Facial"
HSET cart:8f55bfc2 updatedAt "2026-05-30T20:45:00Z"
EXPIRE cart:8f55bfc2 1800
```

### Justificativa

O carrinho e atualizado com frequencia e precisa responder rapido no mobile. O TTL permite identificar abandono e disparar notificacoes ou e-mails sem manter carrinhos antigos indefinidamente.

## 2. Ranking de produtos visualizados

- **Chave:** `ranking:produtos:visualizados`
- **Tipo:** `Sorted Set`
- **Caso de uso:** recomendar produtos populares e observar tendencias sem consultas agregadas pesadas no MySQL.

```redis
ZINCRBY ranking:produtos:visualizados 1 produto:101
ZINCRBY ranking:produtos:visualizados 1 produto:205
ZREVRANGE ranking:produtos:visualizados 0 9 WITHSCORES
```

### Justificativa

O sorted set mantem a pontuacao de visualizacoes de forma incremental. A aplicacao pode usar os produtos mais vistos como recomendacao simples no catalogo ou na pagina de detalhes.

## 3. Notificacao de carrinho abandonado

- **Chave:** `cart:abandoned:{usuarioId}`
- **Tipo:** `String` ou `JSON`
- **TTL sugerido:** 24 horas apos identificacao do abandono.

```redis
SET cart:abandoned:8f55bfc2 "{\"usuarioId\":\"8f55bfc2\",\"quantidadeItens\":3,\"total\":129.90}"
EXPIRE cart:abandoned:8f55bfc2 86400
```

### Justificativa

Essa estrutura registra um resumo temporario para recuperacao de carrinho. O conteudo completo continua no carrinho ou no banco, enquanto o Redis acelera a identificacao do usuario que deve receber alerta.

