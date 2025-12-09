output "API_GATEWAY_TRIGGER_URL" {
  description = "The URL of the API Gateway triggering the FunctionGraph function"
  value       = format("https://%s.apic.%s.otc.t-systems.com", 
                opentelekomcloud_apigw_group_v2.group1.id , 
                opentelekomcloud_apigw_group_v2.group1.region)
  
}