# Roteiro de apresentação

Use este roteiro para demonstrar o Beauty Marketplace nas Entregas 3 e 4.

## 1. Visitante

1. Abrir o projeto e acessar o catálogo.
2. Mostrar produtos reais de maquiagem, skincare e cabelos.
3. Aplicar filtros por categoria, tipo de pele, tipo de cabelo, marca, preço e produto vegano.
4. Abrir detalhes de um produto e mostrar descrição, composição, lojista, avaliações e recomendações.

## 2. Consumidor

Conta: `cliente@beautymarket.com` / `Cliente@123`

1. Entrar como consumidor.
2. Filtrar produtos por tipo de pele ou cabelo.
3. Adicionar produtos de lojistas diferentes ao carrinho.
4. Abrir o carrinho e mostrar compra unificada, lojistas diferentes, subtotal, frete e total.
5. Finalizar a compra com CEP e forma de pagamento.
6. Abrir pedidos e mostrar status e código de rastreio por item.
7. Salvar um produto na lista de desejos.
8. Sair e entrar novamente para demonstrar que o carrinho pode ser recuperado quando houver itens não finalizados.
9. Acessar a página de Recomendação IA e gerar sugestões com cards de produto.

## 3. Lojista

Conta: `lojista@beautymarket.com` / `Lojista@123`

1. Entrar como lojista.
2. Abrir o Painel do Lojista.
3. Mostrar faturamento, repasse, pedidos novos, estoque baixo e produtos cadastrados.
4. Atualizar estoque de um produto.
5. Cadastrar um novo produto com imagem válida e mostrar que ele entra como `Pendente`.
6. Editar produto próprio e confirmar que o lojista não acessa produto de outro lojista.
7. Atualizar o status de envio de um item recebido para `Enviado` ou `Entregue`.

## 4. Administrador

Conta: `admin@beautymarket.com` / `Admin@123`

1. Entrar como administrador.
2. Abrir Administração.
3. Mostrar aprovação/reprovação de lojistas.
4. Aprovar ou reprovar produto pendente.
5. Alterar comissão por categoria.
6. Abrir o Mapa de Atendimento.
7. Demonstrar onde RF01 a RF10 aparecem no sistema.

## 5. Entrega 3

1. Abrir `docs/Entrega 3/README.md`.
2. Mostrar relatório, PDF, C4 C1/C2/C3 em PlantUML, SQL MySQL, MongoDB e Redis.
3. Reforçar que o protótipo usa SQLite local, mas a modelagem oficial relacional está em MySQL.
4. Mostrar que o SQL inclui produtos moderados e carrinho persistido.

## 6. Entrega 4

1. Abrir `docs/Entrega 4/README.md`.
2. Mostrar padrões GoF: Strategy, Facade e Factory Method.
3. Abrir `/swagger` e mostrar somente endpoints `/api`.
4. Abrir `docs/Entrega 4/api/postman_collection.json`.
5. Mostrar a PoC de IA pelo endpoint e pela tela do consumidor.
6. Mostrar GitHub, Kanban e conferência de commits.

## 7. Comandos de validação

```powershell
dotnet build --no-restore
dotnet test --no-build
dotnet list package --vulnerable --include-transitive
```

Checklist rápido:

- `/api/produtos` retorna 60 produtos aprovados.
- `/swagger/v1/swagger.json` contém somente caminhos iniciados por `/api`.
- Endpoints protegidos de carrinho, checkout e pedidos retornam `401/403` sem login.
- Catálogo, carrinho, dashboard do lojista e administração estão responsivos.
- Produto novo de lojista aparece como pendente até aprovação do admin.
