FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Prodotti/GestioneSagre.Prodotti/GestioneSagre.Prodotti.csproj", "src/Prodotti/GestioneSagre.Prodotti/"]
COPY ["src/Prodotti/GestioneSagre.Prodotti.BusinessLayer/GestioneSagre.Prodotti.BusinessLayer.csproj", "src/Prodotti/GestioneSagre.Prodotti.BusinessLayer/"]
COPY ["src/Prodotti/GestioneSagre.Prodotti.BusinessLayer.Test/GestioneSagre.Prodotti.BusinessLayer.Test.csproj", "src/Prodotti/GestioneSagre.Prodotti.BusinessLayer.Test/"]
COPY ["src/Prodotti/GestioneSagre.Prodotti.DataAccessLayer/GestioneSagre.Prodotti.DataAccessLayer.csproj", "src/Prodotti/GestioneSagre.Prodotti.DataAccessLayer/"]
COPY ["src/Prodotti/GestioneSagre.Prodotti.DataAccessLayer.Test/GestioneSagre.Prodotti.DataAccessLayer.Test.csproj", "src/Prodotti/GestioneSagre.Prodotti.DataAccessLayer.Test/"]
COPY ["src/Prodotti/GestioneSagre.Prodotti.IntegrationTest/GestioneSagre.Prodotti.IntegrationTest.csproj", "src/Prodotti/GestioneSagre.Prodotti.IntegrationTest/"]
COPY ["src/Prodotti/GestioneSagre.Prodotti.Shared/GestioneSagre.Prodotti.Shared.csproj", "src/Prodotti/GestioneSagre.Prodotti.Shared/"]
RUN dotnet restore "src/Prodotti/GestioneSagre.Prodotti/GestioneSagre.Prodotti.csproj"
COPY . .

WORKDIR "/src/src/Prodotti/GestioneSagre.Prodotti"
RUN dotnet build "GestioneSagre.Prodotti.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Prodotti.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.Prodotti.dll"]