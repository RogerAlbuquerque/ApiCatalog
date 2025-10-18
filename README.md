# ApiCatalog

Uma **API RESTful** para gerenciamento de catálogo de produtos e categorias. Este projeto permite que clientes realizem operações CRUD em produtos e categorias, com suporte a autenticação, paginação e arquitetura limpa.

## Funcionalidades

- CRUD completo de **Produtos** e **Categorias**
  
- Paginação de resultados
  
- Autenticação via **JWT**
  
- Configuração de **CORS**
  
- Estrutura baseada em **Repository Pattern** e **Unit of Work**, com **DTOs**
  
- Implementação em **MVC** e versão seguindo **Clean Architecture**
  
- Testes unitários com **xUnit**
  

## Tecnologias Utilizadas

- **ASP.NET Core**
  
- **Entity Framework Core 8 e 9**
  
- **MySQL** (compatível com SQL Server)
  
- **AutoMapper**
  
- **Swagger** para documentação da API
  
- **Programação assíncrona** (async/await)
  
- Testes unitários com **xUnit**
  

## Estrutura do Projeto

O projeto possui duas abordagens principais:

1. **Modelo MVC tradicional**
  
2. **Modelo Clean Architecture**, separando camadas de domínio, aplicação, infraestrutura e API
  

## Requisitos

- [.NET 8 ou 9 SDK](https://dotnet.microsoft.com/en-us/download)
  
- MySQL ou SQL Server
  
- Visual Studio / VS Code / JetBrains Rider
  

## Como Rodar

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/apicatalog.git
cd apicatalog
```

2. Configure a **string de conexão** no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=apicatalog;User=root;Password=senha"
}
```

3. Atualize o banco de dados:

```bash
dotnet ef database update
```

4. Execute a aplicação:

```bash
dotnet run
```

5. Acesse a documentação via **Swagger**:

```
http://localhost:5000/swagger
```

## Testes

Execute os testes unitários com:

```bash
dotnet test
```

## Observações

- Suporta múltiplos bancos (MySQL e SQL Server)
  
- Estrutura preparada para evolução e manutenção fácil
  
- Código escrito com foco em **boas práticas**, **manutenibilidade** e **testabilidade**
