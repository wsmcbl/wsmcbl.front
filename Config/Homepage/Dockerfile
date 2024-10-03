FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /homepage

COPY ./src/Homepage/*.csproj .
RUN dotnet restore

COPY ./src/Homepage/ .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /homepage

COPY --from=build /homepage/out .
ENTRYPOINT ["dotnet", "wsmcbl.homepage.dll"]