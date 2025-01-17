# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID=1000
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GoMed.AppointmentManagement.WebApi/GoMed.AppointmentManagement.WebApi.csproj", "GoMed.AppointmentManagement.WebApi/"]
RUN dotnet restore "GoMed.AppointmentManagement.WebApi/GoMed.AppointmentManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/GoMed.AppointmentManagement.WebApi"
RUN dotnet build "GoMed.AppointmentManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "GoMed.AppointmentManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoMed.AppointmentManagement.WebApi.dll"]
