#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tenant.Api/Tenant.Api.csproj", "Tenant.Api/"]
COPY ["MicroShoping.Domain/MicroShoping.DomainBase.csproj", "MicroShoping.Domain/"]
RUN dotnet restore "Tenant.Api/Tenant.Api.csproj"
COPY . .
WORKDIR "/src/Tenant.Api"
RUN dotnet build "Tenant.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tenant.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tenant.Api.dll"]
