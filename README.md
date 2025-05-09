# 📦 Rastreamento de Pedidos

Sistema de gerenciamento e rastreamento de pedidos de entrega, desenvolvido com ASP.NET Core, PostgreSQL e Docker. Permite registrar pedidos, atualizar seus status e acompanhar o progresso da entrega.

## 🚀 Funcionalidades

- 📋 Cadastro de pedidos
- 🔄 Atualização de status de pedidos
- 🔍 Consulta de pedidos por ID
- 🗺️ Rastreio de status por etapas

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose

## 📦 Como Instalar e Executar

### ✅ Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)

### 🐳 Usando Docker (recomendado)

```bash
# Clone o projeto
git clone https://github.com/PedroLuc-cpu/RastreamentoPedidos.git
cd RastreamentoPedidos

# Suba a aplicação e o banco de dados
docker-compose up --build
