#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Servers/CarRentingSystem/CarRentingSystem.Renting/CarRentingSystem.Renting.csproj", "CarRentingSystem.Renting/"]
COPY ["./Servers/CarRentingSystem/CarRentingSystem.Common/CarRentingSystem.Common.csproj", "CarRentingSystem.Common/"]
RUN dotnet restore "CarRentingSystem.Renting/CarRentingSystem.Renting.csproj"
COPY . .
WORKDIR "/src/CarRentingSystem.Renting"
RUN dotnet build "CarRentingSystem.Renting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRentingSystem.Renting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentingSystem.Renting.dll"]