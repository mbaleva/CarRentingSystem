FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Servers/CarRentingSystem/CarRentingSystem.HealthChecks/CarRentingSystem.HealthChecks.csproj", "CarRentingSystem.HealthChecks/"]
COPY ["./Servers/CarRentingSystem/CarRentingSystem.Common/CarRentingSystem.Common.csproj", "CarRentingSystem.Common/"]
RUN dotnet restore "CarRentingSystem.HealthChecks/CarRentingSystem.HealthChecks.csproj"
COPY . .
WORKDIR "/src/CarRentingSystem.HealthChecks"
RUN dotnet build "CarRentingSystem.HealthChecks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRentingSystem.HealthChecks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentingSystem.HealthChecks.dll"]