output "rg_core_name" {
  value = azurerm_resource_group.rg_core.name
}
output "vnet_subnet_id" {
  value = azurerm_subnet.snet_core.id
}
output "kv_id" {
  value = azurerm_key_vault.kv_core.id
}
output "wksp_id" {
  value = azurerm_log_analytics_workspace.wksp_core.id
}
output "ai_instrumentation_key" {
  value = azurerm_application_insights.ai_core.instrumentation_key
}
output "evh_namespace"{
  value = azurerm_eventhub_namespace.evh_core.name 
}