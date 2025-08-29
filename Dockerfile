FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia os arquivos para restaurar
COPY *.sln .
COPY RastreamentoPedidos/*.csproj ./RastreamentoPedidos/
COPY RastreamentoPedido.Core/*.csproj ./RastreamentoPedido.Core/
COPY RastreamentoPedido.WebApi.Core/*.csproj ./RastreamentoPedido.WebApi.Core/
COPY Clients.Web/*.csproj ./Clients.Web/
COPY RastreamentoPedido.Test/*.csproj ./RastreamentoPedido.Test/

RUN dotnet restore

# Copia tudo
COPY . .

# Publica
WORKDIR /app/RastreamentoPedidos
RUN dotnet publish -c Release -o /app/out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "RastreamentoPedidos.API.dll"]
