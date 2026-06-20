# Beauty Marketplace - Roteiro detalhado do Video de Defesa

Documento de apoio para gravar a defesa final do **Beauty Marketplace**. A proposta deste roteiro e deixar as falas simples, objetivas e, principalmente, deixar claro **o que deve aparecer na tela enquanto cada integrante fala**.

## Dados gerais

- Grupo: **4**
- Projeto: **Beauty Marketplace**
- Duracao alvo: **18 a 19 minutos**
- Duracao permitida pela entrega: **15 a 25 minutos**
- Formato: video de defesa final com todos os integrantes participando

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

> Se a ordem real de fala mudar, mantenham a mesma estrutura do roteiro: todos precisam aparecer e falar uma parte objetiva.

## Preparacao antes de gravar

### Abas e arquivos para deixar abertos

1. **Capa da apresentacao ou primeira pagina deste PDF**
   - Usar para a abertura.
   - Deve mostrar: nome do projeto, grupo e integrantes.

2. **Sistema web**
   - URL local: `http://localhost:5016`
   - Usar para mostrar catalogo, filtros, detalhes de produto, carrinho, painel do lojista, area administrativa e IA.

3. **Figma**
   - Link: `https://www.figma.com/make/a9aNk5P93AZZCU46FWkMF5/Prototipo-de-site-de-beleza?code-node-id=0-9&p=f&t=VmBFOeUkHWlRkrfj-0&fullscreen=1`
   - Usar no bloco de prototipo.

4. **Diagramas C4 renderizados**
   - `docs/Entrega 3/diagramas/rendered/c1-context.svg`
   - `docs/Entrega 3/diagramas/rendered/c2-container.svg`
   - `docs/Entrega 3/diagramas/rendered/c3-component-backend.svg`

5. **Modelagem de dados**
   - `docs/Entrega 3/sql/mysql-schema.sql`
   - `docs/Entrega 3/nosql/mongodb-avaliacoes.json`
   - `docs/Entrega 3/nosql/redis-estruturas.md`

6. **Swagger / API**
   - URL local: `http://localhost:5016/swagger`
   - Arquivo versionado: `docs/Entrega 4/api/openapi.json`
   - Colecao Postman: `docs/Entrega 4/api/postman_collection.json`

7. **Repositorio GitHub**
   - Repositorio: `https://github.com/EduardoSilvaNegreiros/TrabalhoFaculdade5Semestre2026`
   - Kanban: `https://github.com/users/EduardoSilvaNegreiros/projects/2`

### Contas de demonstracao

- Consumidor: `cliente@beautymarket.com` / `Cliente@123`
- Lojista: `lojista@beautymarket.com` / `Lojista@123`
- Administrador: `admin@beautymarket.com` / `Admin@123`

### Dica pratica para nao se perder nos logins

Como o sistema usa login por cookie, o mais seguro e gravar em blocos. Exemplo:

- Bloco 1: telas publicas, Figma, documentacao, diagramas e Swagger.
- Bloco 2: consumidor logado para carrinho, pedidos e IA.
- Bloco 3: lojista logado para dashboard e produtos.
- Bloco 4: administrador logado para moderacao.

Depois, basta juntar os blocos na edicao. Isso evita trocar de conta toda hora durante a gravacao.

## Visao geral do tempo

| Tempo | Integrante | Tema | Obrigatorio da entrega |
| --- | --- | --- | --- |
| 0:00 - 0:50 | Eduardo | Abertura do grupo | a) Apresentacao do grupo |
| 0:50 - 1:50 | Josue | Apresentacao do projeto | a) Apresentacao do projeto |
| 1:50 - 3:10 | Gabrielle | Problema | b) Contexto do problema |
| 3:10 - 4:30 | Daiane | Impacto social | b) Impacto social/comunitario |
| 4:30 - 6:15 | Beatriz | Modelo de negocio | c) Modelo de negocio e proposta de valor |
| 6:15 - 8:35 | Kauanne | Prototipo Figma | d) Demonstracao do Figma |
| 8:35 - 10:20 | Julio | Arquitetura C4 | e) Diagramas C4 |
| 10:20 - 12:20 | Matheus | Modelo de dados | e) Modelo de dados |
| 12:20 - 14:45 | Micael | API documentada | f) Demonstracao da API |
| 14:45 - 17:10 | Wesley | Funcionalidade de IA | g) Demonstracao da IA |
| 17:10 - 18:50 | Bryan | Repositorio e conclusao | h) Conclusao e aprendizados |

