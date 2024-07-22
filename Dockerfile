# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /FrontCbl

# Copiamos los archivos del proyecto
COPY ./*.csproj ./
RUN dotnet restore

# Copiamos todo
COPY . .
RUN dotnet publish -c Release -o out

# Fase final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /FrontCbl

# Exponemos los puertos necesarios
EXPOSE 80
EXPOSE 8080

COPY --from=build /FrontCbl/out .
ENTRYPOINT ["dotnet", "wsmcbl.front.dll"]



