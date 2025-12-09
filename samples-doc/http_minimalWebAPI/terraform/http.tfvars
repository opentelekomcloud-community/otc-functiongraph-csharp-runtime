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
# set as env var TF_VAR_API_GATEWAY_INSTANCE_ID or uncomment and set here
#API_GATEWAY_INSTANCE_ID="YOUR_API_GATEWAY_INSTANCE_ID"