# Beauty Marketplace - Entrega Final / Entrega 5 - Roteiro passo a passo do Video de Defesa

Esta e a **Entrega Final / Entrega 5**, correspondente ao **Bloco 5 - Integracao & Entrega Final**. A entrega foi postergada em funcao da licenca medica do professor entre 16/02 e 18/03/2026.

Este documento serve como apoio para a defesa final do **Beauty Marketplace**. A ideia nao e ler tudo palavra por palavra, mas usar as falas como guia para uma apresentacao natural, mostrando o sistema com calma e explicando as decisoes do grupo.

## Regra principal da gravação

Mostrem **uma tela por vez**.

Evitem tela dividida ou varias janelas competindo pela atencao. Quando a fala passar por consumidor, lojista e administrador, terminem uma ideia em uma tela antes de trocar para a proxima. Isso deixa o video mais facil de acompanhar.

## Dados gerais

- Grupo: **4**
- Projeto: **Beauty Marketplace**
- Entrega: **Entrega Final / Entrega 5 - Bloco 5: Integracao & Entrega Final**
- Duracao alvo: **19 a 21 minutos**
- Duracao permitida pela entrega: **15 a 25 minutos**
- Formato: video de defesa final com todos os integrantes participando
- Link final do video: `https://youtu.be/8xRP4LWxfyA?is=C3TIzKe_77GF9F3Z`
- Forma de entrega: submeter no Classroom o link do video e o link do repositorio GitHub final.

## Integrantes e ordem sugerida

1. **Eduardo Silva de Negreiros** - RA **924109760**
2. **Josue Padetti Correa** - RA **924109806**
3. **Gabrielle Victoria de Souza Barboza** - RA **424106162**
4. **Daiane Jheniffer da Silva Araujo** - RA **1726105657**
5. **Beatriz Cerqueira Sonoro** - RA **924106243**
6. **Kauanne Vitoria Soares Bernardo** - RA **924111927**
7. **Julio Guedes de Oliveira** - RA **926107422**
8. **Matheus Da Silva Reis** - RA **926111266**
9. **Micael Dantas da Silva** - RA **924110378**
10. **Wesley Weber Fernandes** - RA **924107330**
11. **Bryan Willian da Silva Almeida** - RA **925116744**

Se a ordem real mudar, tudo bem. O importante e que todos participem e que cada pessoa fale de uma parte que conhece.

## Preparacao antes de gravar

### Abas para deixar abertas, na ordem

1. PDF ou slide de capa do video.
2. Sistema web: `http://localhost:5016`
3. Catálogo: `http://localhost:5016/Produto`
4. Figma do protótipo.
5. Diagrama C1: `docs/Entrega 3/diagramas/rendered/c1-context.svg`
6. Diagrama C2: `docs/Entrega 3/diagramas/rendered/c2-container.svg`
7. Diagrama C3: `docs/Entrega 3/diagramas/rendered/c3-component-backend.svg`
8. SQL: `docs/Entrega 3/sql/mysql-schema.sql`
9. MongoDB: `docs/Entrega 3/nosql/mongodb-avaliacoes.json`
10. Redis: `docs/Entrega 3/nosql/redis-estruturas.md`
11. Swagger: `http://localhost:5016/swagger`
12. OpenAPI: `docs/Entrega 4/api/openapi.json`
13. Postman Collection: `docs/Entrega 4/api/postman_collection.json`
14. Repositorio GitHub.
15. GitHub Projects / Kanban.

### Contas de demonstração

- Consumidor: `cliente@beautymarket.com` / `Cliente@123`
- Lojista: `lojista@beautymarket.com` / `Lojista@123`
- Administrador: `admin@beautymarket.com` / `Admin@123`

### Como organizar os logins

O ideal e gravar em blocos separados, porque o sistema muda menus conforme o perfil logado.

Bloco publico: gravar capa, catálogo, produto, Figma, diagramas, Swagger e GitHub sem depender de login.

Bloco consumidor: entrar como consumidor e gravar carrinho, pedidos, lista de desejos e recomendação IA.

Bloco lojista: entrar como lojista e gravar painel, pedidos recebidos, estoque e produto pendente.

Bloco administrador: entrar como administrador e gravar aprovação de lojistas, moderação de produtos, comissões e mapa de atendimento.

Na edicao, juntem os blocos na ordem do roteiro. Assim a apresentacao fica fluida e ninguem precisa correr com troca de login no meio da gravacao.

## Visao geral do tempo

| Tempo | Integrante | Tema | Tela principal |
| --- | --- | --- | --- |
| 0:00 - 0:50 | Eduardo | Abertura | Capa |
| 0:50 - 2:10 | Josue | Apresentacao do projeto | Catálogo e produto |
| 2:10 - 3:35 | Gabrielle | Contexto do problema | Filtros e detalhes |
| 3:35 - 5:00 | Daiane | Impacto social | Admin ou painel do lojista |
| 5:00 - 6:50 | Beatriz | Modelo de negocio | Uma tela por perfil |
| 6:50 - 9:20 | Kauanne | Prototipo Figma | Figma e tela equivalente |
| 9:20 - 11:10 | Julio | Arquitetura C4 | C1 e C2 |
| 11:10 - 13:05 | Matheus | Componentes e dados | C3, SQL, MongoDB, Redis |
| 13:05 - 15:35 | Micael | API documentada | Swagger, OpenAPI, Postman |
| 15:35 - 18:20 | Wesley | Funcionalidade de IA | Recomendação IA |
| 18:20 - 20:10 | Bryan | Repositorio e conclusao | GitHub, docs e Kanban |

## Guia simples de cliques e telas

Use esta parte como cola para quem estiver compartilhando a tela. Quem fala pode explicar com as proprias palavras; quem controla a tela so precisa seguir a ordem com tranquilidade.

### Eduardo - abertura

1. Abra a capa do PDF ou um slide inicial.
2. Deixe aparecendo o nome `Beauty Marketplace`.
3. Deixe aparecendo `Grupo 4`.
4. Deixe aparecendo a lista de integrantes.
5. Nao clique em nada durante a fala.
6. Quando Eduardo terminar, troque para o navegador com o sistema aberto.

