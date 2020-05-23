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
mkdir -p ./tmp
cd ./tmp
wget https://dl.google.com/go/go1.10.4.linux-amd64.tar.gz
sudo tar -xvf go1.10.4.linux-amd64.tar.gz
sudo mv go /usr/local
cd ..

./ghi download-release -o gobuffalo -r packr -f v2.7.1 -t
./ghi untar -t v2.7.1.tar -o ./v2.7.1
./ghi build-go -a get -d ./v2.7.1/gobuffalo-packr-4b4a3c4/v2/packr2/ || true
./ghi build-go -d ./v2.7.1/gobuffalo-packr-4b4a3c4/v2/packr2/ -o ../../../../packr2
./pack2 --help

# Building a DOTNET app
#TODO 

echo "Passed!"