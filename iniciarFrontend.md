# Como inicializar o projeto

Siga os passos abaixo para rodar o projeto completo em sua máquina.

## 1) Pré-requisitos

Antes de iniciar, certifique-se de ter instalado:

- Git
- Node.js LTS: https://nodejs.org/pt-br/download
- .NET SDK 10: https://dotnet.microsoft.com/pt-br/download

Verifique se tudo foi instalado corretamente:

```bash
git --version
node --version
npm --version
dotnet --version
```

## 2) Clone o projeto

```bash
git clone https://github.com/JPauloVotri/ProjIntegSATCMedieval.git
cd ProjIntegSATCMedieval
```

## 3) Inicialize o backend

Abra um terminal na pasta do backend:

```bash
cd backend/MedievalApi

dotnet restore
dotnet run
```

O backend será executado em uma das portas abaixo:

- http://localhost:5283
- https://localhost:7196

## 4) Inicialize o frontend

Abra outro terminal e execute:

```bash
cd frontend
npm install
npm run dev
```

## 5) Acesse the aplicação

Depois que o frontend iniciar, abra no navegador:

```text
http://localhost:5173/
```

## 6) Caso necessário, ajuste a URL da API

No arquivo `frontend/.env`, confirme se a variável está correta:

```env
VITE_API_URL=http://localhost:5283
```

Se o backend estiver rodando em HTTPS, ajuste para:

```env
VITE_API_URL=https://localhost:7196
```

## 7) Dicas

- Se a porta 5173 estiver ocupada, o Vite pode abrir outra porta; confira o terminal.
- Se o frontend não iniciar, execute novamente `npm install` na pasta `frontend`.
- Se o backend falhar, confirme se o .NET SDK foi instalado corretamente e que a pasta `backend/MedievalApi` exista.

Atenção: o frontend e o backend devem ser iniciados em terminais diferentes.
