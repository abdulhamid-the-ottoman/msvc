# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files first to leverage Docker layer caching
COPY CleanInventory.sln .
COPY src/CleanInventory.Api/CleanInventory.Api.csproj src/CleanInventory.Api/
COPY src/CleanInventory.Application/CleanInventory.Application.csproj src/CleanInventory.Application/
COPY src/CleanInventory.Domain/CleanInventory.Domain.csproj src/CleanInventory.Domain/
COPY src/CleanInventory.Infrastructure/CleanInventory.Infrastructure.csproj src/CleanInventory.Infrastructure/
# NOTE: For CleanGroceryStore, change the paths above to match its project names.


RUN apt-get update && apt-get install -y --no-install-recommends ca-certificates
COPY ./certs/corp-root.crt /usr/local/share/ca-certificates/corp-root.crt
COPY ./certs/corp-sub.crt /usr/local/share/ca-certificates/corp-sub.crt
RUN update-ca-certificates

RUN dotnet restore CleanInventory.sln
# NOTE: For CleanGroceryStore, change the line above to: RUN dotnet restore CleanGroceryStore.sln

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/CleanInventory.Api"
# NOTE: For CleanGroceryStore, change WORKDIR to "/src/src/CleanGroceryStore.Api"

# Publish the API project
RUN dotnet publish -c Release -o /app/publish --no-restore

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CleanInventory.Api.dll"]
# NOTE: For CleanGroceryStore, change ENTRYPOINT to "CleanGroceryStore.Api.dll"