### Josue - apresentacao do projeto

1. Abra `http://localhost:5016`.
2. Clique no menu `Catálogo`, se o catálogo ainda não estiver aberto.
3. Mostre o topo com a marca `BeautyMarket`.
4. Role devagar até aparecerem os filtros.
5. Pare na parte dos filtros e deixe visível: `Buscar`, `Categoria`, `Tipo de pele`, `Tipo de cabelo`, `Marca`, `Preço max.` e `Somente veganos`.
6. Role para baixo até aparecerem os cards dos produtos.
7. Deixe um card bem visível com imagem, nome, marca, lojista, preço e estoque.
8. Clique na imagem do produto para abrir os detalhes. Se aparecer o botão `Ver detalhes`, ele também pode ser usado.
9. Na tela de detalhes, mostre imagem grande, preço, descrição e informações de pele/cabelo.
10. Quando terminar, volte para o `Catálogo`.

### Gabrielle - problema

1. No `Catálogo`, pare exatamente na área de filtros.
2. Clique no campo `Tipo de pele`.
3. Escolha `Oleosa`, se essa opcao estiver disponivel.
4. Clique em `Aplicar filtros`.
5. Mostre a quantidade de produtos encontrados.
6. Mostre os filtros ativos, se aparecerem na tela.
7. Role até os produtos filtrados.
8. Abra um produto clicando na imagem do card. Se aparecer o botão `Ver detalhes`, ele também pode ser usado.
9. Mostre `Tipo de pele`, `Tipo de cabelo`, `Composicao` e `Estoque`.
10. Role um pouco mais e mostre `Avaliacoes de clientes`.
11. Mostre também o nome do lojista em `vendido por`.

### Daiane - impacto social

1. Entre com a conta de lojista, se esse bloco for gravado separado.
2. Clique em `Painel do Lojista`.
3. Mostre os cards do topo: `Produtos cadastrados`, `Itens vendidos`, `Faturamento`, `Repasse` e `Aguardando moderação`.
4. Role até `Pedidos recebidos`.
5. Mostre que o lojista consegue acompanhar pedidos e status.
6. Role até `Estoque físico e catálogo`.
7. Mostre a tabela com produtos, estoque, preço e botão `Editar`.
8. Troque para a conta de administrador, se forem mostrar moderação.
9. Clique em `Administração`.
10. Mostre `Aprovação de lojistas`.
11. Role até `Produtos aguardando aprovação`.
12. Mostre os botões `Aprovar` e `Reprovar`.

### Beatriz - modelo de negocio

1. Abra o `Catálogo`.
2. Mostre produtos e filtros enquanto fala do consumidor.
3. Troque para `Painel do Lojista`.
4. Mostre faturamento, repasse, pedidos e estoque enquanto fala do lojista.
5. Troque para `Administração`.
6. Mostre aprovação de lojistas e produtos pendentes enquanto fala da plataforma.
7. Role até `Comissões por categoria`.
8. Mostre os percentuais e o botão `Salvar comissão`.
9. Nao tente mostrar as tres telas ao mesmo tempo. Mostre uma, termine a frase, depois troque.

### Kauanne - Figma

1. Abra o Figma.
2. Mostre a primeira tela do protótipo.
3. Deixe visível a identidade visual e a navegação.
4. Navegue no Figma para uma tela de catálogo ou produtos.
5. Mostre os cards ou a organizacao da tela.
6. Troque para o sistema real.
7. Abra `Catálogo`.
8. Mostre os cards reais e os filtros implementados.
9. Volte ao Figma e mostre uma tela de produto, carrinho ou compra.
10. Troque novamente para o sistema real.
11. Abra a tela equivalente: detalhe do produto ou carrinho.
12. Mostre o que foi implementado de verdade.

### Julio - arquitetura C4

1. Abra `docs/Entrega 3/diagramas/rendered/c1-context.svg`.
2. Deixe o diagrama inteiro visível.
3. Aponte para o bloco central `Beauty Marketplace`.
4. Aponte para `Consumidor`, `Lojista` e `Administrador`.
5. Aponte para os sistemas externos, como pagamento, transportadora e notificacoes.
6. Abra `docs/Entrega 3/diagramas/rendered/c2-container.svg`.
7. Deixe visível `Navegador / Mobile Web` e `Aplicacao ASP.NET Core MVC`.
8. Dê zoom, se precisar, na parte dos bancos.
9. Mostre `MySQL`, `SQLite local`, `MongoDB` e `Redis`.
10. Nao explique todas as setas. Mostre só os blocos principais.

### Matheus - componentes e dados

1. Abra `docs/Entrega 3/diagramas/rendered/c3-component-backend.svg`.
2. Mostre os controllers: `ProdutoController`, `CarrinhoController`, `PedidosController`, `LojistaController` e `AdminController`.
3. Mostre `ASP.NET Identity`.
4. Mostre `ApplicationDbContext`.
5. Mostre `CartService`.
6. Abra `docs/Entrega 3/sql/mysql-schema.sql`.
7. Role até aparecerem nomes de tabelas.
8. Mostre tabelas como `usuarios`, `lojistas`, `produtos`, `pedidos` e `pedido_itens`.
9. Abra `docs/Entrega 3/nosql/mongodb-avaliacoes.json`.
10. Mostre que a avaliacao tem comentario, midia e perfil de beleza.
11. Abra `docs/Entrega 3/nosql/redis-estruturas.md`.
12. Mostre `cart:{usuarioId}` e `ranking:produtos:visualizados`.

### Micael - API documentada

1. Abra `http://localhost:5016/swagger`.
2. Mostre que as rotas comecam com `/api`.
3. Clique em `GET /api/produtos`.
4. Clique em `Try it out`.
5. Clique em `Execute`, se o sistema estiver rodando normal.
6. Role até `Response body`.
7. Mostre o JSON retornado.
8. Feche ou role para o endpoint `POST /api/checkout`.
9. Mostre que ele usa metodo `POST` e tem respostas como `201`, `400` e `401`.
10. Abra `POST /api/ia/recomendacoes`.
11. Mostre os campos `tipoPele`, `tipoCabelo`, `objetivo`, `vegano` e `precoMax`.
12. Abra `docs/Entrega 4/api/openapi.json`.
13. Mostre que o arquivo da API está salvo no repositório.
14. Abra `docs/Entrega 4/api/postman_collection.json`.
15. Mostre que existe uma colecao Postman para testes.

