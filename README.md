# ProjIntegSATCMedieval

Sistema web para gestão de estoque, compras e cotação de fornecedores de uma churrascaria/restaurante.

## Visão geral

O ProjIntegSATCMedieval foi desenvolvido para digitalizar e centralizar o processo de controle de estoque de um restaurante, reduzindo a dependência de listas impressas, planilhas e comunicação manual com fornecedores.

A solução integrada combina:

- cadastro e gerenciamento de produtos, fornecedores e usuários;
- controle de estoque e movimentações;
- cadastro de grupos e unidades de medida;
- geração de cotações e comparação de preços;
- criação e acompanhamento de pedidos de compra;
- conferência de recebimentos e registro de divergências;
- autenticação de usuários e navegação por módulos.

## Objetivo do projeto

O principal objetivo é melhorar a eficiência operacional do setor de compras e estoque, reduzir erros de digitação e contagem, manter um histórico confiável e apoiar a tomada de decisão com dados mais organizados.

## Funcionalidades principais

- Controle de estoque atualizado por produto e movimentação
- Cadastro de grupos de produtos e unidades de medida
- Gestão de fornecedores e histórico de relacionamento
- Cotação por fornecedor com comparação de preços
- Geração de pedidos de compra
- Registro de saídas e entradas do estoque
- Conferência de recebimentos e divergências
- Login e autenticação de usuários
- Interface com módulos organizados por área de negócio

## Stack tecnológica

### Backend

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Scalar para documentação da API

### Frontend

- React
- TypeScript
- Vite
- React Router

## Estrutura do repositório

```text
ProjIntegSATCMedieval/
├── backend/
│   └── MedievalApi/
│       ├── Controllers/
│       ├── Data/
│       ├── DTOs/
│       ├── Models/
│       ├── Services/
│       ├── appsettings.json
│       ├── app.db
│       └── Program.cs
├── frontend/
│   ├── src/
│   ├── public/
│   ├── package.json
│   ├── vite.config.*
│   └── .env
├── docs/
├── Medieval DB.sql
├── README.md
└── documentação estoque medieval.pdf
```

## Requisitos

Antes de executar o projeto, verifique se você possui:

- .NET SDK 10
- Node.js 20+ e npm
- Git

## Como executar

### 1) Backend

Acesse a pasta do backend e execute:

```bash
cd backend/MedievalApi

dotnet restore

dotnet run
```

O projeto expõe a API em uma porta local do ASP.NET Core, normalmente:

- http://localhost:5283
- https://localhost:7196


Para abrir a documentação, basta adicionar um /scalar no fim da URL.
### 2) Frontend

Abra outro terminal e execute:

```bash
cd frontend

npm install
npm run dev
```

O frontend será servido em:

- http://localhost:5173

### 3) Configuração da API no frontend

O projeto já inclui um arquivo `.env` no frontend com a URL base da API. Se necessário, ajuste o valor para a porta correta do backend:

```env
VITE_API_URL=http://localhost:5283
```

ou, caso esteja usando HTTPS:

```env
VITE_API_URL=https://localhost:7196
```

## Documentação da API

Durante o ambiente de desenvolvimento, a API também pode ser consultada pela documentação gerada com Scalar/OpenAPI.

## Fluxo de uso do sistema

1. Usuário realiza login no sistema.
2. O administrador cadastra ou atualiza produtos, fornecedores e unidades de medida.
3. O responsável registra o estoque disponível.
4. O sistema organiza a cotação com fornecedores e compara os preços.
5. Define-se o pedido de compra.
6. A entrada dos itens é conferida ao chegar no restaurante.
7. Divergências e saídas são registradas e acompanhadas por histórico.

## Módulos principais

### Administração

Área para controle geral da aplicação, com acesso aos cadastros e configurações do sistema.

### Fornecedores

Cadastro e manutenção de fornecedores, contatos e dados relevantes para o relacionamento comercial.

### Produtos e grupos

Organização dos itens do restaurante por categoria e classificação.

### Estoque

Registro da quantidade disponível, histórico e movimentações do estoque.

### Cotação

Acompanhamento das propostas recebidas dos fornecedores e comparação entre preços e condições.

### Pedidos

Criação, organização e controle do status dos pedidos de compra.

### Saída

Registro de saídas e movimentações internas do estoque.

## Observações

- O banco de dados local do backend é SQLite (`backend/MedievalApi/app.db`).
- O projeto possui migrações do Entity Framework e pode ser evoluído por meio de alterações no modelo de dados.
- A estrutura atual é um monorepo com frontend e backend independentes, comunicação via API REST.

## Licença

Este projeto foi desenvolvido para fins acadêmicos e institucionais, conforme o contexto do repositório e da aplicação.

## Autores

- GabrielRodolfoR
- Kirbtop
- JPauloVotri
- Paulocesar-G
Autores:

