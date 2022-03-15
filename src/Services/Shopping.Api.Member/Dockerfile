#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shopping.Api.Member/Shopping.Api.Member.csproj", "Shopping.Api.Member/"]
COPY ["Shopping.Framework.EFCore/Shopping.Framework.EFCore.csproj", "Shopping.Framework.EFCore/"]
COPY ["Shopping.Framework.Domain/Shopping.Framework.Domain.csproj", "Shopping.Framework.Domain/"]
COPY ["Shopping.Framework.Common/Shopping.Framework.Common.csproj", "Shopping.Framework.Common/"]
COPY ["Shopping.Framework.Web/Shopping.Framework.Web.csproj", "Shopping.Framework.Web/"]
COPY ["Shopping.Framework.Application/Shopping.Framework.Application.csproj", "Shopping.Framework.Application/"]
RUN dotnet restore "Shopping.Api.Member/Shopping.Api.Member.csproj"
COPY . .
WORKDIR "/src/Shopping.Api.Member"
RUN dotnet build "Shopping.Api.Member.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.Member.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Api.Member.dll"]