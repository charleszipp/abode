locals {
  suffix   = "-twins-${var.env}-${var.location_suffix}-${var.app}"
  suffix_2 = "twins${var.env}${var.location_suffix}${var.app}"
}
resource "azurerm_resource_group" "rg_twins" {
  name     = "rg${local.suffix}"
  location = var.location
}

## STORAGE
resource "azurerm_storage_account" "st_twins" {
  name                     = "st${local.suffix_2}"
  resource_group_name      = azurerm_resource_group.rg_twins.name
  location                 = azurerm_resource_group.rg_twins.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

## IOT HUB
resource "azurerm_iothub" "iot_twins" {
  name                = "iot${local.suffix}"
  resource_group_name = azurerm_resource_group.rg_twins.name
  location            = azurerm_resource_group.rg_twins.location

  sku {
    name     = "S1"
    capacity = "1"
  }
}

## DIGITAL TWINS
resource "azurerm_digital_twins_instance" "dt_twins" {
  name                = "dt${local.suffix}"
  resource_group_name = azurerm_resource_group.rg_twins.name
  location            = azurerm_resource_group.rg_twins.location
}
resource "azurerm_key_vault_secret" "kv_core_dt_host_name" {
  name         = "dt-host-name"
  value        = azurerm_digital_twins_instance.dt_twins.host_name
  key_vault_id = var.kv_id
}
resource "azurerm_monitor_diagnostic_setting" "log_twins" {
  name                       = "log${local.suffix}"
  target_resource_id         = azurerm_digital_twins_instance.dt_twins.id
  log_analytics_workspace_id = var.wksp_id

  log { category = "DigitalTwinsOperation" }
  log { category = "EventRoutesOperation" }
  log { category = "ModelsOperation" }
  log { category = "QueryOperation" }
  metric { category = "AllMetrics" }
}

## TELEMETRY HANDLER FUNCTION
resource "azurerm_user_assigned_identity" "id_twins" {
  name = "id${local.suffix}"
  resource_group_name = azurerm_resource_group.rg_twins.name
  location            = azurerm_resource_group.rg_twins.location
}
resource "azurerm_app_service_plan" "asp_twins" {
  name                = "asp${local.suffix}"
  location            = azurerm_resource_group.rg_twins.location
  resource_group_name = azurerm_resource_group.rg_twins.name
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}
resource "azurerm_function_app" "fn_twins" {
  name                       = "fn${local.suffix}"
  location                   = azurerm_resource_group.rg_twins.location
  resource_group_name        = azurerm_resource_group.rg_twins.name
  app_service_plan_id        = azurerm_app_service_plan.asp_twins.id
  storage_account_name       = azurerm_storage_account.st_twins.name
  storage_account_access_key = azurerm_storage_account.st_twins.primary_access_key
  os_type                    = "linux"

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY" = var.ai_instrumentation_key,
    "DT_INSTANCE_URL"               = "https://${azurerm_digital_twins_instance.dt_twins.host_name}"
  }

  identity {
    type = "UserAssigned"
    identity_ids = [ azurerm_user_assigned_identity.id_twins.id ]
  }
}

### Assign function managed identity to 
resource "azurerm_role_assignment" "example" {
  scope                = azurerm_digital_twins_instance.dt_twins.id
  role_definition_name = "Azure Digital Twins Data Owner"
  principal_id         = azurerm_user_assigned_identity.id_twins.principal_id
}