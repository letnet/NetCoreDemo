#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NetCoreTemplate.Web/NetCoreTemplate.Web.csproj", "NetCoreTemplate.Web/"]
COPY ["NetCoreTemplate.Domain/NetCoreTemplate.Domain.csproj", "NetCoreTemplate.Domain/"]
COPY ["NetCoreTemplate.Infrastructure/NetCoreTemplate.Infrastructure.csproj", "NetCoreTemplate.Infrastructure/"]
COPY ["NetCoreTemplate.Application/NetCoreTemplate.Application.csproj", "NetCoreTemplate.Application/"]
COPY ["NetCoreTemplate.Utility/NetCoreTemplate.Utility.csproj", "NetCoreTemplate.Utility/"]
RUN dotnet restore "NetCoreTemplate.Web/NetCoreTemplate.Web.csproj"
COPY . .
WORKDIR "/src/NetCoreTemplate.Web"
RUN dotnet build "NetCoreTemplate.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreTemplate.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreTemplate.Web.dll"]