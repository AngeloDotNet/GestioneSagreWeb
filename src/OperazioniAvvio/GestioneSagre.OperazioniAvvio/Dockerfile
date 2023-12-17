FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio/GestioneSagre.OperazioniAvvio.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio/"]
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.BusinessLayer/GestioneSagre.OperazioniAvvio.BusinessLayer.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.BusinessLayer/"]
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.BusinessLayer.Test/GestioneSagre.OperazioniAvvio.BusinessLayer.Test.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.BusinessLayer.Test/"]
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.DataAccessLayer/GestioneSagre.OperazioniAvvio.DataAccessLayer.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.DataAccessLayer/"]
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.DataAccessLayer.Test/GestioneSagre.OperazioniAvvio.DataAccessLayer.Test.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.DataAccessLayer.Test/"]
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.IntegrationTest/GestioneSagre.OperazioniAvvio.IntegrationTest.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.IntegrationTest/"]
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.Shared/GestioneSagre.OperazioniAvvio.Shared.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.Shared/"]
COPY ["src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.MessageBroker/GestioneSagre.OperazioniAvvio.MessageBroker.csproj", "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio.MessageBroker/"]
COPY ["src/Messaging/GestioneSagre.Messaging/GestioneSagre.Messaging.csproj", "src/Messaging/GestioneSagre.Messaging/"]
RUN dotnet restore "src/OperazioniAvvio/GestioneSagre.OperazioniAvvio/GestioneSagre.OperazioniAvvio.csproj"
COPY . .

WORKDIR "/src/src/OperazioniAvvio/GestioneSagre.OperazioniAvvio"
RUN dotnet build "GestioneSagre.OperazioniAvvio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.OperazioniAvvio.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.OperazioniAvvio.dll"]