#!/usr/bin/env bash

rm -rf ./build
dotnet publish ./src/ghinstaller/ -r linux-x64 -o ./build/
cd ./build 
./ghi list-tag -o cloudfoundry -r cli -f v6.51.0
