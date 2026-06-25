# Beauty Marketplace - Entrega 1 - Concepcao

Esta entrega corresponde ao **Bloco 1 - Concepcao** do Projeto Integrador. O material apresenta a definicao inicial do problema, o modelo de negocio, os requisitos, as user stories organizadas para Kanban e a base do repositorio GitHub do projeto.

## Objetivo

Apresentar a concepcao inicial do projeto de extensao, com definicao do problema, modelo de negocio, requisitos, historias de usuario organizadas no Kanban e repositorio GitHub configurado.

## 1. Identificacao do grupo

- Numero do grupo: **4**
- Integrante 1: **Eduardo Silva de Negreiros** - RA **924109760**
- Integrante 2: **Josue Padetti Correa** - RA **924109806**
- Integrante 3: **Gabrielle Victoria de Souza Barboza** - RA **424106162**
- Integrante 4: **Micael Dantas da Silva** - RA **924110378**
- Integrante 5: **Daiane Jheniffer da Silva Araujo** - RA **1726105657**
- Integrante 6: **Beatriz Cerqueira Sonoro** - RA **924106243**
- Integrante 7: **Kauanne Vitoria Soares Bernardo** - RA **924111927**
- Integrante 8: **Julio Guedes de Oliveira** - RA **926107422**
- Integrante 9: **Matheus Da Silva Reis** - RA **926111266**
- Integrante 10: **Wesley Weber Fernandes** - RA **924107330**
- Integrante 11: **Bryan Willian da Silva Almeida** - RA **925116744**

## 2. Definicao do problema e modelo de negocio

### Problema identificado

O mercado de beleza e cuidados pessoais cresce rapidamente, mas a jornada de compra continua fragmentada. O consumidor muitas vezes precisa pesquisar em varios sites, comparar procedencia, lidar com fretes separados e interpretar catalogos pouco adaptados ao contexto real de beleza.

Os principais problemas observados foram:

- dificuldade de reunir diferentes categorias e marcas em uma unica compra;
- falta de confianca sobre procedencia e curadoria em plataformas genericas;
- ausencia de uma experiencia orientada a tipo de pele, cabelo, composicao e preferencias do consumidor brasileiro.

### Publico-alvo

O projeto atende dois grupos principais:

- **Consumidor conectado:** homens e mulheres, em geral entre 18 e 45 anos, que pesquisam produtos online, usam redes sociais como referencia e valorizam praticidade no autocuidado.
- **Lojista parceiro de beleza:** vendedores que precisam de vitrine digital, gestao de catalogo, visibilidade e fluxo comercial organizado dentro de um marketplace especializado.

### Contexto social e comunitario

O Beauty Marketplace se conecta ao contexto de extensao universitaria por atuar em tres frentes:

- **democratizacao do acesso:** amplia a disponibilidade de marcas e produtos para regioes onde a oferta local e mais limitada;
- **diversidade e representatividade:** valoriza filtros e catalogacao para diferentes tons de pele, tipos de cabelo e perfis de consumo;
- **apoio a pequenos lojistas:** fortalece a entrada de vendedores em ambiente digital mais estruturado, com moderacao e organizacao da vitrine.

### Modelo de negocio SaaS / Marketplace

Embora o projeto tenha operacao de marketplace, sua logica de plataforma segue uma visao SaaS ao oferecer um ambiente centralizado para cadastro, gestao e operacao dos lojistas.

#### Business Model Canvas em formato textual

| Bloco | Definicao para o projeto |
| --- | --- |
| Proposta de valor | Centralizar descoberta, recomendacao e compra de produtos de beleza em uma unica plataforma, com catalogo curado, filtros especificos, compra unificada e maior confianca. |
| Segmentos de clientes | Consumidores digitais de produtos de beleza; pequenos e medios lojistas; administracao da plataforma. |
| Canais | Site web, divulgacao em redes sociais, comunidades digitais de beleza, busca organica e campanhas de relacionamento. |
| Relacionamento com clientes | Autoatendimento via plataforma, recomendacoes personalizadas, avaliacoes, historico de pedidos e notificacoes. |
| Fontes de receita | Comissao sobre vendas, possiveis planos de destaque para lojistas e servicos promocionais da plataforma. |
| Recursos principais | Plataforma web, base de produtos, integracao de pagamento, logica de recomendacao, moderacao administrativa e documentacao tecnica. |
| Atividades principais | Curadoria do catalogo, moderacao de lojistas e produtos, manutencao tecnica, gestao da experiencia de compra e documentacao. |
| Parcerias principais | Lojistas parceiros, gateway de pagamento, servicos de frete/rastreio e fornecedores de tecnologia. |
| Estrutura de custos | Hospedagem, manutencao da aplicacao, integracoes externas, suporte, moderacao e divulgacao da plataforma. |

## 3. Requisitos do sistema

### Requisitos funcionais

