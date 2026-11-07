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
- Entity Framework Core (provider Npgsql)
- PostgreSQL
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
│       ├── Middlewares/
│       ├── Migrations/
│       ├── Models/
│       ├── Services/
│       ├── appsettings.json
│       ├── appsettings.Development.json
│       ├── MedievalApi.csproj
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
- PostgreSQL 14 ou superior (testado com a versão 18)
- Node.js 20+ e npm
- Git

A ferramenta `dotnet-ef` é usada para criar e aplicar as migrações do Entity Framework Core:

```bash
dotnet tool install --global dotnet-ef
```

> Se a ferramenta já existir em outra versão, atualize com
> `dotnet tool update --global dotnet-ef`.

## Como executar

### 1) Backend

O backend é uma Web API em ASP.NET Core com Entity Framework Core e PostgreSQL. Os comandos abaixo partem da raiz do repositório.

#### 1.1) Instalar e iniciar o PostgreSQL

**Arch Linux**

```bash
sudo pacman -S postgresql
sudo -iu postgres initdb -D /var/lib/postgres/data
sudo systemctl enable --now postgresql
```

**Debian/Ubuntu**

```bash
sudo apt install postgresql
sudo systemctl enable --now postgresql
```

**Windows**

Opção 1 — instalador oficial (inclui o pgAdmin): baixe em https://www.postgresql.org/download/windows/ e execute. Durante a instalação:

- defina a senha do superusuário `postgres` e anote;
- mantenha a porta `5432`;
- o caminho padrão é `C:\Program Files\PostgreSQL\18`.

Opção 2 — `winget` (Windows 10/11 com App Installer):

```powershell
winget install --id PostgreSQL.PostgreSQL.18 --exact
```

O serviço é criado e iniciado automaticamente. Para conferir no PowerShell:

```powershell
Get-Service postgresql*
```

> Se você instalou via `winget` e a senha do `postgres` não foi definida, conecte com `psql -U postgres -h localhost` (senha em branco) e redefina com
> `ALTER ROLE postgres PASSWORD 'nova-senha';`.

Se o `psql` não for reconhecido no terminal, adicione `C:\Program Files\PostgreSQL\18\bin` ao `PATH` (ou use o atalho "SQL Shell (psql)" do menu Iniciar):

```powershell
$env:Path += ";C:\Program Files\PostgreSQL\18\bin"
```

> No Windows também é possível instalar o WSL2 e seguir as instruções do Debian/Ubuntu.

Conferindo se o servidor está aceitando conexões:

```bash
pg_isready -h localhost -p 5432
```

#### 1.2) Criar o usuário e o banco de dados

A aplicação espera a role `medieval` e o database `medieval`. Se usar outros nomes, ajuste a connection string no passo 1.3.

**Linux**

```bash
sudo -u postgres psql -c "CREATE ROLE medieval LOGIN PASSWORD 'defina-uma-senha-forte';"
sudo -u postgres psql -c "CREATE DATABASE medieval OWNER medieval;"
```

**Windows**

Não existe `sudo`: conecte como o superusuário `postgres`, informando a senha definida na instalação (o `psql` pede a senha).

```powershell
psql -U postgres -h localhost -c "CREATE ROLE medieval LOGIN PASSWORD 'defina-uma-senha-forte';"
psql -U postgres -h localhost -c "CREATE DATABASE medieval OWNER medieval;"
```

Testando a conexão:

```bash
psql -h localhost -U medieval -d medieval -c "select current_user, current_database();"
```

#### 1.3) Configurar a connection string (fora do repositório)

A string de conexão **não** é versionada. Em desenvolvimento ela fica no *user secrets* do .NET, em `~/.microsoft/usersecrets/<UserSecretsId>/secrets.json` — fora da árvore do repositório, com permissão restrita e por usuário. O `UserSecretsId` já está definido no `MedievalApi.csproj`.

```bash
cd backend/MedievalApi

dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "Host=localhost;Port=5432;Database=medieval;Username=medieval;Password=defina-uma-senha-forte"
```

Se a string não estiver configurada, a API não inicia e exibe uma mensagem com o comando exato a executar, em vez de falhar só na primeira requisição.

Conferir e limpar:

```bash
dotnet user-secrets list
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
```

> Os user secrets são carregados apenas quando `ASPNETCORE_ENVIRONMENT=Development`, que é o ambiente definido no `Properties/launchSettings.json` e usado pelo `dotnet run`.

#### 1.4) Aplicar as migrações

```bash
cd backend/MedievalApi

dotnet restore
dotnet ef database update
```

Isso cria as tabelas da aplicação no database configurado. Para inspecionar o SQL sem aplicar nada:

```bash
dotnet ef migrations script --idempotent -o migracao.sql
```

#### 1.5) Executar

```bash
dotnet run
```

Portas definidas no `Properties/launchSettings.json`:

- http://localhost:5283 (perfil `http`)
- https://localhost:7196 (perfil `https`)

Para escolher o perfil explicitamente: `dotnet run --launch-profile https`.

#### 1.6) Criar o primeiro usuário

Não existe seed: o banco sobe vazio. O primeiro administrador pode ser criado pela API ou pela tela de cadastro do frontend:

```bash
curl -X POST http://localhost:5283/api/Usuarios \
  -H "Content-Type: application/json" \
  -d '{"nome":"Administrador","email":"admin@example.com","senha":"senha12345","grupoUsuario":0}'
```

`grupoUsuario` é numérico: `0` = Administrador, `1` = Consulta. O login usado pelo frontend:

```bash
curl -X POST http://localhost:5283/api/Usuarios/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@example.com","senha":"senha12345"}'
```