### Wesley - IA

1. Entre como consumidor: `cliente@beautymarket.com`.
2. No menu, clique em `Recomendação IA`.
3. Mostre o formulário vazio.
4. Clique em `Tipo de pele` e escolha `Oleosa`.
5. Clique em `Tipo de cabelo` e escolha `Cacheado`.
6. Clique em `Categoria` e escolha `Cuidados com a Pele` ou `Cabelos`.
7. No campo `Preço máximo`, digite `120`.
8. No campo `Objetivo`, digite `hidratar`.
9. Marque `Priorizar veganos`, se quiser mostrar essa preferencia.
10. Antes de enviar, pare e mostre o formulário preenchido.
11. Clique em `Gerar recomendação`.
12. Aguarde aparecer `Resultado`.
13. Mostre `Resumo`.
14. Mostre `Aviso`.
15. Mostre os cards de produtos.
16. Em um card, mostre imagem, nome, marca, motivo, preço, `Detalhes` e `Adicionar`.
17. Clique em `Detalhes`.
18. Mostre que abriu a página real do produto recomendado.
19. Nao abra arquivo de configuração, chave de API ou `.env`.

### Bryan - repositório e encerramento

1. Abra o repositório GitHub.
2. Mostre o nome do repositório.
3. Mostre a lista de pastas e arquivos.
4. Role no `README.md`.
5. Mostre descrição do projeto.
6. Mostre integrantes.
7. Mostre funcionalidades.
8. Mostre tecnologias.
9. Mostre como executar.
10. Abra a pasta `/docs`.
11. Mostre `Entrega 1`, `Entrega 2`, `Entrega 3`, `Entrega 4` e `Entrega Final`.
12. Abra `Entrega 3` e mostre diagramas e SQL.
13. Volte para `/docs`.
14. Abra `Entrega 4` e mostre `api` e `padroes`.
15. Abra o Kanban.
16. Mostre as colunas e cards, se o acesso permitir.
17. Para encerrar, volte para a capa ou deixe o GitHub aberto.

## Roteiro passo a passo

### 1. Abertura do grupo e do video

**Responsavel:** Eduardo Silva de Negreiros  
**Tempo:** 0:00 - 0:50  
**Objetivo:** apresentar grupo, projeto e ordem do video.

**Tela 1 - Capa do video**

Ação: deixar aberta a primeira página deste PDF ou um slide simples.

Mostrar exatamente: `Beauty Marketplace`, `Grupo 4`, lista de integrantes e, se houver, nome da disciplina.

Nao mostrar ainda: sistema, codigo, GitHub ou Figma. A abertura deve ser limpa.

Fala sugerida: "Ola, somos o Grupo 4, e este e o video de defesa do Beauty Marketplace. Ao longo do semestre, a gente desenvolveu uma plataforma para aproximar consumidores, lojistas e administradores no mercado de beleza. Neste video, vamos mostrar de onde veio a ideia, como pensamos a experiencia, como o sistema foi construido e o que ficou pronto para a entrega final."

Quando trocar de tela: ao terminar a frase "repositório final", trocar para o catálogo do sistema.

Transicao: "Agora o Josue vai mostrar o sistema de forma geral."

### 2. Apresentacao geral do projeto

**Responsavel:** Josue Padetti Correa  
**Tempo:** 0:50 - 2:10  
**Objetivo:** mostrar o que o sistema faz sem entrar em detalhes tecnicos.

**Tela 1 - Página inicial ou catálogo publico**

Ação: abrir `http://localhost:5016` ou clicar em `Catálogo` no menu.

Mostrar exatamente: topo do site com a marca `BeautyMarket`, menu `Início`, menu `Catálogo` e cards de produtos.

Destacar na fala: o sistema e um marketplace de beleza, não apenas uma loja simples.

Fala nesta tela: "Essa e a entrada do Beauty Marketplace. Logo no inicio, o usuario ja consegue navegar pelo catalogo e conhecer produtos de diferentes lojistas."

**Tela 2 - Filtros do catálogo**

Ação: rolar até o painel de filtros.

Mostrar exatamente: campo `Buscar`, filtros `Categoria`, `Tipo de pele`, `Tipo de cabelo`, `Marca`, `Lojista`, `Tom`, `Acabamento`, `Preço min.`, `Preço max.`, `Ordenar` e `Somente veganos`.

Ação opcional: selecionar `Tipo de pele: Oleosa` e clicar em `Aplicar filtros`.

Destacar na fala: filtros de beleza ajudam o consumidor a encontrar produtos mais adequados.

Fala nesta tela: "A gente percebeu que, em beleza, a compra depende muito do perfil da pessoa. Por isso, os filtros vao alem de preco e marca: entram tipo de pele, cabelo, tom, acabamento e tambem a opcao de produtos veganos."

**Tela 3 - Um card de produto**

Ação: rolar até a lista de produtos.

Mostrar exatamente: imagem do produto, categoria, nome, marca, lojista, tags de pele/cabelo/tom, preço e estoque.

Destacar na fala: o card ja entrega informacao para decisao rapida.

Fala nesta tela: "Antes mesmo de abrir o produto, o consumidor ja ve informacoes que ajudam na escolha, como marca, lojista, preco, estoque e algumas caracteristicas de beleza."

**Tela 4 - Detalhes de um produto**

Ação: clicar na imagem do produto. Se o botão `Ver detalhes` estiver aparecendo, ele também pode ser usado.

Mostrar exatamente: imagem grande, nome, marca, lojista, descrição, tipo de pele, tipo de cabelo, composicao, estoque, preço, avaliacoes e produtos recomendados.

Destacar na fala: o sistema mostra produto, confiança e recomendação no mesmo fluxo.

Fala sugerida de fechamento: "No geral, o consumidor consegue pesquisar e comprar com mais seguranca. O lojista tem um espaco para vender e acompanhar seus produtos. E o administrador ajuda a manter a qualidade da plataforma."

Quando trocar de tela: voltar para a área de filtros ou deixar a tela de detalhes aberta para a Gabrielle.

Transicao: "Com essa visao geral, a Gabrielle vai explicar o problema que o projeto resolve."

### 3. Contexto do problema

**Responsavel:** Gabrielle Victoria de Souza Barboza  
**Tempo:** 2:10 - 3:35  
**Objetivo:** explicar a dor do consumidor e do lojista usando telas do sistema.

**Tela 1 - Filtros do catálogo**

Ação: voltar para `Catálogo` e posicionar a tela no painel de filtros.

Mostrar exatamente: os filtros `Tipo de pele`, `Tipo de cabelo`, `Tom`, `Acabamento`, `Marca` e `Somente veganos`.

Destacar na fala: consumidores precisam de orientacao para escolher produtos que combinem com seu perfil.

Fala nesta tela: "O ponto de partida foi uma dificuldade bem comum: comprar produto de beleza exige pesquisa. A pessoa precisa entender se aquilo combina com sua pele, cabelo, tom, objetivo e preferencia de uso."

**Tela 2 - Resultado filtrado**

Ação: aplicar um filtro simples, como `Tipo de pele: Oleosa`, ou mostrar os filtros ativos se ja estiverem aplicados.

Mostrar exatamente: texto de filtros ativos, quantidade de produtos encontrados e produtos retornados.

Destacar na fala: a plataforma reduz a busca manual.

Fala nesta tela: "Com os filtros, a busca deixa de ser generica e fica mais proxima do que a pessoa realmente precisa. Isso evita aquela sensacao de ter que abrir varios sites ate encontrar algo que faca sentido."

**Tela 3 - Detalhes do produto**

Ação: abrir um produto filtrado.

Mostrar exatamente: `Composicao`, `Tipo de pele`, `Tipo de cabelo`, `Estoque`, `Avaliacoes de clientes` e `Produtos recomendados`.

Destacar na fala: informacao e prova social aumentam confiança.

Fala nesta tela: "Na pagina do produto, a pessoa encontra as informacoes principais em um lugar so: uso, composicao, estoque, avaliacoes e produtos relacionados. Isso torna a decisao menos confusa."

**Tela 4 - Evidencia do lojista no produto**

Ação: manter a tela de detalhes e apontar visualmente para "vendido por".

Mostrar exatamente: o nome do lojista no detalhe do produto.

Destacar na fala: o problema também envolve pequenos lojistas que precisam de visibilidade.

Fala sugerida de fechamento: "Do outro lado, muitos lojistas pequenos precisam de uma vitrine digital mais organizada. A proposta do marketplace e ajudar os dois lados: o consumidor encontra melhor, e o vendedor ganha mais exposicao."

Quando trocar de tela: ir para uma tela de administrador ou painel do lojista.

Transicao: "Agora a Daiane vai explicar o impacto social e comunitario dessa proposta."

### 4. Impacto social e comunitario

**Responsavel:** Daiane Jheniffer da Silva Araujo  
**Tempo:** 3:35 - 5:00  
**Objetivo:** mostrar como o projeto apoia lojistas e melhora a confiança para consumidores.

**Tela 1 - Painel do lojista**

Ação: se ja tiver gravado o bloco do lojista, entrar como `lojista@beautymarket.com` e clicar em `Painel do Lojista`.

Mostrar exatamente: cards `Produtos cadastrados`, `Itens vendidos`, `Faturamento`, `Repasse`, `Estoque baixo`, `Sem estoque` e `Aguardando moderação`.

Destacar na fala: apoio operacional para pequenos vendedores.

Fala nesta tela: "O impacto social aparece principalmente no apoio ao pequeno lojista. Em vez de montar um e-commerce do zero, ele encontra um painel pronto para acompanhar produtos, vendas, estoque e repasses."

**Tela 2 - Estoque físico e catálogo**

Ação: rolar até a seção `Estoque físico e catálogo`.

Mostrar exatamente: tabela com produto, categoria, moderação, estoque, preço, campo de atualizar estoque e botão `Editar`.

Destacar na fala: gestão simples ajuda a profissionalizar a operação.

Fala nesta tela: "Essa parte ajuda na rotina do lojista. Ele consegue ver o que tem em estoque, ajustar valores e manter o catalogo mais confiavel para quem vai comprar."

**Tela 3 - Administração, se preferirem mostrar o lado de confiança**

Ação: entrar como administrador e clicar em `Administração`.

Mostrar exatamente: `Governança do marketplace`, cards de lojistas/produtos pendentes e a seção `Aprovação de lojistas`.

Destacar na fala: moderação aumenta confiança da comunidade.

Fala nesta tela: "Do lado da administracao, existe uma camada de cuidado antes de liberar lojistas e produtos. Isso ajuda a criar um ambiente de compra mais confiavel."

**Tela 4 - Produtos aguardando aprovação**

Ação: rolar até `Produtos aguardando aprovação`.

Mostrar exatamente: tabela com produto, categoria, lojista, preço e botões `Aprovar` e `Reprovar`.

Destacar na fala: produto não entra no catálogo publico sem curadoria.

Fala sugerida de fechamento: "Com isso, o projeto conversa com uma necessidade real: facilitar a entrada de vendedores no digital e, ao mesmo tempo, entregar uma experiencia mais clara para o consumidor."

Quando trocar de tela: abrir catálogo ou documento da Entrega 1 para modelo de negocio.

Transicao: "A Beatriz vai mostrar como essa proposta se organiza como modelo de negocio."

### 5. Modelo de negocio e proposta de valor

**Responsavel:** Beatriz Cerqueira Sonoro  
**Tempo:** 5:00 - 6:50  
**Objetivo:** explicar valor para consumidor, lojista e administração, sempre uma tela por vez.

**Tela 1 - Valor para o consumidor**

Ação: abrir o `Catálogo`.

Mostrar exatamente: produtos, filtros e botão de compra/detalhes.

Destacar na fala: descoberta, filtro e compra em um só lugar.

Fala nesta tela: "Para o consumidor, o valor esta em encontrar produtos de beleza em um ambiente mais especializado, com filtros uteis, informacoes claras e uma jornada de compra mais simples."

