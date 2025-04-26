# Usa a imagem oficial do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo de solução e restaura as dependências
COPY *.sln .
COPY RastreamentoPedidos/*.csproj ./RastreamentoPedidos/
RUN dotnet restore

# Copia o restante do código e faz o build
COPY RastreamentoPedidos/. ./RastreamentoPedidos/
WORKDIR /app/RastreamentoPedidos
RUN dotnet publish -c Release -o out

# Usa a imagem oficial do ASP.NET Core Runtime para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/RastreamentoPedidos/out .

# Expõe a porta padrão
EXPOSE 80

# Comando de inicialização
ENTRYPOINT ["dotnet", "RastreamentoPedidos.dll"]
