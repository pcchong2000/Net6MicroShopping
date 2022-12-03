#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shopping.Api.IdentityTenant/Shopping.Api.IdentityTenant.csproj", "Shopping.Api.IdentityTenant/"]
COPY ["Shopping.Framework.AccountApplication/Shopping.Framework.AccountApplication.csproj", "Shopping.Framework.AccountApplication/"]
COPY ["Shopping.Framework.DomainBase/Shopping.Framework.DomainBase.csproj", "Shopping.Framework.DomainBase/"]
COPY ["Shopping.Framework.Common/Shopping.Framework.Common.csproj", "Shopping.Framework.Common/"]
COPY ["Shopping.Framework.Web/Shopping.Framework.Web.csproj", "Shopping.Framework.Web/"]
RUN dotnet restore "Shopping.Api.IdentityTenant/Shopping.Api.IdentityTenant.csproj"
COPY . .
WORKDIR "/src/Shopping.Api.IdentityTenant"
RUN dotnet build "Shopping.Api.IdentityTenant.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.IdentityTenant.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Api.IdentityTenant.dll"]