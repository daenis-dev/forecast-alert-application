# Forecast Alert Application

### Getting Started

- Configure TLS for the project

  - Create the directory for the certificate files

    ```
    mkdir api/certs && cd api/certs
    ```

  - Create the private key and the certificate

    ```
    openssl req \
    -newkey rsa:2048 \
    -nodes \
    -x509 \
    -days 36500 -nodes \
    -addext "subjectAltName = IP.1:192.168.xx.xx" \
    -keyout forecast_alert_api.key \
    -out forecast_alert_api.crt
    ```

    - Use the local IP address in place of 192.168.xx.xx

  - Package the key and the certificate

    ```
    openssl pkcs12 -export -out forecast_alert_api.p12 -inkey forecast_alert_api.key -in forecast_alert_api.crt -name "forecast_alert_api_cert" -passout pass:changeit
    ```



### Running the Unit Tests

- Build the project with Docker

  ```
  docker compose build --no-cache
  ```

- Run the tests

  ```
  dotnet test api/ForecastAlertService.Tests
  ```

  

### Running the Application

- Build and run the project with Docker

  ```
  docker compose up
  ```

- Navigate to https://localhost:8080/alerts, select 'Advanced' and proceed to the website



### API

- Create Alert

  - **Method:** POST

  - **URL:** https://localhost:8080/alerts

  - **Request:**

    ```
    curl -X POST https://localhost:8080/alertSpecifications -H "Content-Type: application/json" -d "{\"name\": \"Wear Shoes\", \"isUrgent\": false}"
    ```

- Get all Alerts

  - **Method:** GET

  - **URL:** https://localhost:8080/alerts

  - **Response:**

    ```json
    [
      {
        "id": 1,
        "name": "Bring an Umbrella",
        "isUrgent": true,
        "createdDateTimeUtc": "2024-12-12T04:34:15.749352Z",
        "modifiedDateTimeUtc": "2024-12-12T04:34:15.749352Z"
      }
    ]
    ```

- Get all Specifications

  - **Method:** GET

  - **URL:** https://localhost:8080/specifications

  - **Response:**

    ```json
    [
      {
        "id": 1,
        "name": "wind speed in miles per hour"
      }
    ]
    ```

- Get all Operators

  - **Method:** GET

  - **URL:** https://localhost:8080/operators

  - **Response:**

    ```json
    [
      {
        "id": 1,
        "name": "equal to",
        "symbol": "="
      }
    ]
    ```

- Get all Alert Specifications

  - **Method:** GET

  - **URL:** https://localhost:8080/alertSpecifications

  - **Response:**

    ```json
    [
      {
        "id": 1,
        "alertId": 1,
        "alertName": "Bring an Umbrella",
        "specificationId": 3,
        "specificationName": "chance of precipitation percentage",
        "operatorId": 6,
        "operatorSymbol": "\u003E=",
        "thresholdValue": 50,
        "createdDateTimeUtc": "2024-12-12T04:34:15.777931Z",
        "modifiedDateTimeUtc": "2024-12-12T04:34:15.777931Z"
      }
    ]
    ```

- Create Alert Specification

  - **Method:** POST

  - **URL:** https://localhost:8080/alertSpecifications

  - **Request:**

    ```
    curl -X POST https://localhost:8080/alertSpecifications -H "Content-Type: application/json" -d "{\"alertId\": 3, \"specificationId\": 4, \"operatorId\": 6, \"thresholdValue\": 80}"
    ```

    