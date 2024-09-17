#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MultipleDatabase/Ordering/MultipleDatabase.Ordering.Api/MultipleDatabase.Ordering.Api.csproj", "MultipleDatabase/Ordering/MultipleDatabase.Ordering.Api/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["MultipleDatabase/Ordering/MultipleDatabase.Ordering.Application/MultipleDatabase.Ordering.Application.csproj", "MultipleDatabase/Ordering/MultipleDatabase.Ordering.Application/"]
COPY ["MultipleDatabase/Ordering/MultipleDatabase.Ordering.Domain/MultipleDatabase.Ordering.Domain.csproj", "MultipleDatabase/Ordering/MultipleDatabase.Ordering.Domain/"]
COPY ["MultipleDatabase/Ordering/MultipleDatabase.Ordering.Persistence/MultipleDatabase.Ordering.Persistence.csproj", "MultipleDatabase/Ordering/MultipleDatabase.Ordering.Persistence/"]
RUN dotnet restore "./MultipleDatabase/Ordering/MultipleDatabase.Ordering.Api/MultipleDatabase.Ordering.Api.csproj"
COPY . .
WORKDIR "/src/MultipleDatabase/Ordering/MultipleDatabase.Ordering.Api"
RUN dotnet build "./MultipleDatabase.Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MultipleDatabase.Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultipleDatabase.Ordering.Api.dll"]