FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./src/Servers/CarRentingSystem/CarRentingSystem.Analyses", "./Servers/CarRentingSystem/CarRentingSystem.Analyses"]
COPY ["./src/Servers/CarRentingSystem/CarRentingSystem.Common", "./Servers/CarRentingSystem/CarRentingSystem.Common"]
RUN dotnet restore "Servers/CarRentingSystem/CarRentingSystem.Analyses/CarRentingSystem.Analyses.csproj"
RUN dotnet build "Servers/CarRentingSystem/CarRentingSystem.Analyses/CarRentingSystem.Analyses.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "Servers/CarRentingSystem/CarRentingSystem.Analyses/CarRentingSystem.Analyses.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentingSystem.Analyses.dll"]