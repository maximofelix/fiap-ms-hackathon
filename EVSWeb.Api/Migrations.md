# 📦 Executando Migrations no Projeto EVSWeb (.NET Web API)

Este guia ensina como criar e aplicar migrations em uma aplicação ASP.NET Core dividida por camadas/projetos.

---

## 🗂️ Estrutura do Projeto

```
EVSWeb.sln
│
├── EVSWeb.Api              # Projeto principal (startup)
├── EVSWeb.Domain           # Contém as entidades (Models)
└── EVSWeb.Infrastructure   # Contém o DbContext e configurações do EF Core
```

---

## ✅ Pré-requisitos

- .NET 6 ou superior
- EF Core CLI (via `dotnet`)
- O `DbContext` deve estar definido em `EVSWeb.Infrastructure`

---

## 📌 1. Instalar pacotes necessários

Execute os comandos abaixo no terminal, dentro da raiz da solução (onde está o `.sln`):

```bash
dotnet add EVSWeb.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add EVSWeb.Infrastructure package Microsoft.EntityFrameworkCore.Design
dotnet add EVSWeb.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
?? dotnet add EVSWeb.Api package Microsoft.EntityFrameworkCore
?? dotnet add EVSWeb.Api package Microsoft.EntityFrameworkCore.Design
?? dotnet add EVSWeb.Api package Microsoft.EntityFrameworkCore.SqlServer
```

*Troque `SqlServer` por outro provedor, se necessário (ex: `Npgsql` para PostgreSQL, `FirebirdSql.EntityFrameworkCore.Firebird` para Firebird, etc).*

---

## ⚙️ 2. Criar uma Migration

Execute o comando abaixo a partir da **raiz do projeto**:

```bash
dotnet ef migrations add NomeDaMigration \
  --project EVSWeb.Infrastructure \
  --startup-project EVSWeb.Api \
  --output-dir Migrations
```
ex: dotnet ef migrations add InitialMigration --project EVSWeb.Infrastructure --startup-project EVSWeb.Api --output-dir Migrations

### Explicação:
- `--project`: onde está o `DbContext`
- `--startup-project`: projeto que contém o `appsettings.json` e `Program.cs`
- `--output-dir`: diretório onde os arquivos da migration serão criados (opcional)

---

## 🧪 3. Aplicar a Migration no Banco de Dados

```bash
dotnet ef database update \
  --project EVSWeb.Infrastructure \
  --startup-project EVSWeb.Api
```
ex: dotnet ef database update --project EVSWeb.Infrastructure --startup-project EVSWeb.Api
---

## 🔄 Outras operações úteis

### Reverter uma migration (voltar para estado anterior)

```bash
dotnet ef database update NomeDaMigrationAnterior \
  --project EVSWeb.Infrastructure \
  --startup-project EVSWeb.Api
```

### Remover a última migration (se ainda não foi aplicada)

```bash
dotnet ef migrations remove \
  --project EVSWeb.Infrastructure \
  --startup-project EVSWeb.Api
```

---

## 🐞 Dicas de resolução de erros

- **"Unable to create an object of type 'SeuDbContext'"**  
  ➤ Verifique se o `EVSWeb.Api` tem a configuração correta de injeção de dependência (`services.AddDbContext<...>()`).

- **"No design-time DbContext services were found"**  
  ➤ Certifique-se de que o `DbContext` está acessível publicamente e que há um construtor com `DbContextOptions`.

---

## 💡 Exemplo de configuração no Program.cs (EVSWeb.Api)

```csharp
builder.Services.AddDbContext<SeuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

No `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EVSWebDB;Trusted_Connection=True;"
}
```

---

## 🧼 Recomendação

Adicione um `DbContextFactory` se quiser evitar problemas com o design-time:

```csharp
// EVSWeb.Infrastructure/Factories/DbContextFactory.cs
public class DbContextFactory : IDesignTimeDbContextFactory<SeuDbContext>
{
    public SeuDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SeuDbContext>();
        optionsBuilder.UseSqlServer("sua-string-conexao");

        return new SeuDbContext(optionsBuilder.Options);
    }
}
```

---

## 🏁 Conclusão

Você agora está apto a:

- Criar migrations
- Atualizar o banco de dados
- Manter seu modelo de dados versionado via código

Se precisar de ajuda com versionamento, deploy, ou rollback de migrations, consulte a [documentação oficial do EF Core](https://learn.microsoft.com/ef/core/cli/dotnet).

---
