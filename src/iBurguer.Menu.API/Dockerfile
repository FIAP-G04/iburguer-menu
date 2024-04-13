FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/iBurguer.Menu.API/iBurguer.Menu.API.csproj", "src/iBurguer.Menu.API/"]
RUN dotnet restore "src/iBurguer.Menu.API/iBurguer.Menu.API.csproj"
COPY . .
WORKDIR "/src/src/iBurguer.Menu.API"
RUN dotnet build "iBurguer.Menu.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "iBurguer.Menu.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iBurguer.Menu.API.dll"]