**Tela 2 - Valor para o lojista**

Ação: trocar para o `Painel do Lojista`.

Mostrar exatamente: metricas de faturamento, repasse, pedidos e estoque.

Destacar na fala: vitrine digital e gestão operacional.

Fala nesta tela: "Para o lojista, a proposta e ter uma vitrine digital pronta, sem precisar resolver sozinho toda a parte de produto, pedido, estoque e repasse."

**Tela 3 - Valor para a plataforma**

Ação: trocar para `Administração`.

Mostrar exatamente: aprovação de lojistas, produtos pendentes e comissões por categoria.

Destacar na fala: governança e comissão.

Fala nesta tela: "Para a administracao da plataforma, o modelo permite acompanhar a operacao, moderar o catalogo e definir comissoes por categoria."

**Tela 4 - Comissões por categoria**

Ação: rolar até a seção `Comissões por categoria`.

Mostrar exatamente: cards de categoria, percentual e botão `Salvar comissão`.

Destacar na fala: a receita pode vir de comissão sobre vendas.

Fala sugerida de fechamento: "No fim, o modelo junta marketplace e gestao. O consumidor compra com mais clareza, o lojista vende com mais estrutura e a plataforma consegue manter regras e qualidade."

Quando trocar de tela: abrir Figma.

Transicao: "Agora a Kauanne vai mostrar o protótipo no Figma e como ele guiou a implementacao."

### 6. Demonstracao do protótipo Figma

**Responsavel:** Kauanne Vitoria Soares Bernardo  
**Tempo:** 6:50 - 9:20  
**Objetivo:** mostrar o planejamento de UX e comparar com a aplicacao, uma tela por vez.

**Tela 1 - Figma na visao geral**

Ação: abrir o link do Figma e dar zoom para a tela principal do protótipo.

Mostrar exatamente: a primeira tela do protótipo, com identidade visual, estrutura de navegação e área principal.

Destacar na fala: o Figma foi usado antes da implementacao para organizar a experiencia.

Fala nesta tela: "Na Entrega 2, o Figma ajudou o grupo a pensar a experiencia antes de partir para o codigo. Aqui da para ver a base visual e como imaginamos a navegacao no comeco."

**Tela 2 - Figma no catálogo ou tela de produtos**

Ação: no Figma, navegar para a tela de catálogo/produtos.

Mostrar exatamente: lista ou cards de produtos, categorias e caminho de navegação.

Destacar na fala: organizacao de descoberta de produtos.

Fala nesta tela: "Desde o prototipo, a descoberta de produtos ja aparecia como uma parte central do projeto."

**Tela 3 - Sistema implementado no catálogo**

Ação: trocar para o navegador com `Catálogo`.

Mostrar exatamente: cards de produtos reais e filtros implementados.

Destacar na fala: a ideia do Figma virou uma tela funcional.

Fala nesta tela: "Na versao implementada, essa ideia virou um catalogo funcional, com produtos reais, filtros de beleza, estoque e informacoes do lojista."

**Tela 4 - Figma em fluxo de compra ou detalhe**

Ação: voltar ao Figma e mostrar uma tela de produto, carrinho ou fluxo de compra.

Mostrar exatamente: a tela do fluxo escolhido, sem tentar mostrar o sistema ao mesmo tempo.

Destacar na fala: o design serviu para pensar a jornada.

Fala nesta tela: "O Figma tambem ajudou a desenhar o caminho do usuario: ver um produto, comparar informacoes e seguir para a compra."

**Tela 5 - Sistema implementado no detalhe ou carrinho**

Ação: trocar para o sistema e abrir a tela equivalente: detalhes do produto ou carrinho.

Mostrar exatamente: se for detalhe, mostrar imagem, preço, especificações e recomendações; se for carrinho, mostrar lojistas, subtotal, comissão e repasse.

Destacar na fala: a implementacao ficou mais completa que o protótipo.

Fala sugerida de fechamento: "A implementacao manteve a ideia principal do prototipo, mas ganhou regras reais do sistema: filtros, estoque, moderacao, carrinho multi-lojista e recomendacoes."

Quando trocar de tela: abrir diagrama C1.

Transicao: "Depois do protótipo, o Julio vai apresentar a arquitetura C4."

### 7. Arquitetura do sistema - Diagramas C4

**Responsavel:** Julio Guedes de Oliveira  
**Tempo:** 9:20 - 11:10  
**Objetivo:** explicar a arquitetura em duas telas principais: C1 e C2.

**Tela 1 - Diagrama C1 Context**

Ação: abrir `docs/Entrega 3/diagramas/rendered/c1-context.svg`.

Mostrar exatamente: o bloco central `Beauty Marketplace`, os usuarios `Consumidor`, `Lojista`, `Administrador` e os sistemas externos.

Destacar na fala: quem usa o sistema e quais integracoes existem ao redor.

Fala nesta tela: "No C1, a gente mostra o Beauty Marketplace dentro do contexto geral. O sistema conversa com consumidor, lojista e administrador, e tambem considera integracoes externas planejadas, como pagamento, entrega, notificacoes e redes sociais."

**Tela 2 - Zoom no centro do C1**

Ação: dar zoom no bloco central do sistema e nos tres usuarios.

Mostrar exatamente: as conexoes entre usuarios e sistema.

Destacar na fala: cada perfil tem responsabilidade diferente.

Fala nesta tela: "Essa divisao por perfil foi importante desde o comeco, porque cada pessoa usa o sistema de um jeito: o consumidor compra, o lojista vende e o administrador cuida da governanca."

**Tela 3 - Diagrama C2 Container**

Ação: abrir `docs/Entrega 3/diagramas/rendered/c2-container.svg`.

Mostrar exatamente: `Navegador / Mobile Web`, `Aplicacao ASP.NET Core MVC`, `MySQL`, `SQLite local`, `MongoDB`, `Redis` e integracoes externas.

Destacar na fala: containers e tecnologias.

Fala nesta tela: "No C2, a gente abre um pouco mais a arquitetura. A interface web conversa com a aplicacao ASP.NET Core MVC, onde ficam os controllers, as regras principais, a autenticacao por perfil e o acesso a dados."

**Tela 4 - Zoom na parte de dados do C2**

