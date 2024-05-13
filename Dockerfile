FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./src/XpChallenge.Portfolio.Api/XpChallenge.Portfolio.Api.csproj", "src/XpChallenge.Portfolio.Api/"]
COPY ["./src/XpChallenge.Portfolio.Application/XpChallenge.Portfolio.Application.csproj", "src/XpChallenge.Portfolio.Application/"]
COPY ["./src/XpChallenge.Portfolio.Domain/XpChallenge.Portfolio.Domain.csproj", "src/XpChallenge.Portfolio.Domain/"]
COPY ["./src/XpChallenge.Portfolio.Infra.Mongo/XpChallenge.Portfolio.Infra.Mongo.csproj", "src/XpChallenge.Portfolio.Infra.Mongo/"]
RUN dotnet restore "src/XpChallenge.Portfolio.Api/XpChallenge.Portfolio.Api.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "src/XpChallenge.Portfolio.Api/XpChallenge.Portfolio.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/XpChallenge.Portfolio.Api/XpChallenge.Portfolio.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XpChallenge.Portfolio.Api.dll"]