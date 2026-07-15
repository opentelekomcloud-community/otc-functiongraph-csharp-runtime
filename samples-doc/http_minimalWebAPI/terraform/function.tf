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


  ###### relevant part for deploy function code from obs file ######
  code_type = "obs"
  code_url = format("https://%s/%s/%s",
    opentelekomcloud_obs_bucket.codebucket.bucket_domain_name,
    "code",
    basename(var.zip_file_local)
  )
  # on change of the code object etag (hash) new code  version will be deployed.
  source_code_hash = opentelekomcloud_obs_bucket_object.code_object.etag
  ###### ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ ######

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
