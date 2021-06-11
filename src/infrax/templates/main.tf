terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=2.46.0"
    }
  }
  backend "azurerm" {}
}

provider "azurerm" {
  features {
  }
}

module "core" {
  source = "./modules/core"

  location        = var.location
  location_suffix = var.location_short
  env             = var.env
  app             = var.app
}

module "twins" {
  source = "./modules/twins"

  location               = var.location
  location_suffix        = var.location_short
  env                    = var.env
  app                    = var.app
  kv_id                  = module.core.kv_id
  wksp_id                = module.core.wksp_id
  ai_instrumentation_key = module.core.ai_instrumentation_key
  evh_namespace          = module.core.evh_namespace
  rg_core_name           = module.core.rg_core_name
}

module "graph" {
  source = "./modules/graph"

  location               = var.location
  location_suffix        = var.location_short
  env                    = var.env
  app                    = var.app
  dt_twins_host_name     = module.twins.dt_twins_host_name
  dt_twins_id            = module.twins.dt_twins_id
  ai_instrumentation_key = module.core.ai_instrumentation_key
}
