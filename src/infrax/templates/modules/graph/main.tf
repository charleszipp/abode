locals {
  suffix   = "-gph-${var.env}-${var.location_suffix}-${var.app}"
  suffix_2 = "gph${var.env}${var.location_suffix}${var.app}"
}
resource "azurerm_resource_group" "rg_graph" {
  name     = "rg${local.suffix}"
  location = var.location
}

## FUNC STORAGE
resource "azurerm_storage_account" "st_graph" {
  name                     = "st${local.suffix_2}"
  resource_group_name      = azurerm_resource_group.rg_graph.name
  location                 = azurerm_resource_group.rg_graph.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

## FUNC MANAGED IDENTITY
resource "azurerm_user_assigned_identity" "id_graph" {
  name                = "id${local.suffix}"
  resource_group_name = azurerm_resource_group.rg_graph.name
  location            = azurerm_resource_group.rg_graph.location
}

## FUNC SERVICE PLAN
resource "azurerm_app_service_plan" "asp_graph" {
  name                = "asp${local.suffix}"
  location            = azurerm_resource_group.rg_graph.location
  resource_group_name = azurerm_resource_group.rg_graph.name
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "fn_graph" {
  name                       = "fn${local.suffix}"
  location                   = azurerm_resource_group.rg_graph.location
  resource_group_name        = azurerm_resource_group.rg_graph.name
  app_service_plan_id        = azurerm_app_service_plan.asp_graph.id
  storage_account_name       = azurerm_storage_account.st_graph.name
  storage_account_access_key = azurerm_storage_account.st_graph.primary_access_key
  os_type                    = "linux"

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY" = var.ai_instrumentation_key,
    "adt_instance_url"               = "https://${var.dt_twins_host_name}"
    "evh_twins_connection_string"    = var.evh_twins_listen_connection_string
  }

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.id_graph.id]
  }
}

### Grant func MI access to twin instance
resource "azurerm_role_assignment" "id_graph_dt_twins_data_owner" {
  scope                = var.dt_twins_id
  role_definition_name = "Azure Digital Twins Data Owner"
  principal_id         = azurerm_user_assigned_identity.id_graph.principal_id
}
