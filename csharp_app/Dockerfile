# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ./CsharpApp.csproj .
RUN dotnet restore ./CsharpApp.csproj


COPY . .

# Publish to a dedicated /app/publish directory
RUN dotnet publish CsharpApp.csproj -c Release -o /app/publish --no-restore

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CsharpApp.dll"]