# Checklist de Avaliação - Entrega 3

Use esta lista para conferir se a entrega cobre todos os pontos esperados no bloco de Arquitetura.

## 1. Identificação do grupo

- [x] Número do grupo preenchido.
- [x] Nome completo e RA de todos os integrantes preenchidos.

## 2. Diagrama C4 - Nível Context (C1)

- [x] Diagrama disponível em `diagramas/c1-context.puml`.
- [x] Usuários representados: consumidor, lojista e administrador.
- [x] Sistemas externos representados: pagamento, frete/rastreio, notificações e comunidade/redes sociais.
- [x] Relatório com descrição textual dos elementos do contexto.

## 3. Diagrama C4 - Nível Container (C2)

- [x] Diagrama disponível em `diagramas/c2-container.puml`.
- [x] Containers representados: navegador/mobile web, aplicação ASP.NET Core MVC, MySQL, MongoDB, Redis e SQLite local.
- [x] APIs e serviços externos representados no fluxo.
- [x] Tecnologias justificadas semanticamente no relatório.

## 4. Diagrama C4 - Nível Component (C3)

- [x] Diagrama disponível em `diagramas/c3-component-backend.puml`.
- [x] Container principal detalhado: backend ASP.NET Core MVC.
- [x] Responsabilidades de controllers, Identity, DbContext, seed, sessão e integrações descritas no relatório.

## 5. Diagrams as Code

- [x] Diagramas escritos em PlantUML com C4-PlantUML.
- [x] Código-fonte dos diagramas incluído no repositório.
- [x] Relatório informa claramente os caminhos dos arquivos `.puml`.

## 6. Modelagem de dados - SQL (MySQL)

- [x] Script disponível em `sql/mysql-schema.sql`.
- [x] Mínimo de 5 tabelas atendido.
- [x] PKs, FKs, tipos de dados e índices incluídos.
- [x] Modelo relacional e DER textual descritos no relatório.

## 7. Modelagem de dados - NoSQL

- [x] MongoDB: coleção `avaliacoes_produto` modelada em JSON.
- [x] Redis: chave `cart:{usuarioId}` modelada como Hash com TTL.
- [x] Redis: Sorted Set `ranking:produtos:visualizados` modelado.
- [x] Casos de uso e justificativas descritos com clareza.
