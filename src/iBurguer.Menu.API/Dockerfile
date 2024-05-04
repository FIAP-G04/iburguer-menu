FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["iBurguer.Menu.API/iBurguer.Menu.API.csproj", "iBurguer.Menu.API/"]
COPY ["iBurguer.Menu.Core/iBurguer.Menu.Core.csproj", "iBurguer.Menu.Core/"]
COPY ["iBurguer.Menu.Infrastructure/iBurguer.Menu.Infrastructure.csproj", "iBurguer.Menu.Infrastructure/"]
RUN dotnet restore "iBurguer.Menu.API/iBurguer.Menu.API.csproj"
COPY . .
WORKDIR "/src/iBurguer.Menu.API"
RUN dotnet build "iBurguer.Menu.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "iBurguer.Menu.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iBurguer.Menu.API.dll"]
