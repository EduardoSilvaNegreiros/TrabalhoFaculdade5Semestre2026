# Checklist de Avaliação - Entrega 3

## 1. Identificação do grupo

- [ ] Número do grupo preenchido.
- [ ] Nome completo e RA de todos os integrantes preenchidos.

## 2. C4 Context - C1

- [x] Diagrama em `diagramas/c1-context.puml`.
- [x] Usuarios representados: consumidor, lojista e administrador.
- [x] Sistemas externos representados: pagamento, frete/rastreio, notificações e comunidade/redes sociais.
- [x] Descrição textual de cada elemento no relatório.

## 3. C4 Container - C2

- [x] Diagrama em `diagramas/c2-container.puml`.
- [x] Containers representados: navegador/mobile web, aplicacao ASP.NET Core MVC, MySQL, MongoDB, Redis e SQLite local.
- [x] APIs/serviços externos representados.
- [x] Tecnologias justificadas no relatório.

## 4. C4 Component - C3

- [x] Diagrama em `diagramas/c3-component-backend.puml`.
- [x] Container principal detalhado: backend ASP.NET Core MVC.
- [x] Responsabilidade de controllers, Identity, DbContext, seed, sessão e integrações descrita no relatório.

## 5. Diagrams as Code

- [x] Diagramas escritos em PlantUML com C4-PlantUML.
- [x] Código-fonte dos diagramas incluído no repositório.
- [x] Relatorio indica os caminhos dos arquivos `.puml`.

## 6. Modelagem SQL - MySQL

- [x] Script em `sql/mysql-schema.sql`.
- [x] Minimo de 5 tabelas atendido.
- [x] PKs, FKs, tipos de dados e índices incluídos.
- [x] Modelo relacional/DER textual descrito no relatório.

## 7. Modelagem NoSQL

- [x] MongoDB: colecao `avaliacoes_produto` modelada em JSON.
- [x] Redis: chave `cart:{usuarioId}` como Hash com TTL.
- [x] Redis: sorted set `ranking:produtos:visualizados`.
- [x] Casos de uso e justificativas descritos.
