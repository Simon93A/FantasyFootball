# STEG 1.
# Vilket sdk paket för dotnet ska vi använda
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# STEG 2.
# Skapa en katalog dit våra filer och våra kommandon ska köras...
WORKDIR  /App

# STEG 3.
# Kopiera alla filer ifrån vårt projekt...
COPY . /App

# STEG 4.
# Här måste vi nu restore
RUN dotnet restore

# STEG 5.
# Nu måste vi skapa en release build.
RUN dotnet publish -c Release -o out

# STEG 6.
# Ange vilket SDK som ska användas.
FROM mcr.microsoft.com/dotnet/sdk:8.0

# STEG 7.
# Indikerar var vår arbetskatalog ska heta
WORKDIR /App

# STEG 8.
# Kopiera filerna som skapades i vårt build/publish steget
COPY --from=build-env /App/out .

# STEG 9.
# Sista steget i Dockerfile
ENTRYPOINT [ "dotnet", "fantacyfotball-api.dll" ]