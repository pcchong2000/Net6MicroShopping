#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/Shopping.Api.Order/Shopping.Api.Order.csproj", "microservices/Shopping.Api.Order/"]
COPY ["frameworks/Shopping.Framework.Application/Shopping.Framework.Application.csproj", "frameworks/Shopping.Framework.Application/"]
COPY ["frameworks/Shopping.Framework.Domain/Shopping.Framework.Domain.csproj", "frameworks/Shopping.Framework.Domain/"]
COPY ["frameworks/Shopping.Framework.Common/Shopping.Framework.Common.csproj", "frameworks/Shopping.Framework.Common/"]
COPY ["frameworks/Shopping.Framework.EFCore/Shopping.Framework.EFCore.csproj", "frameworks/Shopping.Framework.EFCore/"]
COPY ["frameworks/Shopping.Framework.Web/Shopping.Framework.Web.csproj", "frameworks/Shopping.Framework.Web/"]
RUN dotnet restore "microservices/Shopping.Api.Order/Shopping.Api.Order.csproj"
COPY . .
WORKDIR "/src/microservices/Shopping.Api.Order"
RUN dotnet build "Shopping.Api.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.Order.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Api.Order.dll"]