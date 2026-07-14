##########################################################
# Create Test Event
##########################################################
resource "opentelekomcloud_fgs_event_v2" "test_event_hello" {
  function_urn = opentelekomcloud_fgs_function_v2.MyFunction.urn
  name         = "TestEvent_hello"
  content = filebase64("../resources/apievent_hello.json")
}

resource "opentelekomcloud_fgs_event_v2" "test_event_root" {
  function_urn = opentelekomcloud_fgs_function_v2.MyFunction.urn
  name         = "TestEvent_root"
  content = filebase64("../resources/apievent_root.json")
}
