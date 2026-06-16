# Roteiro de apresentação

Use este roteiro para demonstrar o Beauty Marketplace de forma objetiva, conectando a navegação do sistema com as evidências das Entregas 3 e 4.

## 1. Abertura do sistema

1. Iniciar o projeto e abrir o catálogo público.
2. Explicar rapidamente a proposta do marketplace: múltiplos lojistas, compra unificada, perfis distintos e foco em produtos de beleza.
3. Mostrar filtros por categoria, tipo de pele, tipo de cabelo, marca, faixa de preço e produto vegano.

## 2. Fluxo do visitante

1. Abrir um produto no catálogo.
2. Mostrar descrição, composição, lojista, avaliações e recomendações.
3. Destacar que o catálogo público exibe apenas produtos aprovados.

## 3. Fluxo do consumidor

Conta: `cliente@beautymarket.com` / `Cliente@123`

1. Entrar como consumidor.
2. Filtrar produtos por tipo de pele ou cabelo.
3. Adicionar produtos de lojistas diferentes ao carrinho.
4. Abrir o carrinho e mostrar compra unificada, subtotal, frete e total.
5. Finalizar a compra com CEP e forma de pagamento.
6. Abrir pedidos e mostrar status e rastreio por item.
7. Salvar um produto na lista de desejos.
8. Sair e entrar novamente para demonstrar recuperação do carrinho persistido.
9. Acessar `Consumidor > Recomendação IA` e gerar sugestões com cards de produto.

## 4. Fluxo do lojista

Conta: `lojista@beautymarket.com` / `Lojista@123`

1. Entrar como lojista.
2. Abrir o Painel do Lojista.
3. Mostrar faturamento, repasse, pedidos novos, estoque baixo e produtos cadastrados.
4. Atualizar estoque de um produto.
5. Cadastrar um novo produto com imagem válida e destacar que ele entra como `Pendente`.
6. Editar produto próprio e reforçar que o lojista não acessa produto de outro lojista.
7. Atualizar o status de envio de um item para `Enviado` ou `Entregue`.

## 5. Fluxo do administrador

Conta: `admin@beautymarket.com` / `Admin@123`

1. Entrar como administrador.
2. Abrir Administração.
3. Mostrar aprovação e reprovação de lojistas.
4. Aprovar ou reprovar produto pendente.
5. Alterar comissão por categoria.
6. Abrir o Mapa de Atendimento.
7. Relacionar visualmente onde RF01 a RF10 aparecem no sistema.

## 6. Apresentação da Entrega 3

1. Abrir `docs/Entrega 3/README.md`.
2. Mostrar `relatorio-entrega-3.pdf` como documento consolidado.
3. Abrir os diagramas C4 C1, C2 e C3 em PlantUML.
4. Mostrar `sql/mysql-schema.sql` como modelagem relacional oficial da entrega.
5. Reforçar que o protótipo executa localmente com SQLite, mas a modelagem formal foi produzida em MySQL.
6. Mostrar a modelagem MongoDB e Redis em `docs/Entrega 3/nosql/`.

## 7. Apresentação da Entrega 4

1. Abrir `docs/Entrega 4/README.md`.
2. Mostrar `relatorio-entrega-4.pdf` como documento consolidado.
3. Explicar os padrões GoF aplicados: Strategy, Facade e Factory Method.
4. Abrir `/swagger` e destacar que somente endpoints `/api` estão documentados.
5. Abrir `docs/Entrega 4/api/postman_collection.json`.
6. Mostrar a PoC de IA pela API e pela tela do consumidor.
7. Mostrar GitHub, Kanban e evidências do Checkpoint 2.

## 8. Comandos de validação

```powershell
dotnet restore
dotnet build --no-restore
dotnet test --no-restore
```

## 9. Checklist rápido para apresentação

- `/api/produtos` retorna apenas produtos aprovados.
- `/swagger/v1/swagger.json` contém somente caminhos iniciados por `/api`.
- Endpoints protegidos de carrinho, checkout e pedidos retornam `401/403` sem login adequado.
- Catálogo, carrinho, painel do lojista e administração estão responsivos.
- Produto novo de lojista permanece pendente até aprovação do administrador.
