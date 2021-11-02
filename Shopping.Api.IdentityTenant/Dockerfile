#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shopping.Api.IdentityTenant/Shopping.Api.IdentityTenant.csproj", "Shopping.Api.IdentityTenant/"]
COPY ["Shopping.Framework.EFCore/Shopping.Framework.EFCore.csproj", "Shopping.Framework.EFCore/"]
COPY ["Shopping.Framework.Domain/Shopping.Framework.Domain.csproj", "Shopping.Framework.Domain/"]
COPY ["Shopping.Framework.Common/Shopping.Framework.Common.csproj", "Shopping.Framework.Common/"]
COPY ["Shopping.Framework.Web/Shopping.Framework.Web.csproj", "Shopping.Framework.Web/"]
COPY ["Shopping.Framework.Application/Shopping.Framework.Application.csproj", "Shopping.Framework.Application/"]
RUN dotnet restore "Shopping.Api.IdentityTenant/Shopping.Api.IdentityTenant.csproj"
COPY . .
WORKDIR "/src/Shopping.Api.IdentityTenant"
RUN dotnet build "Shopping.Api.IdentityTenant.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.IdentityTenant.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Api.IdentityTenant.dll"]