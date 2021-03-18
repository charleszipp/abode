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
variable "kv_id" {
  type        = string
  description = "The key vault id"
}
variable "wksp_id" {
  type        = string
  description = "The log analytics workspace id"
}
variable "ai_instrumentation_key" {
  type        = string
  description = "The application insights instrumentation key"
}