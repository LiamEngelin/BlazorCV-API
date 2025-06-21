# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BlazorCV-API.csproj", "./"]
RUN dotnet restore "BlazorCV-API.csproj"
COPY . ./
RUN dotnet publish "BlazorCV-API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Gör att appen lyssnar på port 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "BlazorCV-API.dll"]