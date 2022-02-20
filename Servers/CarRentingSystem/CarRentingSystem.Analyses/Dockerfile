#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarRentingSystem.Analyses/CarRentingSystem.Analyses.csproj", "CarRentingSystem.Analyses/"]
RUN dotnet restore "CarRentingSystem.Analyses/CarRentingSystem.Analyses.csproj"
COPY . .
WORKDIR "/src/CarRentingSystem.Analyses"
RUN dotnet build "CarRentingSystem.Analyses.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRentingSystem.Analyses.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentingSystem.Analyses.dll"]