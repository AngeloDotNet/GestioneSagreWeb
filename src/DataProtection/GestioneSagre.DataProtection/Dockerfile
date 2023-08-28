FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/DataProtection/GestioneSagre.DataProtection/GestioneSagre.DataProtection.csproj", "src/DataProtection/GestioneSagre.DataProtection/"]
COPY ["src/DataProtection/GestioneSagre.DataProtection.BusinessLayer/GestioneSagre.DataProtection.BusinessLayer.csproj", "src/DataProtection/GestioneSagre.DataProtection.BusinessLayer/"]
COPY ["src/DataProtection/GestioneSagre.DataProtection.DataAccessLayer/GestioneSagre.DataProtection.DataAccessLayer.csproj", "src/DataProtection/GestioneSagre.DataProtection.DataAccessLayer/"]
COPY ["src/DataProtection/GestioneSagre.DataProtection.IntegrationTest/GestioneSagre.DataProtection.IntegrationTest.csproj", "src/DataProtection/GestioneSagre.DataProtection.IntegrationTest/"]
COPY ["src/DataProtection/GestioneSagre.DataProtection.Shared/GestioneSagre.DataProtection.Shared.csproj", "src/DataProtection/GestioneSagre.DataProtection.Shared/"]
RUN dotnet restore "src/DataProtection/GestioneSagre.DataProtection/GestioneSagre.DataProtection.csproj"
COPY . .

WORKDIR "/src/src/DataProtection/GestioneSagre.DataProtection"
RUN dotnet build "GestioneSagre.DataProtection.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.DataProtection.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.DataProtection.dll"]