| Codigo | Descricao |
| --- | --- |
| RF01 | O sistema deve processar pagamentos de um unico carrinho contendo produtos de diferentes lojistas, distribuindo automaticamente valores e comissoes. |
| RF02 | O sistema deve permitir cadastro de novos lojistas, envio de documentacao e configuracao da mini-loja. |
| RF03 | O sistema deve permitir importacao e gestao de produtos com fotos, descricoes, composicao e estoque. |
| RF04 | O sistema deve oferecer busca avancada com filtros de beleza, como tipo de pele, tom, curvatura e produtos veganos. |
| RF05 | O sistema deve fornecer painel exclusivo para o lojista acompanhar vendas, frete e inventario. |
| RF06 | O sistema deve permitir avaliacoes com comentarios, fotos, videos e prova social. |
| RF07 | O sistema deve sugerir produtos complementares com base no perfil e no comportamento do usuario. |
| RF08 | O sistema deve permitir recuperacao de carrinho abandonado por notificacoes ou e-mail. |
| RF09 | O sistema deve calcular automaticamente prazos e precos de frete via integracoes externas. |
| RF10 | O sistema deve disponibilizar historico de pedidos e rastreamento por item, inclusive em compras multi-lojista. |

### Requisitos nao funcionais

| Codigo | Descricao | Justificativa |
| --- | --- | --- |
| RNF01 | Seguranca e protecao de dados alinhadas a LGPD. | A confianca e essencial para consumidor e lojista em ambiente digital. |
| RNF02 | Escalabilidade para suportar aumento expressivo de trafego sem queda de desempenho. | O mercado possui picos sazonais como datas promocionais. |
| RNF03 | Disponibilidade minima alta da plataforma. | Indisponibilidade impacta diretamente vendas e operacao dos lojistas. |
| RNF04 | Tempo de resposta rapido nas paginas do catalogo. | O fluxo de compra depende de navegacao visual fluida. |
| RNF05 | Responsividade com foco mobile first. | A maior parte do consumo online de beleza ocorre em smartphone. |

## 4. User Stories e Kanban

### User Stories

1. Como consumidor, quero filtrar produtos por tipo de pele para encontrar itens mais adequados ao meu tratamento.
2. Como consumidor, quero adicionar itens de diferentes vendedores no mesmo carrinho para fazer um pagamento unificado.
3. Como lojista, quero ter um dashboard de vendas para acompanhar faturamento e desempenho dos meus produtos.
4. Como lojista, quero receber notificacoes de novos pedidos para agilizar separacao e envio.
5. Como consumidor, quero salvar produtos em uma lista de desejos para compra futura.
6. Como administrador, quero aprovar ou reprovar novos lojistas para garantir procedencia e qualidade.
7. Como consumidor, quero visualizar fotos reais nas avaliacoes para ganhar mais seguranca na compra.
8. Como lojista, quero integrar meu estoque com a plataforma para evitar venda sem disponibilidade.
9. Como consumidor, quero receber codigo de rastreio apos o envio para acompanhar meu pedido.
10. Como administrador, quero definir niveis de comissao por categoria para controlar melhor a lucratividade da plataforma.

### Kanban

- GitHub Projects / Kanban: **https://github.com/users/EduardoSilvaNegreiros/projects/2**
- Nome do quadro: **Kanban - Projeto 5 Semestre ADS**
- Estrutura esperada: **A Fazer**, **Em Progresso** e **Concluido**

O Kanban organiza backlog, andamento e entregas do grupo ao longo do semestre, servindo como referencia para distribuicao de tarefas e acompanhamento da evolucao do projeto.

## 5. Repositorio GitHub

- Repositorio publico: **https://github.com/EduardoSilvaNegreiros/TrabalhoFaculdade5Semestre2026**
- README da raiz: atualizado com nome do projeto, descricao, integrantes, instrucoes de uso e links principais.

### Conferencia final de colaboracao

O historico visivel no repositorio/local comprova colaboracao dos integrantes do grupo, seja por nome completo ou por alias de usuario. No fechamento da Entrega Final, o shortlog passou a incluir tambem `necxtdigital-crypto <necxtdigital@gmail.com>`, completando a evidencia de participacao coletiva ao longo do semestre.

### Status final

A conferencia final de commits foi validada diretamente no GitHub antes da submissao da Entrega Final / Entrega 5.

## 6. Criterios de avaliacao atendidos

| Criterio | Peso | Atendimento atual |
| --- | --- | --- |
| Clareza e coerencia na definicao do problema | 20% | O problema, o publico-alvo e o contexto social foram descritos de forma objetiva e coerente com o projeto. |
| Completude e qualidade do Business Model Canvas | 20% | O canvas foi consolidado em formato textual cobrindo proposta de valor, segmentos, canais, receita e custos. |
| Qualidade dos requisitos funcionais e nao funcionais | 20% | O relatorio apresenta 10 RFs e 5 RNFs com justificativa alinhada ao dominio do sistema. |
| Qualidade das User Stories e organizacao do Kanban | 20% | As 10 user stories foram organizadas e o Kanban do GitHub foi referenciado com a estrutura esperada. |
| Configuracao e uso do GitHub | 20% | O repositorio publico e o README foram organizados, e o historico de commits foi conferido no GitHub com evidencia de colaboracao dos integrantes do grupo. |
