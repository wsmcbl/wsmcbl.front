FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /system

COPY ./src/System/*.csproj .
RUN dotnet restore

COPY ./src/System/ .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /system

COPY --from=build /system/out .
ENTRYPOINT ["dotnet", "wsmcbl.system.dll"]