Ação: dar zoom na regiao dos bancos de dados.

Mostrar exatamente: MySQL, SQLite local, MongoDB e Redis.

Destacar na fala: MySQL e o modelo oficial, SQLite ajuda na demonstração, MongoDB e Redis foram modelados para usos especificos.

Fala sugerida de fechamento: "Na documentacao, a modelagem oficial considera MySQL. Para a demonstracao local, usamos SQLite porque deixa o projeto mais simples de executar. MongoDB e Redis entram como propostas complementares para avaliacoes flexiveis, carrinho, cache e ranking."

Quando trocar de tela: abrir diagrama C3.

Transicao: "O Matheus vai continuar mostrando os componentes internos e a modelagem de dados."

### 8. Componentes e modelo de dados

**Responsavel:** Matheus Da Silva Reis  
**Tempo:** 11:10 - 13:05  
**Objetivo:** mostrar backend e dados sem virar leitura de codigo longa.

**Tela 1 - Diagrama C3 Component**

Ação: abrir `docs/Entrega 3/diagramas/rendered/c3-component-backend.svg`.

Mostrar exatamente: `ProdutoController`, `CarrinhoController`, `PedidosController`, `LojistaController`, `AdminController`, `ASP.NET Identity`, `ApplicationDbContext`, `CartService` e `MarketplaceSeeder`.

Destacar na fala: componentes principais do backend.

Fala nesta tela: "No C3, a gente mostra como a aplicacao ASP.NET Core MVC foi organizada por dentro. Os controllers separam os fluxos principais: catalogo, carrinho, pedidos, lojista e administracao."

**Tela 2 - Zoom na parte de autenticacao e dados do C3**

Ação: dar zoom em `ASP.NET Identity`, `ApplicationDbContext`, `CartService` e `MarketplaceSeeder`.

Mostrar exatamente: os nomes desses componentes.

Destacar na fala: login por perfis, acesso a dados, carrinho persistido e dados de demonstração.

Fala nesta tela: "Aqui aparecem algumas pecas importantes do backend. O ASP.NET Identity separa os perfis, o ApplicationDbContext centraliza o acesso ao banco, o CartService cuida do carrinho e o seeder cria os dados usados na demonstracao."

**Tela 3 - Script SQL**

Ação: abrir `docs/Entrega 3/sql/mysql-schema.sql`.

Mostrar exatamente: início do arquivo e depois rolar até as tabelas principais.

Destacar na fala: tabelas do dominio.

Fala nesta tela: "O modelo relacional representa a parte transacional do marketplace: usuarios, lojistas, categorias, produtos, pedidos, itens do pedido, avaliacoes, lista de desejos e carrinho persistido."

**Tela 4 - MongoDB**

Ação: abrir `docs/Entrega 3/nosql/mongodb-avaliacoes.json`.

Mostrar exatamente: estrutura de uma avaliacao com comentario, midias, perfil de beleza, status de moderação e compra verificada.

Destacar na fala: avaliacoes podem ter formato mais flexivel.

Fala nesta tela: "O MongoDB foi pensado para avaliacoes mais ricas, porque comentario, foto, video e atributos de beleza podem variar bastante de um produto para outro."

**Tela 5 - Redis**

Ação: abrir `docs/Entrega 3/nosql/redis-estruturas.md`.

Mostrar exatamente: estruturas `cart:{usuarioId}`, `ranking:produtos:visualizados` e `cart:abandoned:{usuarioId}`.

Destacar na fala: cache, carrinho temporario e ranking.

Fala sugerida de fechamento: "Ja o Redis foi pensado para dados rapidos e temporarios, como carrinho com tempo de expiracao, ranking de produtos visualizados e recuperacao de carrinho abandonado."

Quando trocar de tela: abrir Swagger.

Transicao: "Com a arquitetura apresentada, o Micael vai demonstrar a API documentada."

### 9. Demonstracao da API documentada

**Responsavel:** Micael Dantas da Silva  
**Tempo:** 13:05 - 15:35  
**Objetivo:** mostrar que a API está documentada, testável e versionada.

**Tela 1 - Swagger inicial**

Ação: abrir `http://localhost:5016/swagger`.

Mostrar exatamente: titulo da documentacao e lista de endpoints iniciados por `/api`.

Destacar na fala: a documentacao foi filtrada para API, sem misturar telas MVC.

Fala nesta tela: "A API foi documentada com Swagger para facilitar teste e entendimento. Aqui aparecem as rotas `/api`, separadas das telas MVC do sistema."

**Tela 2 - Endpoint GET /api/produtos**

Ação: expandir `GET /api/produtos`.

Mostrar exatamente: parametros de filtro, botão `Try it out`, botão `Execute` e possiveis respostas.

Ação opcional: clicar em `Try it out` e `Execute`.

Destacar na fala: endpoint publico para listar produtos.

Fala nesta tela: "Esse endpoint lista produtos e permite filtros parecidos com os do catalogo visual, so que retornando os dados em formato de API."

**Tela 3 - Resposta do GET /api/produtos**

Ação: se executar o endpoint, rolar até `Response body`.

Mostrar exatamente: JSON retornado com produtos, nome, marca, categoria, preço ou campos equivalentes.

Destacar na fala: a API retorna dados estruturados para consumo externo.

Fala nesta tela: "A resposta vem em JSON, entao outro sistema, aplicativo ou integracao poderia consumir esses dados do catalogo."

**Tela 4 - Endpoint POST /api/checkout**

Ação: recolher o endpoint anterior e expandir `POST /api/checkout`.

Mostrar exatamente: metodo POST, corpo de request e códigos de resposta como `201`, `400` e `401`.

Destacar na fala: checkout exige autenticacao e representa fluxo transacional.

Fala nesta tela: "O checkout fica protegido porque depende de um consumidor logado e de um carrinho valido. Essa regra evita criar pedido sem contexto."

**Tela 5 - Endpoint POST /api/ia/recomendacoes**

Ação: expandir `POST /api/ia/recomendacoes`.

Mostrar exatamente: corpo com `tipoPele`, `tipoCabelo`, `objetivo`, `vegano` e `precoMax`.

