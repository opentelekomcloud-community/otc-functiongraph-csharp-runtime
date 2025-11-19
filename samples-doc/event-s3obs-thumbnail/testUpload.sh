
# https://github.com/opentelekomcloud/obs-s3/blob/master/s3cmd/README.md

# For bucket name see output of terraform output
OBS_INPUT_BUCKET="csharp-doc-sample-event-s3obs-thumbnail-csharp-images"

s3cmd --access_key=${OTC_SDK_AK} --secret_key=${OTC_SDK_SK} --no-ssl \
  put ./test/resources/otc.jpg \
  s3://$OBS_INPUT_BUCKET/otc.jpg