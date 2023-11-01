FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/webapp/GestioneSagre.Web.App.csproj", "src/webapp/"]
COPY ["src/webapp/nginx.conf", "src/webapp/"]
RUN dotnet restore "src/WebApp/GestioneSagre.Web.App.csproj"
COPY . .

WORKDIR "/src/src/WebApp/"
RUN dotnet build "GestioneSagre.Web.App.csproj" -c Release -o /app/build

FROM build AS publish
#RUN dotnet publish "GestioneSagre.Web.App.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet publish "GestioneSagre.Web.App.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "GestioneSagre.Web.App.dll"]

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .