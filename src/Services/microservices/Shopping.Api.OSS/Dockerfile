#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/Shopping.Api.OSS/Shopping.Api.OSS.csproj", "microservices/Shopping.Api.OSS/"]
COPY ["frameworks/Shopping.Framework.Web/Shopping.Framework.Web.csproj", "frameworks/Shopping.Framework.Web/"]
COPY ["frameworks/Shopping.Framework.Domain/Shopping.Framework.Domain.csproj", "frameworks/Shopping.Framework.Domain/"]
COPY ["frameworks/Shopping.Framework.Common/Shopping.Framework.Common.csproj", "frameworks/Shopping.Framework.Common/"]
RUN dotnet restore "microservices/Shopping.Api.OSS/Shopping.Api.OSS.csproj"
COPY . .
WORKDIR "/src/microservices/Shopping.Api.OSS"
RUN dotnet build "Shopping.Api.OSS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.OSS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /app/wwwroot
ENTRYPOINT ["dotnet", "Shopping.Api.OSS.dll"]