##########################################################
# Create Function
##########################################################
resource "opentelekomcloud_fgs_function_v2" "MyFunction" {
  name = format("%s_%s", var.prefix, var.function_name)
  app  = "default"

  handler          = var.function_handler_name

  description      = "Sample for timer triggered FunctionGraph using terraform."
  memory_size      = 128
  timeout          = 30
  max_instance_num = 10

  runtime       = var.function_runtime
  
  # -------------------------------------------------------------- #
  # Upload code as ZIP file directly
  code_type     = "zip"
  func_code     = filebase64(var.zip_file_local)
  code_filename = basename(var.zip_file_local)
  # ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ #

  log_group_id   = opentelekomcloud_lts_group_v2.MyLogGroup.id
  log_group_name = opentelekomcloud_lts_group_v2.MyLogGroup.group_name

  log_topic_id   = opentelekomcloud_lts_stream_v2.MyLogStream.id
  log_topic_name = opentelekomcloud_lts_stream_v2.MyLogStream.stream_name


  tags = {
    "app_group" = var.tag_app_group
  }
}

output "MY_FUNCTION_URN" {
  value = opentelekomcloud_fgs_function_v2.MyFunction.urn
}

output "MY_FUNCTION_VERSION" {
  value = opentelekomcloud_fgs_function_v2.MyFunction.version
}
