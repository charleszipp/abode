variable "location" {
  type        = string
  description = "The name of the target location"
}
variable "location_short" {
  type        = string
  description = "The abbreviated name of the target location"
}
variable "env" {
  type        = string
  description = "The short name of the target env (i.e. dev, staging, or prod)"
}
variable "app" {
  type        = string
  description = "The short name of the app"
}