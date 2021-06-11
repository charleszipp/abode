locals {
  suffix = "-core-${var.env}-${var.location_suffix}-${var.app}"
}
data "azurerm_client_config" "current" {}
resource "azurerm_resource_group" "rg_core" {
  name     = "rg${local.suffix}"
  location = var.location
}
resource "azurerm_log_analytics_workspace" "wksp_core" {
  name                = "log${local.suffix}"
  location            = azurerm_resource_group.rg_core.location
  resource_group_name = azurerm_resource_group.rg_core.name
  sku                 = "PerGB2018"
}
resource "azurerm_application_insights" "ai_core" {
  name                = "ai${local.suffix}"
  location            = azurerm_resource_group.rg_core.location
  resource_group_name = azurerm_resource_group.rg_core.name
  application_type    = "web"
}
resource "azurerm_key_vault_secret" "kv_core_ai_key" {
  name         = "ai-instrumentation-key"
  value        = azurerm_application_insights.ai_core.instrumentation_key
  key_vault_id = azurerm_key_vault.kv_core.id
}
resource "azurerm_virtual_network" "vnet_core" {
  name                = "vnet${local.suffix}"
  location            = azurerm_resource_group.rg_core.location
  resource_group_name = azurerm_resource_group.rg_core.name
  address_space       = ["10.1.0.0/16"]
}
resource "azurerm_subnet" "snet_core" {
  name                 = "snet${local.suffix}"
  resource_group_name  = azurerm_resource_group.rg_core.name
  address_prefixes     = ["10.1.0.0/24"]
  virtual_network_name = azurerm_virtual_network.vnet_core.name
}
resource "azurerm_key_vault" "kv_core" {
  name                        = "kv${local.suffix}"
  location                    = azurerm_resource_group.rg_core.location
  resource_group_name         = azurerm_resource_group.rg_core.name
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  sku_name                    = "standard"

  # todo: this should probably be deny but, if set to deny then need to find out a way to add the ip address of the build agent so it can get & set secrets
  network_acls {
    bypass         = "AzureServices"
    default_action = "Allow"
  }

  access_policy {
    tenant_id = data.azurerm_client_config.current.tenant_id
    object_id = data.azurerm_client_config.current.object_id

    secret_permissions = [
      "get",
      "list",
      "set",
      "delete",
      "purge"
    ]
  }
}

resource "azurerm_eventhub_namespace" "evh_core" {
  name                = "evh${local.suffix}"
  location            = azurerm_resource_group.rg_core.location
  resource_group_name = azurerm_resource_group.rg_core.name
  sku                 = "Basic"
  capacity            = 1
}

resource "azurerm_key_vault_secret" "kv_core_evhn_core_connection_string" {
  name         = "evh-core-connection-string"
  value        = azurerm_eventhub_namespace.evh_core.default_primary_connection_string
  key_vault_id = azurerm_key_vault.kv_core.id
}