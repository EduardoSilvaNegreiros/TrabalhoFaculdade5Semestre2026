# Modelagem Redis

O Redis foi escolhido para dados de acesso frequente e baixa durabilidade, especialmente carrinho, recuperação de carrinho abandonado e rankings simples para recomendação.

## 1. Carrinho rápido

- **Chave:** `cart:{usuarioId}`
- **Tipo:** `Hash`
- **TTL sugerido:** 30 minutos, renovado a cada interação do usuário.
- **Caso de uso:** manter o carrinho responsivo sem consultar o banco relacional a cada alteração.

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

O carrinho é atualizado com frequência e precisa responder rápido no mobile. O TTL permite identificar abandono e disparar notificações ou e-mails sem manter carrinhos antigos indefinidamente.

## 2. Ranking de produtos visualizados

- **Chave:** `ranking:produtos:visualizados`
- **Tipo:** `Sorted Set`
- **Caso de uso:** recomendar produtos populares e observar tendências sem consultas agregadas pesadas no MySQL.

```redis
ZINCRBY ranking:produtos:visualizados 1 produto:101
ZINCRBY ranking:produtos:visualizados 1 produto:205
ZREVRANGE ranking:produtos:visualizados 0 9 WITHSCORES
```

### Justificativa

O sorted set mantém a pontuação de visualizações de forma incremental. A aplicação pode usar os produtos mais vistos como recomendação simples no catálogo ou na página de detalhes.

## 3. Notificação de carrinho abandonado

- **Chave:** `cart:abandoned:{usuarioId}`
- **Tipo:** `String` ou `JSON`
- **TTL sugerido:** 24 horas após identificação do abandono.

```redis
SET cart:abandoned:8f55bfc2 "{\"usuarioId\":\"8f55bfc2\",\"quantidadeItens\":3,\"total\":129.90}"
EXPIRE cart:abandoned:8f55bfc2 86400
```

### Justificativa

Essa estrutura registra um resumo temporário para recuperação de carrinho. O conteúdo completo continua no carrinho ou no banco, enquanto o Redis acelera a identificação do usuário que deve receber alerta.