Destacar na fala: a mesma funcionalidade de IA pode ser usada pela tela e pela API.

Fala nesta tela: "A recomendacao de IA tambem pode ser chamada pela API. Ela recebe o perfil de beleza e devolve sugestoes estruturadas."

**Tela 6 - OpenAPI versionado**

Ação: abrir `docs/Entrega 4/api/openapi.json`.

Mostrar exatamente: arquivo JSON versionado no repositório, com caminhos `/api`.

Destacar na fala: a documentacao não depende apenas do servidor local.

Fala nesta tela: "Alem do Swagger rodando localmente, salvamos o OpenAPI no repositorio para deixar a documentacao versionada."

**Tela 7 - Postman Collection**

Ação: abrir `docs/Entrega 4/api/postman_collection.json`.

Mostrar exatamente: nome da collection e alguns itens/endpoints.

Destacar na fala: permite testar chamadas fora do Swagger.

Fala sugerida de fechamento: "No total, a entrega documenta 12 operacoes `/api`, acima do minimo exigido, com exemplos, parametros e codigos de resposta."

Quando trocar de tela: abrir sistema logado como consumidor em `Recomendação IA`.

Transicao: "Agora o Wesley vai mostrar a funcionalidade de IA funcionando na interface."

### 10. Demonstracao da funcionalidade de IA

**Responsavel:** Wesley Weber Fernandes  
**Tempo:** 15:35 - 18:20  
**Objetivo:** demonstrar a recomendação por perfil de beleza.

**Tela 1 - Login como consumidor**

Ação: entrar com `cliente@beautymarket.com` e senha `Cliente@123`, se ainda não estiver logado.

Mostrar exatamente: depois do login, menu com `Minha área`, `Recomendação IA`, `Pedidos`, `Lista de Desejos` e `Carrinho`.

Destacar na fala: a funcionalidade aparece para consumidor.

Fala nesta tela: "A recomendacao de IA fica disponivel para o consumidor porque ela ajuda justamente no momento de escolher produtos."

**Tela 2 - Tela Recomendação IA vazia**

Ação: clicar no menu `Recomendação IA`.

Mostrar exatamente: titulo `Recomendação IA`, texto explicativo e formulário.

Destacar na fala: o usuario informa seu perfil de beleza.

Fala nesta tela: "Nesta tela, o usuario informa um pouco do seu perfil para receber sugestoes do catalogo."

**Tela 3 - Preenchimento do formulário**

Ação: preencher os campos nesta ordem:

Tipo de pele: `Oleosa`

Tipo de cabelo: `Cacheado`

Categoria: `Cuidados com a Pele` ou `Cabelos`

Preço máximo: `120`

Objetivo: `hidratar`

Priorizar veganos: marcar se quiser demonstrar a preferencia.

Mostrar exatamente: formulário preenchido antes de enviar.

Destacar na fala: os campos viram entrada para a recomendação.

Fala nesta tela: "Essas informacoes ajudam a personalizar a recomendacao, considerando pele, cabelo, categoria, objetivo, preferencia vegana e faixa de preco."

**Tela 4 - Botão Gerar recomendação**

Ação: clicar em `Gerar recomendação`.

Mostrar exatamente: mensagem `Gerando recomendação...`, se aparecer.

Destacar na fala: o sistema chama o endpoint de IA.

Fala nesta tela: "Quando a gente envia o formulario, a tela chama o endpoint `/api/ia/recomendacoes`."

**Tela 5 - Resultado da IA**

Ação: aguardar os cards aparecerem.

Mostrar exatamente: bloco `Resultado`, `Resumo`, `Aviso`, cards de produtos, imagem, categoria, nome, marca, motivo, preço, botão `Detalhes` e botão `Adicionar`.

Destacar na fala: não e só lista de produto; existe justificativa.

Fala nesta tela: "O retorno traz um resumo, um aviso de compatibilidade e produtos recomendados com justificativa. A ideia e que o consumidor nao receba so uma lista solta, mas entenda por que aqueles itens fazem sentido."

**Tela 6 - Detalhes de um recomendado**

Ação: clicar em `Detalhes` em um card recomendado.

Mostrar exatamente: página do produto recomendado.

Destacar na fala: a IA conecta recomendação com fluxo real de compra.

Fala nesta tela: "A recomendacao nao fica isolada. O usuario pode abrir os detalhes do produto e seguir normalmente para a compra."

**Tela 7 - Explicacao tecnica sem abrir segredo**

Ação: voltar para a tela de resultado ou abrir o Swagger no endpoint `POST /api/ia/recomendacoes`.

Mostrar exatamente: endpoint ou tela de resultado. Nao abrir `appsettings` com chave, `.env` ou variaveis secretas.

Destacar na fala: OpenAI quando configurado e fallback local quando não configurado.

Fala sugerida de fechamento: "Na parte tecnica, usamos uma fabrica para escolher o provedor de IA. Se houver chave da OpenAI configurada, o sistema usa a integracao externa. Se nao houver, ele usa um fallback local, entao a demonstracao continua funcionando."

Quando trocar de tela: abrir GitHub.

Transicao: "Para encerrar, o Bryan vai mostrar o repositório final e os aprendizados do grupo."

### 11. Conclusao, repositório final e aprendizados

**Responsavel:** Bryan Willian da Silva Almeida  
**Tempo:** 18:20 - 20:10  
**Objetivo:** mostrar que a entrega final está organizada.

**Tela 1 - Repositorio GitHub**

Ação: abrir `https://github.com/EduardoSilvaNegreiros/TrabalhoFaculdade5Semestre2026`.

Mostrar exatamente: nome do repositório, lista de arquivos e `README.md` aparecendo na tela.

Destacar na fala: repositório final centraliza codigo e documentacao.

Fala nesta tela: "O repositorio final reune o codigo do sistema e a documentacao que o grupo produziu ao longo do semestre."

**Tela 2 - README da raiz**

Ação: rolar o README.

Mostrar exatamente: descrição do projeto, integrantes, funcionalidades, tecnologias, como executar, contas de demonstração e links principais.

Destacar na fala: o README permite entender e executar o projeto.

