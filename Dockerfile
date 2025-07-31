# Use the official .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY PUT_Backend/PUT_Backend.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY PUT_Backend/. ./

# Build the application
RUN dotnet build -c Release -o out

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o out

# Use the ASP.NET Core runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory
WORKDIR /app

# Copy the published output from the previous stage
COPY --from=publish /app/out .

# Create a non-root user and switch to it for security
RUN adduser --disabled-password appuser
USER appuser

# Expose the necessary port (default for ASP.NET Core)
EXPOSE 80

# Set the environment variable for ASP.NET Core
ENV ASPNETCORE_ENVIRONMENT=Production

# Start the application
ENTRYPOINT ["dotnet", "PUT_Backend.dll"]