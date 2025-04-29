# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
 
# Set working directory
WORKDIR /src
 
# Copy project files
COPY ./SiteAPI/SiteAPI.csproj ./SiteAPI/
COPY ./SiteAPI.Applications/SiteAPI.Applications.csproj ./SiteAPI.Applications/
COPY ./SiteAPI.Domain/SiteAPI.Domain.csproj ./SiteAPI.Domain/
COPY ./SiteAPI.Infrastructer/SiteAPI.Infrastructer.csproj ./SiteAPI.Infrastructer/
 
# Restore dependencies
RUN dotnet restore ./SiteAPI/SiteAPI.csproj
 
# Copy all source code
COPY . .
 
# Publish the app
RUN dotnet publish ./SiteAPI/SiteAPI.csproj -c Release -o /app
 
# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
 
WORKDIR /app
EXPOSE 80
 
# Copy published files from build stage
COPY --from=build /app .
 
# Run the API
ENTRYPOINT ["dotnet", "SiteAPI.dll"]