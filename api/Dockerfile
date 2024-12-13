FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ForecastAlertService/*.csproj ./
RUN dotnet restore

# Copy the remaining source code and build the application
COPY ForecastAlertService/. ./
RUN dotnet publish -c Release -o /out

# Use a lightweight runtime image for deployment
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the built application
COPY --from=build /out ./

# Copy TLS certificates into the container
COPY certs/forecast_alert_api.p12 /certs/forecast_alert_api.p12
COPY certs/forecast_alert_api.crt /certs/forecast_alert_api.crt

# Set environment variables for TLS
ENV DOTNET_KESTREL__ENDPOINTS__HTTPS__URLS="https://*:8080"
ENV DOTNET_KESTREL__ENDPOINTS__HTTPS__CERTIFICATE__PATH="/certs/forecast_alert_api.p12"
ENV DOTNET_KESTREL__ENDPOINTS__HTTPS__CERTIFICATE__PASSWORD="changeit"

# Expose HTTPS port
EXPOSE 8080

# Set the entry point
ENTRYPOINT ["dotnet", "ForecastAlertService.dll"]