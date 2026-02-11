# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["WowSpellDleAPI.csproj", "./"]
RUN dotnet restore "WowSpellDleAPI.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "WowSpellDleAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WowSpellDleAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5052

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WowSpellDleAPI.dll"]
