# Escala de Segurança - API

## Visão Geral

A API do projeto **Escala de Segurança** é responsável por fornecer os serviços de backend necessários para o funcionamento do sistema de gerenciamento de escalas de segurança. Esta API foi desenvolvida utilizando .NET Core, com o objetivo de ser eficiente, escalável e fácil de manter.

## Requisitos

- .NET Core 8 ou superior
- SQL Server

## Configuração e Instalação

### Clonar o Repositório

Primeiro, clone o repositório para sua máquina local:

```bash
git clone https://github.com/ccintianunes/escala-de-seguranca.git
cd escala-de-seguranca/Api
```

### Configurar o Banco de Dados

1- Crie um banco de dados no SQL Server.
2- Atualize a string de conexão no arquivo appsettings.json com as credenciais do seu banco de dados:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
}
```

### Executar as Migrações
Execute as migrações para criar as tabelas no banco de dados:

```bash
dotnet ef database update
```

### Executar a Aplicação

Para iniciar a API, use o seguinte comando:

```bash
dotnet run
```

API URL:
`http://localhost:5115`

Link do swagger com os endpoints:
`http://localhost:5115/swagger/index.html`

## Estrutura do Projeto

### Escala

#### `GET /api/Escala`
- Descrição: Obtém todas as escalas.

#### `POST /api/Escala`
- Descrição: Cria uma nova escala.
- Corpo da requisição: `EscalaDTO`

#### `GET /api/Escala/{id}`
- Descrição: Obtém uma escala específica por ID.
- Parâmetros:
  - `id` (path): ID da escala (integer, required)

#### `PUT /api/Escala/{id}`
- Descrição: Atualiza uma escala existente por ID.
- Parâmetros:
  - `id` (path): ID da escala (integer, required)
- Corpo da requisição: `EscalaDTO`

#### `DELETE /api/Escala/{id}`
- Descrição: Remove uma escala específica por ID.
- Parâmetros:
  - `id` (path): ID da escala (integer, required)

#### `GET /api/Escala/pagination`
- Descrição: Obtém escalas com paginação.
- Parâmetros:
  - `PageNumber` (query): Número da página (integer)
  - `PageSize` (query): Tamanho da página (integer)

#### `PATCH /api/Escala/{id}/UpdatePartial`
- Descrição: Atualiza parcialmente uma escala específica por ID.
- Parâmetros:
  - `id` (path): ID da escala (integer, required)
- Corpo da requisição: Array de operações de patch

### Local

#### `GET /api/Local`
- Descrição: Obtém todos os locais.

#### `POST /api/Local`
- Descrição: Cria um novo local.
- Corpo da requisição: `LocalDTO`

#### `GET /api/Local/{id}`
- Descrição: Obtém um local específico por ID.
- Parâmetros:
  - `id` (path): ID do local (integer, required)

#### `PUT /api/Local/{id}`
- Descrição: Atualiza um local existente por ID.
- Parâmetros:
  - `id` (path): ID do local (integer, required)
- Corpo da requisição: `LocalDTO`

#### `DELETE /api/Local/{id}`
- Descrição: Remove um local específico por ID.
- Parâmetros:
  - `id` (path): ID do local (integer, required)

#### `GET /api/Local/pagination`
- Descrição: Obtém locais com paginação.
- Parâmetros:
  - `PageNumber` (query): Número da página (integer)
  - `PageSize` (query): Tamanho da página (integer)

#### `PATCH /api/Local/{id}/UpdatePartial`
- Descrição: Atualiza parcialmente um local específico por ID.
- Parâmetros:
  - `id` (path): ID do local (integer, required)
- Corpo da requisição: Array de operações de patch

### MarcacaoEscala

#### `GET /api/MarcacaoEscala`
- Descrição: Obtém todas as marcações de escala.

#### `POST /api/MarcacaoEscala`
- Descrição: Cria uma nova marcação de escala.
- Corpo da requisição: `MarcacaoEscalaDTO`

#### `GET /api/MarcacaoEscala/{id}`
- Descrição: Obtém uma marcação de escala específica por ID.
- Parâmetros:
  - `id` (path): ID da marcação de escala (integer, required)

#### `PUT /api/MarcacaoEscala/{id}`
- Descrição: Atualiza uma marcação de escala existente por ID.
- Parâmetros:
  - `id` (path): ID da marcação de escala (integer, required)
- Corpo da requisição: `MarcacaoEscalaDTO`

#### `DELETE /api/MarcacaoEscala/{id}`
- Descrição: Remove uma marcação de escala específica por ID.
- Parâmetros:
  - `id` (path): ID da marcação de escala (integer, required)

#### `GET /api/MarcacaoEscala/pagination`
- Descrição: Obtém marcações de escala com paginação.
- Parâmetros:
  - `PageNumber` (query): Número da página (integer)
  - `PageSize` (query): Tamanho da página (integer)

#### `PATCH /api/MarcacaoEscala/{id}/UpdatePartial`
- Descrição: Atualiza parcialmente uma marcação de escala específica por ID.
- Parâmetros:
  - `id` (path): ID da marcação de escala (integer, required)
- Corpo da requisição: Array de operações de patch

### Policial

#### `GET /api/Policial`
- Descrição: Obtém todos os policiais.

#### `POST /api/Policial`
- Descrição: Cria um novo policial.
- Corpo da requisição: `PolicialDTO`

#### `GET /api/Policial/{id}`
- Descrição: Obtém um policial específico por ID.
- Parâmetros:
  - `id` (path): ID do policial (integer, required)

#### `PUT /api/Policial/{id}`
- Descrição: Atualiza um policial existente por ID.
- Parâmetros:
  - `id` (path): ID do policial (integer, required)
- Corpo da requisição: `PolicialDTO`

#### `DELETE /api/Policial/{id}`
- Descrição: Remove um policial específico por ID.
- Parâmetros:
  - `id` (path): ID do policial (integer, required)

#### `GET /api/Policial/pagination`
- Descrição: Obtém policiais com paginação.
- Parâmetros:
  - `PageNumber` (query): Número da página (integer)
  - `PageSize` (query): Tamanho da página (integer)

#### `PATCH /api/Policial/{id}/UpdatePartial`
- Descrição: Atualiza parcialmente um policial específico por ID.
- Parâmetros:
  - `id` (path): ID do policial (integer, required)
- Corpo da requisição: Array de operações de patch

#### `GET /api/Policial/filter`
- Descrição: Filtra policiais por nome e CPF.
- Parâmetros:
  - `Nome` (query): Nome do policial (string)
  - `CPF` (query): CPF do policial (string)
  - `PageNumber` (query): Número da página (integer)
  - `PageSize` (query): Tamanho da página (integer)

## Endpoints Principais
Escalas
GET /api/Escala: Retorna uma lista de escalas de segurança.
POST /api/Escalas: Cria uma nova escala de segurança.
PUT /api/Escalas/{id}: Atualiza uma escala de segurança existente.
DELETE /api/Escalas/{id}: Remove uma escala de segurança.

## Contribuição
- Se você deseja contribuir com este projeto, por favor, siga os passos abaixo:
