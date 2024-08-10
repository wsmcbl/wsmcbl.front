#!/bin/bash

rm -rf bin publish || true
cd src && dotnet publish "wsmcbl.src.csproj" -c Release -o ../publish
