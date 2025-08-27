# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files first to leverage Docker layer caching
COPY CleanGroceryStore.sln .
COPY src/CleanGroceryStore.Api/CleanGroceryStore.Api.csproj src/CleanGroceryStore.Api/
COPY src/CleanGroceryStore.Application/CleanGroceryStore.Application.csproj src/CleanGroceryStore.Application/
COPY src/CleanGroceryStore.Domain/CleanGroceryStore.Domain.csproj src/CleanGroceryStore.Domain/
COPY src/CleanGroceryStore.Infrastructure/CleanGroceryStore.Infrastructure.csproj src/CleanGroceryStore.Infrastructure/


RUN apt-get update && apt-get install -y --no-install-recommends ca-certificates
COPY ./certs/corp-root.crt /usr/local/share/ca-certificates/corp-root.crt
COPY ./certs/corp-sub.crt /usr/local/share/ca-certificates/corp-sub.crt
RUN update-ca-certificates

# Restore dependencies for the entire solution
RUN dotnet restore CleanGroceryStore.sln
# NOTE: For CleanGroceryStore, change the line above to: RUN dotnet restore CleanGroceryStore.sln

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/CleanGroceryStore.Api"
# NOTE: For CleanGroceryStore, change WORKDIR to "/src/src/CleanGroceryStore.Api"

# Publish the API project
RUN dotnet publish -c Release -o /app/publish --no-restore

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CleanGroceryStore.Api.dll"]
# NOTE: For CleanGroceryStore, change ENTRYPOINT to "CleanGroceryStore.Api.dll"