Fala nesta tela: "O README foi organizado para quem abrir o projeto entender rapidamente o que ele faz, quem participou, quais tecnologias foram usadas e como executar localmente."

**Tela 3 - Pasta docs**

Ação: abrir a pasta `/docs`.

Mostrar exatamente: `Entrega 1`, `Entrega 2`, `Entrega 3`, `Entrega 4`, `Entrega Final`, `roteiro-apresentacao.md` e materiais de apoio.

Destacar na fala: todas as entregas estão organizadas.

Fala nesta tela: "A pasta `/docs` guarda as entregas anteriores e a entrega final. Ali ficam relatorios, checklists, diagramas, API, SQL e o roteiro do video."

**Tela 4 - Entrega 3 dentro de docs**

Ação: abrir `docs/Entrega 3`.

Mostrar exatamente: relatorio, diagramas, SQL e modelagens NoSQL.

Destacar na fala: arquitetura e dados estão versionados.

Fala nesta tela: "Na Entrega 3 ficam os diagramas C4, os arquivos PlantUML renderizados, o script SQL e as modelagens MongoDB e Redis."

**Tela 5 - Entrega 4 dentro de docs**

Ação: voltar para `/docs` e abrir `Entrega 4`.

Mostrar exatamente: relatorio, pasta `api`, pasta `padroes` e documentos da entrega.

Destacar na fala: GoF, API e IA estão documentados.

Fala nesta tela: "Na Entrega 4 ficam os padroes GoF, a documentacao da API, a colecao Postman e a prova de conceito de IA."

**Tela 6 - Kanban**

Ação: abrir `https://github.com/users/EduardoSilvaNegreiros/projects/2`.

Mostrar exatamente: quadro Kanban com colunas e cards, se estiver acessivel.

Destacar na fala: acompanhamento do trabalho ao longo do semestre.

Fala nesta tela: "O Kanban ajudou o grupo a organizar tarefas e acompanhar a evolucao do projeto durante o semestre."

**Tela 7 - Encerramento**

Ação: voltar para a capa ou deixar o GitHub aberto.

Mostrar exatamente: capa do video ou repositório final.

Fala sugerida: "Para concluir, o Beauty Marketplace reuniu concepcao, UX, arquitetura, desenvolvimento, API e IA em um sistema funcional. O maior aprendizado do grupo foi transformar uma ideia de negocio em uma experiencia navegavel, documentada e demonstravel. Esse foi o Beauty Marketplace, desenvolvido pelo Grupo 4. Obrigado pela atencao."

## Resumo rapido por integrante

| Integrante | Ideia principal | Tela 1 | Tela 2 | Tela 3 |
| --- | --- | --- | --- | --- |
| Eduardo | Abertura | Capa | - | - |
| Josue | Sistema geral | Catálogo | Filtros | Detalhes de produto |
| Gabrielle | Problema | Filtros | Resultado filtrado | Detalhe com avaliacao |
| Daiane | Impacto social | Painel lojista | Estoque | Admin/moderação |
| Beatriz | Modelo de negocio | Catálogo | Painel lojista | Admin/comissões |
| Kauanne | Figma | Figma | Catálogo implementado | Detalhe/carrinho |
| Julio | C4 | C1 | C2 | Zoom nos dados |
| Matheus | Dados | C3 | SQL | MongoDB/Redis |
| Micael | API | Swagger | OpenAPI | Postman |
| Wesley | IA | Formulario IA | Resultado IA | Produto recomendado |
| Bryan | Fechamento | GitHub | Docs | Kanban |

## Ordem final de gravação de tela

1. Capa.
2. Catálogo publico.
3. Filtros do catálogo.
4. Detalhe de produto.
5. Painel do lojista.
6. Estoque do lojista.
7. Administração.
8. Produtos aguardando aprovação.
9. Comissões por categoria.
10. Figma.
11. Catálogo implementado.
12. Carrinho ou detalhe equivalente ao Figma.
13. Diagrama C1.
14. Diagrama C2.
15. Diagrama C3.
16. SQL.
17. MongoDB.
18. Redis.
19. Swagger.
20. OpenAPI.
21. Postman Collection.
22. Login consumidor.
23. Recomendação IA.
24. Resultado da IA.
25. Detalhe do produto recomendado.
26. GitHub.
27. README.
28. Pasta `/docs`.
29. Entrega 3.
30. Entrega 4.
31. Kanban.
32. Capa ou GitHub para encerramento.

## Plano B para problemas durante a gravação

- **Sistema não abriu:** usar prints ou gravacoes curtas das telas e explicar o fluxo esperado.
- **Login atrapalhou:** pausar, entrar com a conta correta e continuar em outro bloco.
- **Swagger não executou:** mostrar a documentacao do endpoint e o arquivo `openapi.json`.
- **IA retornou fallback local:** explicar que isso e esperado quando não existe chave externa configurada.
- **Figma ficou lento:** mostrar a tela principal e depois ir direto para a tela implementada equivalente.
- **Kanban não abriu por permissao:** mostrar o link no README ou na documentacao e explicar que ele foi usado como quadro do projeto.

## Cuidados importantes

- Mostrar uma tela por vez.
- Esperar a tela carregar antes de comecar a falar.
- Nao ficar mexendo o mouse sem necessidade.
- Deixar zoom entre 90% e 110%.
- Nao mostrar chave de API, `.env`, dados sensiveis ou abas pessoais.
- Usar as falas como base, sem ler de forma mecânica.
- Conferir se todos os integrantes falaram.
- Conferir se o video final ficou entre 15 e 25 minutos.

## Checklist final da entrega

- [x] Video gravado com duracao entre 15 e 25 minutos.
- [x] Todos os integrantes aparecem ou participam ativamente na gravação.
- [x] Link do video publicado no YouTube não listado ou Google Drive: `https://youtu.be/8xRP4LWxfyA?is=C3TIzKe_77GF9F3Z`.
- [x] Repositorio GitHub final revisado.
- [x] README.md da raiz atualizado.
- [x] Pasta `/docs` com Entregas 1, 2, 3, 4 e Entrega Final.
- [x] Scripts SQL, diagramas, OpenAPI, Postman e codigo dos protótipos versionados.
- [x] Link final do video e link do GitHub prontos para submissao.
