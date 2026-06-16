# Checklist de Avaliação - Entrega 4

Use esta lista para conferir se a entrega cobre todos os pontos esperados no bloco de Desenvolvimento e no Checkpoint 2.

## 1. Identificação do grupo

- [ ] Número do grupo preenchido.
- [ ] Nome completo e RA de todos os integrantes preenchidos.

## 2. Padrões de projeto (GoF)

- [x] Strategy aplicado na recomendação de produtos.
- [x] Facade aplicado no checkout.
- [x] Factory Method aplicado na escolha do provedor de IA.
- [x] Cada padrão apresenta nome, categoria, problema resolvido, UML e trecho de código no relatório.

## 3. Documentação de APIs

- [x] Swagger configurado em `/swagger`.
- [x] OpenAPI disponível em `/swagger/v1/swagger.json`.
- [x] Arquivo `api/openapi.json` versionado.
- [x] Postman Collection criada em `api/postman_collection.json`.
- [x] OpenAPI documenta apenas endpoints `/api`.
- [x] Mínimo de 10 operações documentadas atendido.
- [x] Métodos HTTP, parâmetros, exemplos e status codes descritos.
- [x] Endpoints protegidos documentados com exigência de login como `Consumidor` e retorno `401/403`.

## 4. Inteligência Artificial na aplicação

- [x] Uso da IA descrito como recomendação personalizada de produtos de beleza.
- [x] Ferramenta escolhida: OpenAI Responses API com fallback local.
- [x] Justificativa técnica registrada no relatório.
- [x] PoC implementada em `POST /api/ia/recomendacoes`.
- [x] Tela de demonstração com cards de produto, imagem, preço, motivo, detalhes e botão de carrinho.

## 5. Checkpoint 2 - Estado atual do projeto

- [x] Estado atual do projeto documentado.
- [x] Validação técnica registrada: build sem erros e 15 testes automatizados aprovados.
- [x] Checkout transacional aplicado via `CheckoutFacade`.
- [x] Carrinho persistente por 7 dias implementado.
- [x] Moderação de produtos implementada para separar produtos pendentes, aprovados e reprovados.
- [x] Lojista consegue atualizar o status de envio dos próprios itens de pedido.
- [x] Painel do lojista atualizado com cadastro, edição e upload validado de imagem.
- [x] Catálogo realista com 60 produtos, imagens locais rastreadas e preços compatíveis com o mercado brasileiro.
- [x] Link do GitHub preenchido.
- [x] Link do Kanban/GitHub Projects preenchido.
- [x] Conferência de commits registrada.
