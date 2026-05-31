# Entrega 4 - Padrões, APIs e IA

Pasta oficial da quarta entrega do Projeto Integrador.

## Arquivos principais

- `relatorio-entrega-4.pdf`: documento final para submissão.
- `relatorio-entrega-4.md`: fonte editável do relatório.
- `relatorio-entrega-4.html`: versão HTML para impressão.
- `api/openapi.json`: especificação OpenAPI gerada pelo Swagger, filtrada para endpoints `/api`.
- `api/postman_collection.json`: coleção Postman com exemplos.
- `padroes/uml/*.puml`: diagramas UML dos padrões GoF.
- `padroes/uml/rendered/*.svg`: diagramas UML renderizados.
- `checklist-avaliacao.md`: conferência dos itens obrigatórios.

## Links da entrega

- Repositório GitHub: <https://github.com/EduardoSilvaNegreiros/TrabalhoFaculdade5Semestre2026>
- GitHub Projects/Kanban: <https://github.com/users/EduardoSilvaNegreiros/projects/2>
- Nome do Kanban: `Kanban - Projeto 5 Semestre ADS`
- Swagger local: `http://localhost:5016/swagger`
- Postman Collection: `api/postman_collection.json`

## Conferência de commits

Conferência realizada em 31/05/2026. O histórico visível mostra commits associados a `Eduardo <edunegreiross@gmail.com>` e `Eduardo Silva de Negreiros <edunegreiross@gmail.com>`.

Caso o professor exija evidência individual de todos os integrantes, os demais membros devem realizar commits no repositório ou serem incluídos como coautores nos commits.

## Validação executada

- `dotnet restore`: restauração concluída.
- `dotnet build --no-restore`: compilação concluída com 0 erros e 0 warnings.
- `dotnet test --no-build`: 3 testes aprovados, cobrindo recomendação, checkout multi-lojista e roles de acesso.
- `dotnet list package --vulnerable --include-transitive`: conferido sem vulnerabilidades conhecidas.
- Swagger/OpenAPI: documentação filtrada para rotas `/api`, mantendo mais de 10 operações.

Observação: endpoints de carrinho, checkout e pedidos exigem login com perfil `Consumidor`; sem autenticação retornam `401/403`, conforme esperado.