## Roteiro detalhado

### 1. Abertura do grupo e do video

**Responsavel:** Eduardo Silva de Negreiros  
**Tempo:** 0:00 - 0:50  
**Objetivo:** dizer quem e o grupo, qual e o projeto e o que sera apresentado.

**O que mostrar no fundo**

- Mostrar uma capa simples com:
  - `Beauty Marketplace`
  - `Grupo 4`
  - nome dos integrantes
  - disciplina/projeto integrador, se quiserem incluir
- Nao precisa mostrar o sistema ainda.
- Se todos forem aparecer por camera, deixar a capa compartilhada e as cameras ligadas.

**Fala sugerida**

"Ola, nos somos o Grupo 4 e este e o video de defesa do projeto Beauty Marketplace. O nosso sistema e um marketplace de produtos de beleza, pensado para conectar consumidores, lojistas e administradores em uma unica plataforma. Neste video vamos mostrar o problema que motivou o projeto, a proposta de valor, o prototipo no Figma, a arquitetura, a modelagem de dados, a API documentada, a funcionalidade de IA e a organizacao final do repositorio."

**Transicao**

"Agora o Josue vai apresentar, de forma geral, o que o sistema faz."

### 2. Apresentacao geral do projeto

**Responsavel:** Josue Padetti Correa  
**Tempo:** 0:50 - 1:50  
**Objetivo:** explicar o produto de forma rapida e visual.

**O que mostrar no fundo**

- Abrir o sistema em `http://localhost:5016`.
- Mostrar a pagina inicial ou catalogo publico.
- Rolar um pouco a pagina para aparecerem produtos reais.
- Mostrar rapidamente os filtros de categoria, tipo de pele, tipo de cabelo, marca, preco e produto vegano.
- Se possivel, abrir um produto e mostrar nome, imagem, preco, categoria, avaliacao e lojista.

**Fala sugerida**

"O Beauty Marketplace funciona como uma plataforma para compra e venda de produtos de beleza. O consumidor consegue navegar pelo catalogo, filtrar produtos de acordo com o seu perfil, adicionar itens ao carrinho e finalizar uma compra. O lojista tem uma area propria para cadastrar produtos, acompanhar pedidos e controlar estoque. Ja o administrador faz a moderacao de lojistas e produtos, ajudando a manter o catalogo mais confiavel. A ideia e reunir em um so lugar descoberta, compra, gestao e curadoria."

**Transicao**

"Com essa visao geral, a Gabrielle vai explicar qual problema o projeto busca resolver."

### 3. Contexto do problema

**Responsavel:** Gabrielle Victoria de Souza Barboza  
**Tempo:** 1:50 - 3:10  
**Objetivo:** mostrar a dor do consumidor e do lojista.

**O que mostrar no fundo**

- Manter o catalogo aberto.
- Destacar visualmente os filtros do catalogo.
- Abrir uma tela de detalhes de produto.
- Se quiserem reforcar com documento, abrir rapidamente `docs/Entrega 1/relatorio-entrega-1.pdf` na parte de problema, mas nao ficar lendo o PDF.
- O ideal e usar o proprio sistema para representar o problema.

**Fala sugerida**

"O problema que observamos e que a compra de produtos de beleza costuma ser fragmentada. O consumidor muitas vezes pesquisa em varios sites, compara marcas, tenta entender se o produto combina com seu tipo de pele ou cabelo e ainda precisa lidar com fretes separados. Ao mesmo tempo, pequenos lojistas encontram dificuldade para vender online com estrutura, visibilidade e confianca. Por isso, o projeto busca organizar essa jornada em uma plataforma especializada, com filtros de beleza, produtos aprovados e fluxos separados para consumidor, lojista e administrador."

