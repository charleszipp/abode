output "dt_twins_host_name" {
    value = azurerm_digital_twins_instance.dt_twins.host_name
}
output "dt_twins_id" {
    value = azurerm_digital_twins_instance.dt_twins.id
}
output "evh_twins_listen_connection_string" {
    value = azurerm_eventhub_authorization_rule.evh_twins_ar_send.primary_connection_string
}