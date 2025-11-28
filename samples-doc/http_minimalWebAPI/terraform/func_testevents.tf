##########################################################
# Create Test Event
##########################################################
resource "opentelekomcloud_fgs_event_v2" "apievent_hello" {
  function_urn = opentelekomcloud_fgs_function_v2.MyFunction.urn
  name         = "hello-event"
  content      = filebase64("${path.module}/../resources/apievent_hello.json")
}

##########################################################
# Create Test Event
##########################################################
resource "opentelekomcloud_fgs_event_v2" "apievent_root" {
  function_urn = opentelekomcloud_fgs_function_v2.MyFunction.urn
  name         = "root-event"
  content      = filebase64("${path.module}/../resources/apievent_root.json")
}