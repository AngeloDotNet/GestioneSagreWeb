FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApp/GestioneSagre.Web.App.csproj", "src/WebApp/"]
COPY ["src/WebApp/nginx.conf", "src/WebApp/"]
RUN dotnet restore "src/WebApp/GestioneSagre.Web.App.csproj"
COPY . .

WORKDIR "/src/src/WebApp/"
RUN dotnet build "GestioneSagre.Web.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneSagre.Web.App.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
