FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Menu/GestioneSagre.Menu/GestioneSagre.Menu.csproj", "src/Menu/GestioneSagre.Menu/"]
COPY ["src/Menu/GestioneSagre.Menu.BusinessLayer/GestioneSagre.Menu.BusinessLayer.csproj", "src/Menu/GestioneSagre.Menu.BusinessLayer/"]
COPY ["src/Menu/GestioneSagre.Menu.BusinessLayer.Test/GestioneSagre.Menu.BusinessLayer.Test.csproj", "src/Menu/GestioneSagre.Menu.BusinessLayer.Test/"]
COPY ["src/Menu/GestioneSagre.Menu.DataAccessLayer/GestioneSagre.Menu.DataAccessLayer.csproj", "src/Menu/GestioneSagre.Menu.DataAccessLayer/"]
COPY ["src/Menu/GestioneSagre.Menu.DataAccessLayer.Test/GestioneSagre.Menu.DataAccessLayer.Test.csproj", "src/Menu/GestioneSagre.Menu.DataAccessLayer.Test/"]
COPY ["src/Menu/GestioneSagre.Menu.IntegrationTest/GestioneSagre.Menu.IntegrationTest.csproj", "src/Menu/GestioneSagre.Menu.IntegrationTest/"]
COPY ["src/Menu/GestioneSagre.Menu.Shared/GestioneSagre.Menu.Shared.csproj", "src/Menu/GestioneSagre.Menu.Shared/"]
#COPY ["src/Menu/GestioneSagre.Menu.MessageBroker/GestioneSagre.Menu.MessageBroker.csproj", "src/Menu/GestioneSagre.Menu.MessageBroker/"]
COPY ["src/Messaging/GestioneSagre.Messaging/GestioneSagre.Messaging.csproj", "src/Messaging/GestioneSagre.Messaging/"]
RUN dotnet restore "src/Menu/GestioneSagre.Menu/GestioneSagre.Menu.csproj"
COPY . .

WORKDIR "/src/src/Menu/GestioneSagre.Menu"
RUN dotnet build "GestioneSagre.Menu.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Menu.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.Menu.dll"]