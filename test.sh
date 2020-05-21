#!/usr/bin/env bash

set -ex
set -o pipefail

cd ./build 

# Listing tags
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

# Downloading tags
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
./ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t -z

# Listing releases
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

# Downloading releases
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a
./ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a -af linux

# Building a go app
sudo apt install golang 
./ghi download-release -o cloudfoundry -r cli -f v6.51.0
./ghi untar -t v6.51.0.tar -o ./v6.51.0
./ghi build-go -a get -d ./v6.51.0/cloudfoundry-cli-2acd156/ || true
./ghi build-go -d ./v6.51.0/cloudfoundry-cli-2acd156/ -o ../../cli

# Building a DOTNET app
#TODO 

echo "Passed!"