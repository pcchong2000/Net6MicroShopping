#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Member.Api/Member.Api.csproj", "Member.Api/"]
COPY ["MicroShoping.Domain/MicroShoping.Domain.csproj", "MicroShoping.Domain/"]
RUN dotnet restore "Member.Api/Member.Api.csproj"
COPY . .
WORKDIR "/src/Member.Api"
RUN dotnet build "Member.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Member.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Member.Api.dll"]
