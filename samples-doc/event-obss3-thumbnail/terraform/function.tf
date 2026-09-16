

##########################################################
# Create Function
##########################################################
resource "opentelekomcloud_fgs_function_v2" "MyFunction" {
  name             = format("%s_%s", var.prefix, var.function_name)
  app              = "default"
  agency           = opentelekomcloud_identity_agency_v3.agency.name
  handler          = var.function_handler_name
  initializer_handler = var.function_initializer_name
  initializer_timeout =  30

  description      = "Sample on how to create Thumbnails from images uploaded to OBS"
  memory_size      = 512
  timeout          = 30
  max_instance_num = 1
  runtime = var.function_runtime

  code_type     = "zip"
  func_code     = filebase64(var.zip_file_local)

  code_filename = basename(var.zip_file_local)

  log_group_id   = opentelekomcloud_lts_group_v2.MyLogGroup.id
  log_group_name = opentelekomcloud_lts_group_v2.MyLogGroup.group_name

  log_topic_id   = opentelekomcloud_lts_stream_v2.MyLogStream.id
  log_topic_name = opentelekomcloud_lts_stream_v2.MyLogStream.stream_name

  # set some environment variables
  user_data = jsonencode({
    "OUTPUT_BUCKET" : opentelekomcloud_s3_bucket.outbucket.bucket,
    "OBS_ENDPOINT" : "https://obs.otc.t-systems.com",
    # "RUNTIME_LOG_LEVEL" : "ERROR",
    # "RUNTIME_LOG_PATH" : "/tmp"
  })

  tags = {
    "app_group" = var.tag_app_group
  }

}
