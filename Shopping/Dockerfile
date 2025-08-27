# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY Shopping.sln .
COPY src/Shopping.ConsoleUI/Shopping.ConsoleUI.csproj src/Shopping.ConsoleUI/
COPY src/Shopping.Application/Shopping.Application.csproj src/Shopping.Application/
COPY src/Shopping.Domain/Shopping.Domain.csproj src/Shopping.Domain/
COPY src/Shopping.Infrastructure/Shopping.Infrastructure.csproj src/Shopping.Infrastructure/

RUN apt-get update && apt-get install -y --no-install-recommends ca-certificates
COPY ./certs/corp-root.crt /usr/local/share/ca-certificates/corp-root.crt
COPY ./certs/corp-sub.crt /usr/local/share/ca-certificates/corp-sub.crt
RUN update-ca-certificates

# Restore dependencies
RUN dotnet restore Shopping.sln

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/Shopping.ConsoleUI"

# Publish the Console project
RUN dotnet publish -c Release -o /app/publish --no-restore

# Final Stage
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Shopping.ConsoleUI.dll"]