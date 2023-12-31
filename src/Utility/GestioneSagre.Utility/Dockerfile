FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Utility/GestioneSagre.Utility/GestioneSagre.Utility.csproj", "src/Utility/GestioneSagre.Utility/"]
COPY ["src/Utility/GestioneSagre.Utility.BusinessLayer/GestioneSagre.Utility.BusinessLayer.csproj", "src/Utility/GestioneSagre.Utility.BusinessLayer/"]
COPY ["src/Utility/GestioneSagre.Utility.BusinessLayer.Test/GestioneSagre.Utility.BusinessLayer.Test.csproj", "src/Utility/GestioneSagre.Utility.BusinessLayer.Test/"]
COPY ["src/Utility/GestioneSagre.Utility.DataAccessLayer/GestioneSagre.Utility.DataAccessLayer.csproj", "src/Utility/GestioneSagre.Utility.DataAccessLayer/"]
COPY ["src/Utility/GestioneSagre.Utility.DataAccessLayer.Test/GestioneSagre.Utility.DataAccessLayer.Test.csproj", "src/Utility/GestioneSagre.Utility.DataAccessLayer.Test/"]
COPY ["src/Utility/GestioneSagre.Utility.IntegrationTest/GestioneSagre.Utility.IntegrationTest.csproj", "src/Utility/GestioneSagre.Utility.IntegrationTest/"]
COPY ["src/Utility/GestioneSagre.Utility.Shared/GestioneSagre.Utility.Shared.csproj", "src/Utility/GestioneSagre.Utility.Shared/"]
RUN dotnet restore "src/Utility/GestioneSagre.Utility/GestioneSagre.Utility.csproj"
COPY . .

WORKDIR "/src/src/Utility/GestioneSagre.Utility"
RUN dotnet build "GestioneSagre.Utility.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Utility.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneSagre.Utility.dll"]