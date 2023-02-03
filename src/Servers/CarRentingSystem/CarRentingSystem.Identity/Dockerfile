FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./src/Servers/CarRentingSystem/CarRentingSystem.Identity", "./Servers/CarRentingSystem/CarRentingSystem.Identity"]
COPY ["./src/Servers/CarRentingSystem/CarRentingSystem.Common", "./Servers/CarRentingSystem/CarRentingSystem.Common"]
RUN dotnet restore "Servers/CarRentingSystem/CarRentingSystem.Identity/CarRentingSystem.Identity.csproj"
RUN dotnet build "Servers/CarRentingSystem/CarRentingSystem.Identity/CarRentingSystem.Identity.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "Servers/CarRentingSystem/CarRentingSystem.Identity/CarRentingSystem.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentingSystem.Identity.dll"]