**Transicao**

"A Daiane vai complementar mostrando o impacto social e comunitario da proposta."

### 4. Impacto social e comunitario

**Responsavel:** Daiane Jheniffer da Silva Araujo  
**Tempo:** 3:10 - 4:30  
**Objetivo:** conectar o projeto com extensao universitaria e beneficio para a comunidade.

**O que mostrar no fundo**

- Mostrar a area administrativa, se possivel:
  - aprovacao/reprovacao de lojistas;
  - moderacao de produtos pendentes;
  - comissoes por categoria.
- Se nao quiser trocar login agora, mostrar o catalogo com produtos aprovados e explicar que existe moderacao por administrador.
- Tambem pode mostrar o painel do lojista para reforcar apoio ao pequeno vendedor.

**Fala sugerida**

"O impacto social do Beauty Marketplace esta principalmente no apoio a pequenos e medios lojistas de beleza. A plataforma oferece uma vitrine digital mais organizada, sem exigir que cada vendedor construa um e-commerce proprio do zero. Para o consumidor, o impacto esta em facilitar o acesso a produtos, informacoes e filtros mais proximos da sua realidade. A moderacao de lojistas e produtos tambem ajuda a aumentar a confianca, porque nem tudo entra diretamente no catalogo publico sem validacao."

**Transicao**

"A Beatriz vai apresentar o modelo de negocio e a proposta de valor do marketplace."

### 5. Modelo de negocio e proposta de valor

**Responsavel:** Beatriz Cerqueira Sonoro  
**Tempo:** 4:30 - 6:15  
**Objetivo:** explicar como o projeto gera valor para consumidor, lojista e plataforma.

**O que mostrar no fundo**

- Mostrar tres telas, uma por vez:
  1. Catalogo publico ou tela de consumidor.
  2. Painel do lojista.
  3. Area administrativa.
- Se preferirem usar documento, abrir a parte do Business Model Canvas em `docs/Entrega 1/relatorio-entrega-1.pdf`.
- Nao precisa ler todos os blocos do canvas; mostrar apenas a ideia central.

**Fala sugerida**

"O modelo de negocio segue a logica de marketplace. A plataforma conecta consumidores e lojistas, centralizando catalogo, compra, recomendacao e gestao. Para o consumidor, a proposta de valor e encontrar produtos de beleza em um ambiente mais organizado, com filtros, recomendacoes e compra unificada. Para o lojista, o valor esta em ter uma vitrine digital, painel de gestao, controle de produtos e acompanhamento de pedidos. Para a administracao da plataforma, o sistema permite moderar o catalogo, definir comissoes por categoria e manter a qualidade da operacao."

**Transicao**

"Agora a Kauanne vai mostrar como o prototipo no Figma ajudou a guiar a experiencia do usuario."

### 6. Demonstracao do prototipo Figma

**Responsavel:** Kauanne Vitoria Soares Bernardo  
**Tempo:** 6:15 - 8:35  
**Objetivo:** mostrar que o design foi planejado antes da implementacao.

**O que mostrar no fundo**

- Abrir o Figma no link do prototipo.
- Mostrar a tela inicial/prototipo principal.
- Mostrar rapidamente:
  - tela de catalogo;
  - tela de produto;
  - carrinho ou fluxo de compra;
  - alguma tela relacionada ao lojista, se existir no prototipo.
- Depois trocar para o sistema implementado e mostrar a tela equivalente.
- A comparacao ideal e: "isso foi planejado no Figma" -> "isso virou tela funcional no sistema".

**Fala sugerida**

"Na Entrega 2, trabalhamos a parte de design e experiencia do usuario. O Figma serviu para organizar visualmente as principais telas e validar a navegacao antes do desenvolvimento. Aqui conseguimos ver a ideia do catalogo, da visualizacao de produtos e do fluxo de compra. Na implementacao final, algumas telas evoluiram, mas a logica principal foi mantida: facilitar a descoberta de produtos, deixar a navegacao mais clara e separar melhor os fluxos de consumidor, lojista e administrador."

