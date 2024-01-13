FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Worker/Notification.Worker/Notification.Worker.csproj", "src/Worker/Notification.Worker/"]
RUN dotnet restore "src/Worker/Notification.Worker/Notification.Worker.csproj"
COPY . .
WORKDIR "/src/src/Worker/Notification.Worker"
RUN dotnet build "Notification.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notification.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Worker.dll"]
