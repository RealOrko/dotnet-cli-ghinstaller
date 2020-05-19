#!/usr/bin/env bash

rm -rf ./build
dotnet publish ./src/ghinstaller/ -r linux-x64 -o ./build/
cd ./build 
./ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a -af linux -an