**Transicao**

"Depois do prototipo, o Julio vai apresentar a arquitetura do sistema usando os diagramas C4."

### 7. Arquitetura do sistema - Diagramas C4

**Responsavel:** Julio Guedes de Oliveira  
**Tempo:** 8:35 - 10:20  
**Objetivo:** explicar a arquitetura sem entrar em detalhes excessivos.

**O que mostrar no fundo**

- Abrir `docs/Entrega 3/diagramas/rendered/c1-context.svg`.
  - Dar zoom para aparecer Consumidor, Lojista, Administrador e sistemas externos.
- Depois abrir `docs/Entrega 3/diagramas/rendered/c2-container.svg`.
  - Mostrar aplicacao ASP.NET Core MVC, banco relacional, SQLite local, MongoDB, Redis e integracoes externas.
- Nao precisa explicar cada seta. Escolher as principais.

**Fala sugerida**

"A arquitetura foi documentada com o modelo C4. No C1, mostramos o Beauty Marketplace dentro do seu contexto: consumidor, lojista, administrador e integracoes externas, como pagamento, entrega e notificacoes. No C2, detalhamos os principais containers. A aplicacao foi desenvolvida em ASP.NET Core MVC, com interface web, regras de negocio, autenticacao por perfis e acesso a dados. A modelagem oficial considera MySQL, mas o prototipo roda localmente com SQLite para facilitar a demonstracao. Tambem modelamos MongoDB e Redis para cenarios especificos."

**Transicao**

"O Matheus vai continuar a arquitetura mostrando os componentes internos e o modelo de dados."

### 8. Componentes e modelo de dados

**Responsavel:** Matheus Da Silva Reis  
**Tempo:** 10:20 - 12:20  
**Objetivo:** mostrar como o backend e os dados foram organizados.

**O que mostrar no fundo**

- Abrir `docs/Entrega 3/diagramas/rendered/c3-component-backend.svg`.
  - Destacar controllers: Produto, Carrinho, Pedidos, Lojista e Admin.
  - Destacar Identity, ApplicationDbContext, CartService e MarketplaceSeeder.
- Abrir `docs/Entrega 3/sql/mysql-schema.sql`.
  - Mostrar as tabelas principais: usuarios, lojistas, produtos, pedidos, pedido_itens, avaliacoes, lista_desejos_itens e carrinho_persistido_itens.
- Abrir rapidamente:
  - `docs/Entrega 3/nosql/mongodb-avaliacoes.json`
  - `docs/Entrega 3/nosql/redis-estruturas.md`

**Fala sugerida**

"No C3, detalhamos os componentes principais do backend. Os controllers organizam os fluxos do sistema, como catalogo, carrinho, pedidos, area do lojista e administracao. O ASP.NET Identity controla login e perfis, enquanto o ApplicationDbContext centraliza o acesso ao banco. Na modelagem relacional, o MySQL representa as entidades principais do marketplace: usuarios, lojistas, produtos, pedidos, itens de pedido, avaliacoes, lista de desejos e carrinho persistido. Tambem documentamos MongoDB para avaliacoes mais flexiveis e Redis para cenarios de carrinho, cache e ranking."

**Transicao**

"Com a arquitetura apresentada, o Micael vai mostrar a API documentada do sistema."

### 9. Demonstracao da API documentada

**Responsavel:** Micael Dantas da Silva  
**Tempo:** 12:20 - 14:45  
**Objetivo:** mostrar que a API existe, esta documentada e possui exemplos.

**O que mostrar no fundo**

- Abrir `http://localhost:5016/swagger`.
- Mostrar que aparecem apenas rotas `/api`.
- Expandir pelo menos estes endpoints:
  - `GET /api/produtos`
  - `GET /api/produtos/{id}`
  - `POST /api/carrinho/itens`
  - `POST /api/checkout`
  - `POST /api/ia/recomendacoes`
