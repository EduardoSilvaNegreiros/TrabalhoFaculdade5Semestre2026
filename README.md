# Beauty Marketplace

Marketplace web de produtos de beleza com multiplos perfis de acesso, compra unificada entre lojistas, moderacao administrativa, recomendacao de produtos e documentacao academica das entregas do Projeto Integrador.

## Integrantes - Grupo 4

1. Eduardo Silva de Negreiros - RA 924109760
2. Josue Padetti Correa - RA 924109806
3. Gabrielle Victoria de Souza Barboza - RA 424106162
4. Micael Dantas da Silva - RA 924110378
5. Daiane Jheniffer da Silva Araujo - RA 1726105657
6. Beatriz Cerqueira Sonoro - RA 924106243
7. Kauanne Vitoria Soares Bernardo - RA 924111927
8. Julio Guedes de Oliveira - RA 926107422
9. Matheus Da Silva Reis - RA 926111266
10. Wesley Weber Fernandes - RA 924107330
11. Bryan Willian da Silva Almeida - RA 925116744

## Principais funcionalidades

- Catalogo publico com filtros de beleza
- Perfis distintos: consumidor, lojista e administrador
- Carrinho multi-lojista com checkout unificado
- Lista de desejos, avaliacoes e historico de pedidos
- Painel do lojista com cadastro e edicao de produtos
- Moderacao administrativa de lojistas e produtos
- API documentada com Swagger/OpenAPI e Postman
- PoC de recomendacao com IA e fallback local

## Tecnologias

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core
- ASP.NET Identity
- SQLite para demonstracao local
- Swagger / OpenAPI

## Como executar

Pre-requisito: instalar o SDK do `.NET 10`. Antes de rodar, confira a versao ativa:

```powershell
dotnet --version
```

O comando deve retornar uma versao `10.x`.

```powershell
dotnet restore
dotnet build
dotnet run
```

Aplicacao local:

- Sistema web: `http://localhost:5016`
- Swagger: `http://localhost:5016/swagger`

## Contas de demonstracao

- Consumidor: `cliente@beautymarket.com` / `Cliente@123`
- Lojista: `lojista@beautymarket.com` / `Lojista@123`
- Administrador: `admin@beautymarket.com` / `Admin@123`

## Estrutura de documentacao

- `docs/Entrega 1`: concepcao inicial do projeto
- `docs/Entrega 2`: design, UX, prototipo e validacao de usabilidade
- `docs/Entrega 3`: arquitetura, C4 e modelagem de dados
- `docs/Entrega 4`: padroes GoF, APIs e IA
- `docs/Entrega Final`: Entrega Final / Entrega 5, com roteiro do video de defesa e checklist de submissao

## Links principais

- Repositorio publico: `https://github.com/EduardoSilvaNegreiros/TrabalhoFaculdade5Semestre2026`
- GitHub Projects / Kanban: `https://github.com/users/EduardoSilvaNegreiros/projects/2`
- Video final de defesa: `https://youtu.be/8xRP4LWxfyA?is=C3TIzKe_77GF9F3Z`
