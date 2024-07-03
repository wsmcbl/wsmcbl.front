#!/bin/bash

rm -rf bin publish || true
dotnet publish "wsmcbl.front.csproj" -c Release -o publish