# ===== Build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie la solution API
COPY . ./

# 👉 Si ton projet ne s'appelle pas api.csproj, remplace-le ci-dessous
RUN dotnet restore ./api.csproj
RUN dotnet publish ./api.csproj -c Release -o /app/publish \
    /p:SelfContained=false /p:UseAppHost=false /p:PublishAot=false /p:AssemblyName=app

# ===== Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
COPY --from=build /app/publish ./

# 👉 Ici on lance le DLL, pas un binaire natif
ENTRYPOINT ["dotnet", "api.dll"]
