#!/usr/bin/env bash

set -ex
set -o pipefail

rm -rf ./build
dotnet publish ./src/ghinstaller/ -r linux-x64 -o ./build/
