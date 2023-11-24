FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gateway/GestioneSagre.Gateway/GestioneSagre.Gateway.csproj", "src/Gateway/GestioneSagre.Gateway/"]
#COPY ["src/Gateway/GestioneSagre.Gateway/ocelot.json", "src/Gateway/GestioneSagre.Gateway/"]
COPY ["src/Gateway/GestioneSagre.Gateway.BusinessLayer/GestioneSagre.Gateway.BusinessLayer.csproj", "src/Gateway/GestioneSagre.Gateway.BusinessLayer/"]

# Copy the Ocelot configuration files
COPY ["src/Gateway/GestioneSagre.Gateway/ConfigOcelot/ocelot.categorie.json", "src/Gateway/GestioneSagre.Gateway/ConfigOcelot/"]
COPY ["src/Gateway/GestioneSagre.Gateway/ConfigOcelot/ocelot.dataprotection.json", "src/Gateway/GestioneSagre.Gateway/ConfigOcelot/"]
COPY ["src/Gateway/GestioneSagre.Gateway/ConfigOcelot/ocelot.gateway.json", "src/Gateway/GestioneSagre.Gateway/ConfigOcelot/"]
COPY ["src/Gateway/GestioneSagre.Gateway/ConfigOcelot/ocelot.operazioniavvio.json", "src/Gateway/GestioneSagre.Gateway/ConfigOcelot/"]
COPY ["src/Gateway/GestioneSagre.Gateway/ConfigOcelot/ocelot.prodotti.json", "src/Gateway/GestioneSagre.Gateway/ConfigOcelot/"]
COPY ["src/Gateway/GestioneSagre.Gateway/ConfigOcelot/ocelot.utility.json", "src/Gateway/GestioneSagre.Gateway/ConfigOcelot/"]

RUN dotnet restore "src/Gateway/GestioneSagre.Gateway/GestioneSagre.Gateway.csproj"
COPY . .

WORKDIR "/src/src/Gateway/GestioneSagre.Gateway"
RUN dotnet build "GestioneSagre.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.Gateway.dll"]