#!/bin/bash


git chekcout master
git resert --hard origin/master


rm -rf bin publish || true
dotnet publish "src/wsmcbl.src.csproj" -c Release -o publish
