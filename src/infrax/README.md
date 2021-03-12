# Abode Infrax

This directory contains the infrastructure templates required to deploy the solution.

## Setup Configuration

Create `.devcontainer/devcontainer.env` and populate the necessary configuration values. The following example below can be used to create the file.

```shell
# the short name for the application
APP=abode

# the azure location name (i.e. eastus, westus, westeurope)
LOCATION=eastus

# a 3-4 character abbreviated name for the location
LOCATION_SHORT=usea

# the name of the environment (i.e. dev, test, qa, prod)
ENV=dev
```

## Open as Dev Container

`CTRL+SHIFT+P` -> `Remote Containers: [Rebuild and ] Reopen in Container`

## Run Az Login

Execute `az login` and `az account show -s <my-subscription-id>`

## Create Terraform Remote Backend

`CTRL+SHIFT+P` -> `Tasks: Run Task` -> `terraform create backend`

## Terraform Init

`CTRL+SHIFT+P` -> `Tasks: Run Task` -> `terraform init`