FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./src/Servers/CarRentingSystem/CarRentingSystem.HealthChecks", "./Servers/CarRentingSystem/CarRentingSystem.HealthChecks"]
COPY ["./src/Servers/CarRentingSystem/CarRentingSystem.Common", "./Servers/CarRentingSystem/CarRentingSystem.Common"]
RUN dotnet restore "Servers/CarRentingSystem/CarRentingSystem.HealthChecks/CarRentingSystem.HealthChecks.csproj"
RUN dotnet build "Servers/CarRentingSystem/CarRentingSystem.HealthChecks/CarRentingSystem.HealthChecks.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "Servers/CarRentingSystem/CarRentingSystem.HealthChecks/CarRentingSystem.HealthChecks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentingSystem.HealthChecks.dll"]