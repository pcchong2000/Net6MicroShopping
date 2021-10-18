#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pay.Api/Pay.Api.csproj", "Pay.Api/"]
COPY ["MicroShoping.Domain/MicroShoping.DomainBase.csproj", "MicroShoping.Domain/"]
RUN dotnet restore "Pay.Api/Pay.Api.csproj"
COPY . .
WORKDIR "/src/Pay.Api"
RUN dotnet build "Pay.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pay.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pay.Api.dll"]
