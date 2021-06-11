variable "location" {
  type        = string
  description = "The name of the target location"
}
variable "location_suffix" {
  type        = string
  description = "The shortened suffix representation of the target location"
}
variable "env" {
  type        = string
  description = "The short name of the target env (i.e. dev, staging, or prod)"
}
variable "app" {
  type        = string
  description = "The short name of the app"
}
variable "ai_instrumentation_key" {
  type        = string
  description = "The application insights instrumentation key"
}
variable "dt_twins_host_name" {
  type        = string
  description = "The digital twins instance host name"
}
variable "dt_twins_id" {
  type        = string
  description = "The digital twins instance resource id"
}
variable "evh_twins_listen_connection_string"{
  type = string
  description = "Connection string to listen to the twins event hub"
}