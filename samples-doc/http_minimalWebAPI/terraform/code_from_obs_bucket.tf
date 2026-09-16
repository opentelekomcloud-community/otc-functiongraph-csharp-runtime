###################################################################################
# Code bucket to store function code zip file
###################################################################################
resource "opentelekomcloud_obs_bucket" "codebucket" {
  bucket = format("%s-%s", var.prefix, "codebucket")
  acl    = "private"

  tags = {
    "app_group" = var.tag_app_group
  }
}

###################################################################################
# Code bucket object to upload function code zip file
###################################################################################
resource "opentelekomcloud_obs_bucket_object" "code_object" {
  bucket       = opentelekomcloud_obs_bucket.codebucket.bucket
  key          = format("%s/%s", "code", basename(var.zip_file_local))
  source       = var.zip_file_local
  etag         = filemd5(var.zip_file_local)
  content_type = "application/zip"
}
