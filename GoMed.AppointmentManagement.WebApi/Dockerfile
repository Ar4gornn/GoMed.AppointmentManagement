FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GoMed.AppointmentManagement.WebApi/GoMed.AppointmentManagement.WebApi.csproj", "GoMed.AppointmentManagement.WebApi/"]
RUN dotnet restore "GoMed.AppointmentManagement.WebApi/GoMed.AppointmentManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/GoMed.AppointmentManagement.WebApi"
RUN dotnet build "GoMed.AppointmentManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GoMed.AppointmentManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoMed.AppointmentManagement.WebApi.dll"]
