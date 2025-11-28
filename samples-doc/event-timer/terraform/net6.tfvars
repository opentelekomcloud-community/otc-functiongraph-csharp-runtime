# Terraform variables for event-timer sample

# prefix of all resources
prefix="csharp" 

# name of the function (will be prefixed)
function_name="doc-sample-event-timer-csharp"

# function handler name, see handler.txt
function_handler_name="event_timer::src.Program::Handler"

# function runtime, use ".NET Core 6.0" in this sample
function_runtime="C#(.NET Core 6.0)"

# path to local zip file to be deployed created 
# by 'dotnet build' command
zip_file_local="../src/event_timer_net6.0.zip"

# resources will be tagged with this app_group tag
tag_app_group="csharp-doc-sample-event-timer"