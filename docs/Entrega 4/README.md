# Entrega 4 - Padrões, APIs e IA

Pasta oficial da quarta entrega do Projeto Integrador.

## Arquivos principais

- `relatorio-entrega-4.pdf`: documento final para submissão.
- `relatorio-entrega-4.md`: fonte editável do relatório.
- `relatorio-entrega-4.html`: versão HTML para impressão.
- `api/openapi.json`: especificação OpenAPI gerada pelo Swagger, filtrada para endpoints `/api`.
- `api/postman_collection.json`: coleção Postman com exemplos reais do catálogo atual.
- `padroes/uml/*.puml`: diagramas UML dos padrões GoF.
- `padroes/uml/rendered/*.svg`: diagramas UML renderizados.
- `checklist-avaliacao.md`: conferência dos itens obrigatórios.

## Links da entrega

- Repositório GitHub: <https://github.com/EduardoSilvaNegreiros/TrabalhoFaculdade5Semestre2026>
- GitHub Projects/Kanban: <https://github.com/users/EduardoSilvaNegreiros/projects/2>
- Nome do Kanban: `Kanban - Projeto 5 Semestre ADS`
- Swagger local: `http://localhost:5016/swagger`
- OpenAPI JSON local: `http://localhost:5016/swagger/v1/swagger.json`
- Postman Collection: `api/postman_collection.json`

## Observações da API

- O Swagger está filtrado para documentar somente rotas iniciadas por `/api`.
- A documentação inclui esquema informativo de autenticação por cookie do ASP.NET Identity.
- Endpoints de carrinho, checkout e pedidos exigem login como `Consumidor`.
- Sem autenticação, esses endpoints retornam `401`; com perfil sem permissão, retornam `403`.
- Não foram criadas novas rotas de API neste refinamento.

## Conferência de commits

Conferência realizada em 31/05/2026. O histórico visível mostra commits associados a `Eduardo <edunegreiross@gmail.com>` e `Eduardo Silva de Negreiros <edunegreiross@gmail.com>`.

Caso o professor exija evidência individual de todos os integrantes, os demais membros devem realizar commits no repositório ou serem incluídos como coautores nos commits.

## Validação executada

- `dotnet restore`: restauração concluída.
- `dotnet build --no-restore`: compilação concluída.
- `dotnet test --no-build`: 10 testes aprovados, cobrindo recomendação, checkout multi-lojista, roles de acesso, slugs, catálogo com 60 produtos, imagens locais, filtros do seed, upload de imagem e proteção de produto por lojista.
- `dotnet list package --vulnerable --include-transitive`: conferido sem vulnerabilidades conhecidas.
- Swagger/OpenAPI: documentação filtrada para rotas `/api`, mantendo 12 operações.
- Catálogo: curadoria realista com 60 produtos, imagens locais rastreadas e preços compatíveis com o mercado brasileiro.
- Banco demo: `meubanco.db` usado como base local para apresentação.
