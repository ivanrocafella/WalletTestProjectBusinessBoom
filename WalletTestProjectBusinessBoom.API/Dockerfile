# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WalletTestProjectBusinessBoom.API/WalletTestProjectBusinessBoom.API.csproj", "WalletTestProjectBusinessBoom.API/"]
COPY ["WalletTestProjectBusinessBoom.BAL/WalletTestProjectBusinessBoom.BAL.csproj", "WalletTestProjectBusinessBoom.BAL/"]
COPY ["WalletTestProjectBusinessBoom.DAL/WalletTestProjectBusinessBoom.DAL.csproj", "WalletTestProjectBusinessBoom.DAL/"]
COPY ["WalletTestProjectBusinessBoom.Сore/WalletTestProjectBusinessBoom.Сore.csproj", "WalletTestProjectBusinessBoom.Сore/"]
RUN dotnet restore "./WalletTestProjectBusinessBoom.API/WalletTestProjectBusinessBoom.API.csproj"
COPY . .
WORKDIR "/src/WalletTestProjectBusinessBoom.API"
RUN dotnet build "./WalletTestProjectBusinessBoom.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WalletTestProjectBusinessBoom.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный этап
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Копируем файлы миграций в финальный образ
COPY --from=build /src/WalletTestProjectBusinessBoom.DAL/Migrations ./Migrations

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Копируем скрипт в контейнер
COPY entrypoint.sh .
# Делаем скрипт исполняемым
RUN chmod +x entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]