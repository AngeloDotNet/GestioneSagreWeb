FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApp/GestioneSagre.Web.App/GestioneSagre.Web.App.csproj", "src/WebApp/GestioneSagre.Web.App/"]
COPY ["src/WebApp/GestioneSagre.Web.App/libman.json", "src/WebApp/GestioneSagre.Web.App/"]
RUN dotnet restore "src/WebApp/GestioneSagre.Web.App/GestioneSagre.Web.App.csproj"
COPY . .

WORKDIR "/src/src/WebApp/GestioneSagre.Web.App"
RUN dotnet build "GestioneSagre.Web.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Web.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.Web.App.dll"]