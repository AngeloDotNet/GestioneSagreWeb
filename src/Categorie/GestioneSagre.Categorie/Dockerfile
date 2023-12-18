FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Categorie/GestioneSagre.Categorie/GestioneSagre.Categorie.csproj", "src/Categorie/GestioneSagre.Categorie/"]
COPY ["src/Categorie/GestioneSagre.Categorie.BusinessLayer/GestioneSagre.Categorie.BusinessLayer.csproj", "src/Categorie/GestioneSagre.Categorie.BusinessLayer/"]
COPY ["src/Categorie/GestioneSagre.Categorie.BusinessLayer.Test/GestioneSagre.Categorie.BusinessLayer.Test.csproj", "src/Categorie/GestioneSagre.Categorie.BusinessLayer.Test/"]
COPY ["src/Categorie/GestioneSagre.Categorie.DataAccessLayer/GestioneSagre.Categorie.DataAccessLayer.csproj", "src/Categorie/GestioneSagre.Categorie.DataAccessLayer/"]
COPY ["src/Categorie/GestioneSagre.Categorie.DataAccessLayer.Test/GestioneSagre.Categorie.DataAccessLayer.Test.csproj", "src/Categorie/GestioneSagre.Categorie.DataAccessLayer.Test/"]
COPY ["src/Categorie/GestioneSagre.Categorie.IntegrationTest/GestioneSagre.Categorie.IntegrationTest.csproj", "src/Categorie/GestioneSagre.Categorie.IntegrationTest/"]
COPY ["src/Categorie/GestioneSagre.Categorie.Shared/GestioneSagre.Categorie.Shared.csproj", "src/Categorie/GestioneSagre.Categorie.Shared/"]
COPY ["src/Categorie/GestioneSagre.Categorie.MessageBroker/GestioneSagre.Categorie.MessageBroker.csproj", "src/Categorie/GestioneSagre.Categorie.MessageBroker/"]
COPY ["src/Messaging/GestioneSagre.Messaging/GestioneSagre.Messaging.csproj", "src/Messaging/GestioneSagre.Messaging/"]
RUN dotnet restore "src/Categorie/GestioneSagre.Categorie/GestioneSagre.Categorie.csproj"
COPY . .

WORKDIR "/src/src/Categorie/GestioneSagre.Categorie"
RUN dotnet build "GestioneSagre.Categorie.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Categorie.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.Categorie.dll"]