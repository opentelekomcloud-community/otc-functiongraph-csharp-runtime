##########################################################
# Create Function
##########################################################
resource "opentelekomcloud_fgs_function_v2" "MyFunction" {
  depends_on = [opentelekomcloud_obs_bucket_object.code_object]
  name       = format("%s_%s", var.prefix, var.function_name)
  app        = "default"

  handler = var.function_handler_name

  description      = "Minimal WebAPI deployed with terraform."
  memory_size      = 256
  timeout          = 30
  max_instance_num = 10

  runtime = var.function_runtime

  user_data = jsonencode({
    "USE_SWAGGER_UI" = var.use_swagger_ui
  })


  # -------------------------------------------------------------- #
  # Use code uploaded to OBS as ZIP file 
  # see code in code_from_obs_bucket.tf
  code_type = "obs"
  code_url = format("https://%s/%s/%s",
    opentelekomcloud_obs_bucket.codebucket.bucket_domain_name,
    "code",
    basename(var.zip_file_local)
  )
  # ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ #

  log_group_id   = opentelekomcloud_lts_group_v2.MyLogGroup.id
  log_group_name = opentelekomcloud_lts_group_v2.MyLogGroup.group_name

  log_topic_id   = opentelekomcloud_lts_stream_v2.MyLogStream.id
  log_topic_name = opentelekomcloud_lts_stream_v2.MyLogStream.stream_name


  tags = {
    "app_group" = var.tag_app_group
  }
}

##########################################################
# Create Log Group
##########################################################
resource "opentelekomcloud_lts_group_v2" "MyLogGroup" {
  group_name  = format("%s_%s_%s", var.prefix, var.function_name, "log_group")
  ttl_in_days = 1

  tags = {
    "app_group" = var.tag_app_group
  }
}

##########################################################
# Create Log Stream
##########################################################
resource "opentelekomcloud_lts_stream_v2" "MyLogStream" {
  group_id    = opentelekomcloud_lts_group_v2.MyLogGroup.id
  stream_name = format("%s_%s_%s", var.prefix, var.function_name, "log_stream")

  tags = {
    "app_group" = var.tag_app_group
  }
}


