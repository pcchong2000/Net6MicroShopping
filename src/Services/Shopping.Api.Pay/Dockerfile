#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shopping.Api.Pay/Shopping.Api.Pay.csproj", "Shopping.Api.Pay/"]
COPY ["Shopping.Framework.Web/Shopping.Framework.Web.csproj", "Shopping.Framework.Web/"]
COPY ["Shopping.Framework.DomainBase/Shopping.Framework.DomainBase.csproj", "Shopping.Framework.DomainBase/"]
COPY ["Shopping.Framework.Common/Shopping.Framework.Common.csproj", "Shopping.Framework.Common/"]
RUN dotnet restore "Shopping.Api.Pay/Shopping.Api.Pay.csproj"
COPY . .
WORKDIR "/src/Shopping.Api.Pay"
RUN dotnet build "Shopping.Api.Pay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.Pay.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Api.Pay.dll"]