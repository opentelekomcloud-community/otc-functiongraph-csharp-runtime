# Terraform variables for http_minimalWebAPI sample

# prefix of all resources
prefix="csharp" 

# name of the function (will be prefixed)
function_name="doc-sample-minimal-webapi"

# function handler name, for http functions always "bootstrap"
function_handler_name="bootstrap"

# function runtime, for http functions always "http"
function_runtime="http"

# path to local zip file to be deployed
zip_file_local="../src/http_minimalWebAPI_net8.0.zip"

# resources will be tagged with this app_group tag
tag_app_group="csharp-doc-sample-minimal-webapi"

# change to your API Gateway instance ID
api_gateway_instance_id="46af2dde65dc4323b0f1178c1a8a7926"