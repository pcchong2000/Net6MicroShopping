#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IdentityTenant.Api/IdentityTenant.Api.csproj", "IdentityTenant.Api/"]
COPY ["MicroShoping.EFCoreBase/MicroShoping.EFCore.csproj", "MicroShoping.EFCoreBase/"]
COPY ["MicroShoping.Domain/MicroShoping.Domain.csproj", "MicroShoping.Domain/"]
COPY ["MicroShoping.Common/MicroShoping.Common.csproj", "MicroShoping.Common/"]
COPY ["MicroShoping.ApplicationBase/MicroShoping.Application.csproj", "MicroShoping.ApplicationBase/"]
RUN dotnet restore "IdentityTenant.Api/IdentityTenant.Api.csproj"
COPY . .
WORKDIR "/src/IdentityTenant.Api"
RUN dotnet build "IdentityTenant.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityTenant.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityTenant.Api.dll"]
