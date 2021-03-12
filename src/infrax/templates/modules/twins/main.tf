locals {
  suffix = "-twins-${var.env}-${var.location_suffix}-${var.app}"
}
resource "azurerm_resource_group" "rg_twins" {
  name     = "rg${local.suffix}"
  location = var.location
}
resource "azurerm_iothub" "iot_twins" {
  name                = "iot${local.suffix}"
  resource_group_name = azurerm_resource_group.rg_twins.name
  location            = azurerm_resource_group.rg_twins.location

  sku {
    name     = "S1"
    capacity = "1"
  }
}
resource "azurerm_digital_twins_instance" "adt_twins" {
  name                = "adt${local.suffix}"
  resource_group_name = azurerm_resource_group.rg_twins.name
  location            = azurerm_resource_group.rg_twins.location
}
resource "azurerm_key_vault_secret" "kv_core_adt_host_name" {
  name         = "adt-host-name"
  value        = azurerm_digital_twins_instance.adt_twins.host_name
  key_vault_id = var.kv_id
}
resource "azurerm_monitor_diagnostic_setting" "log_twins" {
  name                       = "log${local.suffix}"
  target_resource_id         = azurerm_digital_twins_instance.adt_twins.id
  log_analytics_workspace_id = var.wksp_id

  log { category = "DigitalTwinsOperation" }
  log { category = "EventRoutesOperation" }
  log { category = "ModelsOperation" }
  log { category = "QueryOperation" }
  metric { category = "AllMetrics" }
}