# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia arquivos da solução e projetos
COPY *.sln .
COPY RastreamentoPedidos/*.csproj ./RastreamentoPedidos/
COPY RastreamentoPedido.Core/*.csproj ./RastreamentoPedido.Core/
COPY RastreamentoPedido.WebApi.Core/*.csproj ./RastreamentoPedido.WebApi.Core/
COPY Clients.Web/*.csproj ./Clients.Web/
COPY RastreamentoPedido.Test/*.csproj ./RastreamentoPedido.Test/

# Restaura dependências
RUN dotnet restore

# Copia tudo e publica
COPY . .
WORKDIR /app/RastreamentoPedidos
RUN dotnet publish -c Release -o /app/out

# Runtime com .NET + NGINX
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Instala NGINX
RUN apt-get update && apt-get install -y nginx && rm -rf /var/lib/apt/lists/*

# Copia a aplicação publicada
COPY --from=build /app/out .

# Copia configuração do NGINX
COPY nginx.conf /etc/nginx/nginx.conf

# Expõe portas
EXPOSE 80

# Cria script para rodar NGINX + API
RUN echo '#!/bin/bash \n\
  service nginx start \n\
  dotnet RastreamentoPedidos.API.dll' > /start.sh && chmod +x /start.sh

ENTRYPOINT ["/start.sh"]
