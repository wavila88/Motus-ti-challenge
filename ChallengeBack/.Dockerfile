# ==========================================================
# STAGE 1: BUILD - Copies the entire solution and publishes the executable project
# ==========================================================

# ----------------------------------------------------------
# STAGE 1: Build
# ----------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia solo los archivos de la solución y los proyectos
COPY ChallengeBack.sln ./
COPY ChallengeBack/ChallengeBack.csproj ./ChallengeBack/
COPY Domain/Domain.csproj ./Domain/
COPY RepositorySQL/RepositorySQL.csproj ./RepositorySQL/

# Copia el resto de los archivos de los proyectos
COPY ChallengeBack/. ./ChallengeBack/
COPY Domain/. ./Domain/
COPY RepositorySQL/. ./RepositorySQL/

# Restaura dependencias
RUN dotnet restore "ChallengeBack.sln"

# Publica el proyecto principal
RUN dotnet publish ./ChallengeBack/ChallengeBack.csproj -c Release -o /out --no-restore

# ----------------------------------------------------------
# STAGE 2: Runtime
# ----------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Expone el puerto 80
ENV ASPNETCORE_URLS=http://+:80

# Copia los archivos publicados
COPY --from=build /out .

# Entry point
ENTRYPOINT ["dotnet", "ChallengeBack.dll"]
