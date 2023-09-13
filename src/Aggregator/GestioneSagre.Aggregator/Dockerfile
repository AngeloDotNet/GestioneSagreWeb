FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Aggregator/GestioneSagre.Aggregator/GestioneSagre.Aggregator.csproj", "src/Aggregator/GestioneSagre.Aggregator/"]
COPY ["src/Aggregator/GestioneSagre.Aggregator.Shared/GestioneSagre.Aggregator.Shared.csproj", "src/Aggregator/GestioneSagre.Aggregator.Shared/"]
RUN dotnet restore "src/Aggregator/GestioneSagre.Aggregator/GestioneSagre.Aggregator.csproj"
COPY . .

WORKDIR "/src/src/Aggregator/GestioneSagre.Aggregator"
RUN dotnet build "GestioneSagre.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Aggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.Aggregator.dll"]