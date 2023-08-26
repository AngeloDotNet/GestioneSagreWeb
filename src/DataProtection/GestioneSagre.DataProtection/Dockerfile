FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /src

FROM base as deps

WORKDIR /src/GestioneSagre.DataProtection.BusinessLayer
COPY ./src/DataProtection/GestioneSagre.DataProtection.BusinessLayer/*.csproj .

WORKDIR /src/GestioneSagre.DataProtection.DataAccessLayer
COPY ./src/DataProtection/GestioneSagre.DataProtection.DataAccessLayer/*.csproj .

WORKDIR /src/GestioneSagre.DataProtection.Shared
COPY ./src/DataProtection/GestioneSagre.DataProtection.Shared/*.csproj .

WORKDIR /src/GestioneSagre.DataProtection
COPY ./src/DataProtection/GestioneSagre.DataProtection/*.csproj .

#COPY *.csproj .
RUN dotnet restore "GestioneSagre.DataProtection.csproj"

FROM deps as build
COPY . .
RUN dotnet publish "GestioneSagre.DataProtection.csproj" -c Release -o ../compiled

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as deploy
ENV ASPNETCORE_URLS=http://*:5001
ENV ASPNETCORE_ENVIRONMENT="production"

COPY --from=build /src/compiled .

EXPOSE 5001
ENTRYPOINT ["dotnet", "GestioneSagre.DataProtection.dll"]