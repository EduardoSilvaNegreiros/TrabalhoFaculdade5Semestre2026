# Beauty Marketplace - Entrega Final - Roteiro do Video de Defesa

## Dados gerais

- Grupo: **4**
- Projeto: **Beauty Marketplace**
- Duracao sugerida: **17 a 19 minutos**
- Formato: **video de defesa final**

## Integrantes

1. **Eduardo Silva de Negreiros** - RA **924109760**
2. **Josue Padetti Correa** - RA **924109806**
3. **Gabrielle Victoria de Souza Barboza** - RA **424106162**
4. **Micael Dantas da Silva** - RA **924110378**
5. **Daiane Jheniffer da Silva Araujo** - RA **1726105657**
6. **Beatriz Cerqueira Sonoro** - RA **924106243**
7. **Kauanne Vitoria Soares Bernardo** - RA **924111927**
8. **Julio Guedes de Oliveira** - RA **926107422**
9. **Matheus Da Silva Reis** - RA **926111266**
10. **Wesley Weber Fernandes** - RA **924107330**
11. **Bryan Willian da Silva Almeida** - RA **925116744**

## Objetivo do video

Consolidar todo o trabalho desenvolvido no semestre em um video de defesa profissional, demonstrando o sistema funcionando, a documentacao das entregas, a API, a funcionalidade de IA e a coerencia tecnica do projeto.

## Contas de demonstracao

- Consumidor: `cliente@beautymarket.com` / `Cliente@123`
- Lojista: `lojista@beautymarket.com` / `Lojista@123`
- Administrador: `admin@beautymarket.com` / `Admin@123`

## Estrutura do roteiro

| Tempo | Integrante | Parte | O que mostrar | Fala base |
| --- | --- | --- | --- | --- |
| 0:00 - 0:45 | Eduardo Silva de Negreiros | Abertura | Slide inicial com Grupo 4, nome do projeto e lista dos integrantes. | "Ola, nos somos o Grupo 4 e este e o Beauty Marketplace. Neste video vamos apresentar o problema, a proposta do projeto, o prototipo, a arquitetura, a API, a funcionalidade de IA e o repositorio final." |
| 0:45 - 1:40 | Josue Padetti Correa | Apresentacao do projeto | Catalogo publico do sistema. | "O Beauty Marketplace e um marketplace de produtos de beleza com multiplos perfis de acesso: consumidor, lojista e administrador, cada um com responsabilidades diferentes dentro da plataforma." |
| 1:40 - 2:50 | Gabrielle Victoria de Souza Barboza | Contexto do problema | Slide com dores do problema + sistema ao fundo. | "Nosso projeto responde a dificuldade de pequenos lojistas venderem em um ambiente digital organizado e a dificuldade do consumidor em encontrar produtos adequados ao proprio perfil de beleza." |
| 2:50 - 4:00 | Daiane Jheniffer da Silva Araujo | Impacto social/comunitario | Slide de impacto social + area administrativa e catalogo aprovado. | "O impacto social do projeto esta em apoiar pequenos vendedores, aumentar visibilidade e oferecer ao consumidor uma experiencia com mais confianca, curadoria, moderacao e acesso a informacao." |
| 4:00 - 5:30 | Beatriz Cerqueira Sonoro | Modelo de negocio e proposta de valor | Slide com proposta de valor + telas dos tres perfis. | "Nossa proposta de valor e centralizar descoberta, recomendacao e compra de produtos de beleza em um unico ambiente, com compra unificada para o consumidor e gestao estruturada para o lojista e para a administracao." |
| 5:30 - 7:40 | Kauanne Vitoria Soares Bernardo | Prototipo Figma | Figma + comparacao com telas implementadas. | "O prototipo em Figma definiu a estrutura de navegacao e a experiencia principal do usuario. Aqui mostramos como a implementacao final manteve a logica planejada e evoluiu para uma versao funcional." |
| 7:40 - 9:20 | Julio Guedes de Oliveira | Arquitetura C4 | `relatorio-entrega-3.pdf` + diagramas C1 e C2. | "Na arquitetura, comecamos pelo modelo C4. O C1 mostra o sistema no contexto geral e o C2 detalha os principais containers, incluindo aplicacao web, dados e integracoes externas." |
| 9:20 - 11:20 | Matheus Da Silva Reis | Componentes e modelo de dados | C3 + `mysql-schema.sql` + arquivos MongoDB e Redis. | "No C3 detalhamos o backend da aplicacao. Na modelagem de dados, usamos MySQL como modelo relacional oficial da entrega, alem de MongoDB e Redis para cenarios especificos do dominio." |
| 11:20 - 13:40 | Micael Dantas da Silva | API documentada | Swagger, OpenAPI e Postman. | "Na parte de APIs, documentamos 12 operacoes `/api`, acima do minimo exigido, com metodos HTTP, parametros, exemplos, respostas e codigos de status." |
| 13:40 - 16:00 | Wesley Weber Fernandes | Funcionalidade de IA | Tela `Consumidor > Recomendacao IA` + endpoint `POST /api/ia/recomendacoes`. | "Na funcionalidade de IA, o usuario informa seu perfil de beleza e recebe recomendacoes de produtos com justificativa. A arquitetura usa OpenAI quando ha chave configurada e fallback local quando nao ha." |
| 16:00 - 17:30 | Bryan Willian da Silva Almeida | Conclusao e aprendizados | GitHub final + pasta `/docs` + GitHub Projects/Kanban. | "Como conclusao, o projeto reuniu requisitos de negocio, arquitetura, desenvolvimento, API e IA em um repositorio organizado, com aprendizado forte em separacao por perfis, documentacao tecnica e integracao de funcionalidades." |

## O que abrir antes de gravar

1. Sistema rodando localmente no navegador.
2. Figma aberto na tela principal do prototipo.
3. `docs/Entrega 3/relatorio-entrega-3.pdf`.
4. `docs/Entrega 4/relatorio-entrega-4.pdf`.
5. `http://localhost:5016/swagger`.
6. Repositorio GitHub final.
7. GitHub Projects/Kanban.

## Ordem pratica da demonstracao

1. Slide inicial com grupo e nome do projeto.
2. Catalogo publico com filtros.
3. Explicacao do problema, impacto social e proposta de valor.
4. Prototipo Figma e comparacao com a aplicacao.
5. Entrega 3: C1, C2, C3 e modelagem de dados.
6. Entrega 4: GoF, Swagger, Postman e IA.
7. GitHub final, pasta `docs` e Kanban.
8. Encerramento com aprendizados.

## Pontos importantes durante a gravacao

- Todos os integrantes devem aparecer e falar.
- Evitar ler tudo mecanicamente; usar as falas como base.
- Nao mostrar chave de API, `.env` ou dados sensiveis.
- Mostrar sempre algo visual enquanto alguem fala.
- Priorizar fala objetiva e tela limpa.
- Se houver erro ao vivo, manter a gravacao e explicar rapidamente o fluxo esperado.

## Frase de encerramento sugerida

"Esse foi o Beauty Marketplace, desenvolvido pelo Grupo 4. Obrigado pela atencao."
