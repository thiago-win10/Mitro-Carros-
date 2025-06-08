# Usar a imagem base do .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usar a imagem SDK para build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar todo o conteúdo da raiz do projeto para dentro do contêiner
COPY . .

# Verificar a estrutura do diretório no contêiner
RUN ls -R /src

# Restaurar dependências
RUN dotnet restore "MitroVehicle.Internal.API/MitroVehicle.Internal.API.csproj"

# Build do projeto
RUN dotnet build "MitroVehicle.Internal.API/MitroVehicle.Internal.API.csproj" -c Release -o /app/build

# Publicar o projeto
FROM build AS publish
RUN dotnet publish "MitroVehicle.Internal.API/MitroVehicle.Internal.API.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MitroVehicle.Internal.API.dll"]
