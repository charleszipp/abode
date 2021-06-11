locals {
  suffix   = "-twins-${var.env}-${var.location_suffix}-${var.app}"
  suffix_2 = "twins${var.env}${var.location_suffix}${var.app}"
}

data "azurerm_client_config" "current" {
}

resource "azurerm_resource_group" "rg_twins" {
  name     = "rg${local.suffix}"
  location = var.location
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

### Grant current identity access to twin instance
resource "azurerm_role_assignment" "id_current_dt_twins_data_owner" {
  scope                = azurerm_digital_twins_instance.dt_twins.id
  role_definition_name = "Azure Digital Twins Data Owner"
  principal_id         = data.azurerm_client_config.current.object_id
}

# EVENT ROUTE
resource "azurerm_eventhub" "evh_twins" {
  name                = "evh${local.suffix}"
  namespace_name      = var.evh_namespace
  resource_group_name = var.rg_core_name
  partition_count     = 2
  message_retention   = 1
}

resource "azurerm_eventhub_authorization_rule" "evh_twins_ar_send" {
  name                = "evh-twins-ar-send"
  namespace_name      = var.evh_namespace
  eventhub_name       = azurerm_eventhub.evh_twins.name
  resource_group_name = var.rg_core_name

  listen = false
  send   = true
  manage = false
}

resource "azurerm_digital_twins_endpoint_eventhub" "ep_twins_evh_core" {
  name                                 = "ep-twins-evh-core"
  digital_twins_id                     = azurerm_digital_twins_instance.dt_twins.id
  eventhub_primary_connection_string   = azurerm_eventhub_authorization_rule.evh_twins_ar_send.primary_connection_string
  eventhub_secondary_connection_string = azurerm_eventhub_authorization_rule.evh_twins_ar_send.secondary_connection_string
  
  provisioner "local-exec" {
    command = "az dt route create -n ${azurerm_digital_twins_instance.dt_twins.name} --en ${azurerm_digital_twins_endpoint_eventhub.ep_twins_evh_core.name} --rn ert-twins --filter \"type = 'Microsoft.DigitalTwins.Twin.Create' OR type = 'Microsoft.DigitalTwins.Twin.Update' OR type = 'Microsoft.DigitalTwins.Twin.Delete' OR type = 'Microsoft.DigitalTwins.Relationship.Create' OR type = 'Microsoft.DigitalTwins.Relationship.Update' OR type = 'Microsoft.DigitalTwins.Relationship.Delete' OR type = 'microsoft.iot.telemetry'\""
  }
}