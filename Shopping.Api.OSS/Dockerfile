#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shopping.Api.OSS/Shopping.Api.OSS.csproj", "Shopping.Api.OSS/"]
COPY ["Shopping.Framework.Web/Shopping.Framework.Web.csproj", "Shopping.Framework.Web/"]
COPY ["Shopping.Framework.Domain/Shopping.Framework.Domain.csproj", "Shopping.Framework.Domain/"]
COPY ["Shopping.Framework.Common/Shopping.Framework.Common.csproj", "Shopping.Framework.Common/"]
RUN dotnet restore "Shopping.Api.OSS/Shopping.Api.OSS.csproj"
COPY . .
WORKDIR "/src/Shopping.Api.OSS"
RUN dotnet build "Shopping.Api.OSS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.OSS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /app/wwwroot
ENTRYPOINT ["dotnet", "Shopping.Api.OSS.dll"]