- Se estiver seguro, executar `GET /api/produtos` no Swagger.
- Abrir rapidamente `docs/Entrega 4/api/openapi.json` para mostrar que o OpenAPI esta versionado.
- Abrir rapidamente `docs/Entrega 4/api/postman_collection.json` para mostrar a colecao Postman.

**Fala sugerida**

"A API foi documentada com Swagger e OpenAPI. Ela possui 12 operacoes iniciadas por `/api`, acima do minimo pedido na entrega. Aqui conseguimos ver endpoints para produtos, avaliacoes, recomendacoes, carrinho, checkout, pedidos e IA. A documentacao mostra metodo HTTP, parametros, exemplos de entrada e codigos de resposta. Alem do Swagger local, o projeto tambem versiona o arquivo OpenAPI e a colecao Postman dentro da pasta da Entrega 4. Isso facilita testar e revisar a API sem depender apenas da interface visual do sistema."

**Transicao**

"Agora o Wesley vai demonstrar a funcionalidade de IA aplicada ao marketplace."

### 10. Demonstracao da funcionalidade de IA

**Responsavel:** Wesley Weber Fernandes  
**Tempo:** 14:45 - 17:10  
**Objetivo:** mostrar a IA funcionando e explicar por que ela faz sentido no projeto.

**O que mostrar no fundo**

- Entrar como consumidor:
  - `cliente@beautymarket.com`
  - `Cliente@123`
- Abrir a tela `Consumidor > Recomendacao IA`.
- Preencher um exemplo simples:
  - tipo de pele: `Oleosa`
  - tipo de cabelo: `Cacheado`
  - objetivo: `hidratar`
  - produto vegano: `sim`, se existir a opcao
  - preco maximo: `120`
- Gerar a recomendacao e mostrar:
  - resumo;
  - alerta de compatibilidade;
  - cards de produtos;
  - motivo da recomendacao;
  - botao de detalhes ou carrinho.
- Depois, se der tempo, mostrar no Swagger o endpoint `POST /api/ia/recomendacoes`.
- Nao abrir chave de API, `appsettings` com segredo ou variavel sensivel.

**Fala sugerida**

"A funcionalidade de IA foi pensada para ajudar o consumidor a escolher produtos de beleza de acordo com seu perfil. O usuario informa dados como tipo de pele, tipo de cabelo, objetivo, preferencia por produto vegano e faixa de preco. A partir disso, o sistema retorna recomendacoes com justificativa, imagem, preco e link para detalhes. Tecnicamente, a arquitetura usa uma fabrica para escolher o provedor: se houver chave da OpenAI configurada, o sistema usa a integracao externa; se nao houver, usa um fallback local demonstrativo. Assim, a funcionalidade continua funcionando durante a apresentacao sem depender de credencial externa."

**Transicao**

"Para encerrar, o Bryan vai mostrar a organizacao final do repositorio e os aprendizados do grupo."

### 11. Conclusao, repositorio final e aprendizados

**Responsavel:** Bryan Willian da Silva Almeida  
**Tempo:** 17:10 - 18:50  
**Objetivo:** fechar o video mostrando que a entrega final esta organizada.

**O que mostrar no fundo**

- Abrir o repositorio GitHub.
- Mostrar o `README.md` da raiz com:
  - descricao do projeto;
  - integrantes;
  - funcionalidades;
  - tecnologias;
  - como executar;
  - links principais.
- Abrir a pasta `/docs` e mostrar:
  - Entrega 1;
  - Entrega 2;
  - Entrega 3;
  - Entrega 4;
  - Entrega Final.
- Mostrar rapidamente o Kanban/GitHub Projects.
- Se quiser reforcar qualidade tecnica, mencionar que o projeto possui testes automatizados.

**Fala sugerida**

"Como conclusao, o Beauty Marketplace reuniu as entregas do semestre em um unico projeto: concepcao, UX, arquitetura, desenvolvimento, API e IA. O repositorio final esta organizado com README atualizado, pasta `/docs`, relatorios, diagramas, scripts SQL, OpenAPI, Postman e roteiro de defesa. O principal aprendizado do grupo foi integrar visao de negocio, experiencia do usuario e arquitetura tecnica em um sistema funcional. Tambem aprendemos a separar responsabilidades por perfil, documentar APIs, aplicar padroes de projeto e demonstrar uma funcionalidade de IA conectada ao problema real do marketplace."

