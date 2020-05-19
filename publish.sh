#!/usr/bin/env bash

rm -rf ./build
dotnet publish ./src/ghinstaller/ -r linux-x64 -o ./build/
cd ./build 
./ghi download-tag -o cloudfoundry -r cli