> **Windows/PowerShell:** use `curl.exe` no lugar de `curl`, porque no PowerShell o `curl` é um alias de `Invoke-WebRequest` e não aceita as opções `-X` / `-d` da mesma forma. Ex.:
> `curl.exe -X POST http://localhost:5283/api/Usuarios -H "Content-Type: application/json" -d "{\"nome\":\"Administrador\",\"email\":\"admin@example.com\",\"senha\":\"senha12345\",\"grupoUsuario\":0}"`
>
> Alternativamente, use o Scalar (`http://localhost:5283/scalar/v1`) ou o pgAdmin para executar o mesmo cadastro.

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

## Ambiente de produção

Em produção a configuração vem do ambiente do processo (variáveis de ambiente), não de user secrets nem de arquivo versionado.

### Variáveis de ambiente

| Variável | Obrigatória | Descrição |
|---|---|---|
| `ASPNETCORE_ENVIRONMENT` | Sim | Use `Production` |
| `ConnectionStrings__DefaultConnection` | Sim | Connection string completa do PostgreSQL |
| `ASPNETCORE_URLS` | Não | Endereço de escuta, ex.: `http://0.0.0.0:8080` |

```bash
export ASPNETCORE_ENVIRONMENT=Production
export ConnectionStrings__DefaultConnection="Host=db.interno;Port=5432;Database=medieval;Username=medieval;Password=SENHA_FORTE;SSL Mode=Require"
export ASPNETCORE_URLS="http://0.0.0.0:8080"
```

No Windows/PowerShell o equivalente é:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Production"
$env:ConnectionStrings__DefaultConnection = "Host=db.interno;Port=5432;Database=medieval;Username=medieval;Password=SENHA_FORTE;SSL Mode=Require"
$env:ASPNETCORE_URLS = "http://0.0.0.0:8080"
```

Em produção de verdade (IIS, Windows Service, Docker, systemd), o ideal é definir essas variáveis no serviço/orquestrador e não no terminal da sessão.

O `Program.cs` valida a connection string no startup e aborta com uma mensagem clara quando ela não existe.

### Publicação

```bash
cd backend/MedievalApi

dotnet publish -c Release -o ./publish

cd publish
dotnet MedievalApi.dll
```

### Migrações em produção

Com o banco acessível a partir da máquina onde o comando roda:

```bash
cd backend/MedievalApi
dotnet ef database update
```

Quando o banco não é acessível da máquina de build (caso comum), gere o script e aplique com `psql`:

```bash
dotnet ef migrations script --idempotent -o migracao.sql
psql "host=db.interno dbname=medieval user=medieval password=SENHA_FORTE" -f migracao.sql
```

O `--idempotent` gera blocos `DO $$ ... IF NOT EXISTS`, então o script pode ser executado novamente sem erro em bancos que já receberam a migração.

### CORS

As origens liberadas estão fixas no `Program.cs` e cobrem apenas o frontend local:

```csharp
.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
```

Em produção é necessário incluir a URL real do frontend, senão o navegador bloqueia as requisições. Para não recompilar a cada ambiente, o caminho natural é ler as origens da configuração (por exemplo `Cors:AllowedOrigins` no `appsettings.json`) em vez de mantê-las no código.

### Autenticação do PostgreSQL

O `initdb` padrão cria regras `trust` para conexões locais, o que faz qualquer senha ser aceita. Em ambiente publicado, confira e ajuste para `scram-sha-256`:

```bash
sudo -u postgres psql -c "select type, database, user_name, address, auth_method from pg_hba_file_rules;"
```

Verifique também a exposição de rede: `listen_addresses = 'localhost'` quando a aplicação roda na mesma máquina, ou firewall e regras de rede quando o banco é externo (nesse caso, use `SSL Mode=Require` na connection string).

### O que muda em relação ao desenvolvimento

- Scalar e o documento OpenAPI são mapeados apenas quando `app.Environment.IsDevelopment()`; em produção essas rotas retornam 404.
- A connection string vem da variável `ConnectionStrings__DefaultConnection`, nunca do user secrets.

## Documentação da API

Com a API em execução em ambiente de desenvolvimento:

- Scalar: http://localhost:5283/scalar/v1
- OpenAPI (JSON): http://localhost:5283/openapi/v1.json

O endereço `/scalar` sem a versão redireciona para `/scalar/v1`.

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

- O backend usa PostgreSQL com Entity Framework Core (provider Npgsql) e migrações versionadas em `backend/MedievalApi/Migrations`.
- A connection string fica no user secrets em desenvolvimento e em variável de ambiente em produção; ela nunca é versionada.
- O projeto possui migrações do Entity Framework e pode ser evoluído por meio de alterações no modelo de dados (com `dotnet ef migrations add <Nome>`).
- O banco não possui seed: o primeiro usuário precisa ser criado pela API (`POST /api/Usuarios`) ou pela tela de cadastro.
- A estrutura atual é um monorepo com frontend e backend independentes, comunicação via API REST.
- Limitação conhecida: os campos de data sem hora (`dataSolicitacao`, `dataLimiteResposta`, `dataPrevistaEntrega`, `dataValidade`, `ultimaEntrada`, `ultimaSaida`) chegam do `<input type="date">` como `AAAA-MM-DD` e estão mapeados como `timestamptz`, então o Npgsql rejeita a gravação (`Kind=Unspecified`). Enquanto o mapeamento não for ajustado, cadastros que enviam esses campos retornam erro 500.

## Licença

Este projeto foi desenvolvido para fins acadêmicos e institucionais, conforme o contexto do repositório e da aplicação.

## Autores

- GabrielRodolfoR
- Kirbtop
- JPauloVotri
- Paulocesar-G
Autores:

