#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CoordinateRegistration.Api/CoordinateRegistration.Api.csproj", "CoordinateRegistration.Api/"]
COPY ["CoordinateRegistration.Application/CoordinateRegistration.Application.csproj", "CoordinateRegistration.Application/"]
COPY ["CoordinateRegistration.Persistence/CoordinateRegistration.Persistence.csproj", "CoordinateRegistration.Persistence/"]
COPY ["CoordinateRegistration.Domain/CoordinateRegistration.Domain.csproj", "CoordinateRegistration.Domain/"]
RUN dotnet restore "CoordinateRegistration.Api/CoordinateRegistration.Api.csproj"
COPY . .
WORKDIR "/src/CoordinateRegistration.Api"
RUN dotnet build "CoordinateRegistration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoordinateRegistration.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoordinateRegistration.Api.dll"]