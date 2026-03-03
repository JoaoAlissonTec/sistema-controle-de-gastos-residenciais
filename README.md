
# Sistema de Controle de Gastos Residenciais

Aplicação construída para gerenciar gastos residencias. Construída usando .NET e React.


## Instalação

Instale as dependecias do front-end via npm:

```bash
cd front
npm install
```

E para certificar que o banco de dados esteja criado corretamente, no projeto webApi execute os comandos:

```bash
cd webApi
dotnet ef database update
```
## Variáveis de Ambiente

Para rodar o projeto front-end, você precisa adicionar o arquivo de variáveis de ambiente .env com as seguintes variáveis:

`APP_API_BASE_URL`


## Referência API

### Persons

#### Get all Persons

```http
  GET /api/Persons
```

| Parâmetro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `page` | `number` | Página |
| `pageSize` | `number` | Quantidade de itens por página |

#### Get Persons

```http
  GET /api/Persons/${id}
```

| Parâmetro | Tipo     | Descrição                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id da pessoa |

#### Get Persons Total Transactions

```http
  GET /api/Persons/TotalTransactions
```

| Parâmetro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `page` | `number` | Página |
| `pageSize` | `number` | Quantidade de itens por página |

#### Post Persons

```http
  POST /api/Persons
```

#### Put Persons

```http
  PUT /api/Persons
```

#### Patch Persons

```http
  PUT /api/Persons/${id}
```

| Parâmetro | Tipo     | Descrição                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id da pessoa |

#### Delete Persons

```http
  DELETE /api/Persons/${id}
```

| Parâmetro | Tipo     | Descrição                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id da pessoa |

### Categories

#### Get all Categories

```http
  GET /api/Categories
```

| Parâmetro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `page` | `number` | Página |
| `pageSize` | `number` | Quantidade de itens por página |

#### Get Categories

```http
  GET /api/Categories/${id}
```

| Parâmetro | Tipo     | Descrição                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id da categoria |

#### Get Categories Total Transactions

```http
  GET /api/Categories/TotalTransactions
```

| Parâmetro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `page` | `number` | Página |
| `pageSize` | `number` | Quantidade de itens por página |

#### Post Categories

```http
  POST /api/Categories
```

### Transactions

#### Get all Transactions

```http
  GET /api/Transactions
```

| Parâmetro | Tipo     | Descrição                |
| :-------- | :------- | :------------------------- |
| `page` | `number` | Página |
| `pageSize` | `number` | Quantidade de itens por página |

#### Get Transactions

```http
  GET /api/Transactions/${id}
```

| Parâmetro | Tipo     | Descrição                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id da transação |

#### Post Transactions

```http
  POST /api/Transactions
```
