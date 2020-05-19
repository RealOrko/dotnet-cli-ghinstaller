#!/usr/bin/env bash

set -ex
set -o pipefail

rm -rf ./build
dotnet publish ./src/ghinstaller/ -r linux-x64 -o ./build/
cd ./build 

./ghi list-tag -o cloudfoundry -r bosh-bootloader
./ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0
./ghi list-tag -o cloudfoundry -r bosh-bootloader -n
./ghi list-tag -o cloudfoundry -r bosh-bootloader -t
./ghi list-tag -o cloudfoundry -r bosh-bootloader -z
./ghi list-tag -o cloudfoundry -r bosh-bootloader -n -t -z
./ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -n
./ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
./ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
./ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -n -t -z
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t -z
./ghi list-release -o cloudfoundry -r bosh-bootloader
./ghi list-release -o cloudfoundry -r bosh-bootloader -n
./ghi list-release -o cloudfoundry -r bosh-bootloader -t
./ghi list-release -o cloudfoundry -r bosh-bootloader -z
./ghi list-release -o cloudfoundry -r bosh-bootloader -n -t -z
./ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -n
./ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
./ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
./ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a
./ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -n -t -z -a
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a -af linux
