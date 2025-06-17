# Etapa base: runtime do ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa build: SDK do .NET para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo da solution (se existir)
COPY ["*.sln", "./"]

# Copia apenas os arquivos .csproj e restaura as dependências
COPY ["EVSWeb.Api/EVSWeb.Api.csproj", "EVSWeb.Api/"]
# Se houver outros projetos na solution, adicione aqui:
# COPY ["OutroProjeto/OutroProjeto.csproj", "OutroProjeto/"]

RUN dotnet restore "EVSWeb.Api/EVSWeb.Api.csproj"

# Copia o restante dos arquivos da solution
COPY . .

# Compila o projeto específico
WORKDIR "/src/EVSWeb.Api"
RUN dotnet build "EVSWeb.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EVSWeb.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final: gera a imagem final para produção
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EVSWeb.Api.dll"]