**Frase final**

"Esse foi o Beauty Marketplace, desenvolvido pelo Grupo 4. Obrigado pela atencao."

## Resumo rapido para decorar

| Integrante | Ideia principal da fala | Tela de fundo principal |
| --- | --- | --- |
| Eduardo | Quem somos e o que sera apresentado | Capa do video/PDF |
| Josue | O que e o Beauty Marketplace | Catalogo publico |
| Gabrielle | Problema do consumidor e do lojista | Filtros e detalhes de produto |
| Daiane | Impacto social e apoio a pequenos lojistas | Admin, moderacao ou catalogo aprovado |
| Beatriz | Marketplace, valor para consumidor, lojista e plataforma | Catalogo, painel do lojista e admin |
| Kauanne | Figma guiou a UX e evoluiu para o sistema | Figma + telas implementadas |
| Julio | C1 e C2 explicam contexto e containers | Diagramas C1 e C2 |
| Matheus | C3 e dados mostram organizacao tecnica | C3, SQL, MongoDB e Redis |
| Micael | API tem Swagger, OpenAPI e Postman | Swagger e arquivos de API |
| Wesley | IA recomenda produtos por perfil de beleza | Tela Recomendacao IA |
| Bryan | Repositorio esta completo e aprendizados | GitHub, `/docs` e Kanban |

## Ordem sugerida para gravar a tela

1. Capa do video.
2. Sistema em `http://localhost:5016`.
3. Catalogo publico e filtros.
4. Detalhes de um produto.
5. Area administrativa ou painel do lojista para impacto.
6. Figma.
7. Tela implementada equivalente ao Figma.
8. Diagramas C1, C2 e C3.
9. Script SQL, MongoDB e Redis.
10. Swagger.
11. OpenAPI e Postman.
12. Tela de Recomendacao IA.
13. GitHub, pasta `/docs` e Kanban.

## Plano B para problemas durante a gravacao

- **Se o sistema nao abrir:** mostrar as telas ja gravadas anteriormente ou prints, e explicar rapidamente o fluxo esperado.
- **Se o Swagger nao executar uma chamada:** mostrar a documentacao dos endpoints e o arquivo `openapi.json`.
- **Se a IA usar fallback local:** isso faz parte da arquitetura; explicar que o fallback foi criado para manter a demonstracao funcionando sem chave externa.
- **Se o Figma estiver lento:** mostrar o link no relatorio da Entrega 2 e comparar com as telas implementadas.
- **Se algum login atrapalhar:** pausar a gravacao, entrar com a conta certa e continuar em outro bloco.

## Cuidados importantes

- Todos os integrantes devem falar.
- Evitar ler o texto palavra por palavra; usar como base.
- Manter a tela limpa, sem abas pessoais abertas.
- Nao mostrar chave de API, variaveis secretas, `.env` ou dados sensiveis.
- Deixar o zoom do navegador entre 90% e 110%, para a banca conseguir enxergar.
- Falar devagar o suficiente para a tela acompanhar a explicacao.
- Mostrar sempre algo visual enquanto alguem fala.
- Conferir se o video final ficou entre 15 e 25 minutos.

## Checklist final da entrega

- [ ] Video gravado com duracao entre 15 e 25 minutos.
- [ ] Todos os integrantes aparecem ou participam ativamente na gravacao.
- [ ] Link do video publicado no YouTube nao listado ou Google Drive.
- [ ] Repositorio GitHub final revisado.
- [ ] README.md da raiz atualizado.
- [ ] Pasta `/docs` com Entregas 1, 2, 3, 4 e Entrega Final.
- [ ] Scripts SQL, diagramas, OpenAPI, Postman e codigo dos prototipos versionados.
- [ ] Link final do video e link do GitHub prontos para submissao.
