#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shopping.Api.IdentityMember/Shopping.Api.IdentityMember.csproj", "Shopping.Api.IdentityMember/"]
COPY ["Shopping.Framework.Domain/Shopping.Framework.Domain.csproj", "Shopping.Framework.Domain/"]
COPY ["Shopping.Framework.Common/Shopping.Framework.Common.csproj", "Shopping.Framework.Common/"]
COPY ["Shopping.Framework.EFCore/Shopping.Framework.EFCore.csproj", "Shopping.Framework.EFCore/"]
COPY ["Shopping.Framework.Web/Shopping.Framework.Web.csproj", "Shopping.Framework.Web/"]
RUN dotnet restore "Shopping.Api.IdentityMember/Shopping.Api.IdentityMember.csproj"
COPY . .
WORKDIR "/src/Shopping.Api.IdentityMember"
RUN dotnet build "Shopping.Api.IdentityMember.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.Api.IdentityMember.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.Api.IdentityMember.dll"]