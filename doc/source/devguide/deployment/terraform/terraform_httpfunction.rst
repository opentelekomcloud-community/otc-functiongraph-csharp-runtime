Deploying a HTTP Function using Terraform
=========================================

This section describes on how to deploy an HTTP Function using Terraform.

Prerequisite
------------

* Terraform configured according to :ref:`ref_terraform_setup`

Example
-------

An example for deploying a **HTTP Function** using Terraform can be found in:
:github_repo_master:`samples-doc/http_minimalWebAPI/terraform </samples-doc/http_minimalWebAPI/terraform>`.

This example deploys a minimal C# HTTP Function and demonstrates how to:

- upload the Function code as zip file to an OBS bucket (and update on changes),
  see: :github_repo_master:`code_from_obs_bucket.tf </samples-doc/http_minimalWebAPI/terraform/code_from_obs_bucket.tf>`

- create the Function using the code from the OBS bucket,
  see: :github_repo_master:`function.tf </samples-doc/http_minimalWebAPI/terraform/function.tf>`

  .. code-block:: terraform

      resource "opentelekomcloud_functiongraph_function_v2" "MyEventFunction" {
        ...
         code_type = "obs"
         code_url = format("https://%s/%s/%s",
              opentelekomcloud_obs_bucket.codebucket.bucket_domain_name,
              "code",
              basename(var.zip_file_local)
         )
        ...
      }

- configure the API Trigger for the Function using

  - API-Group,
  - API and
  - publishment to an environment,

  see: :github_repo_master:`api_trigger.tf </samples-doc/http_minimalWebAPI/terraform/api_trigger.tf>`

- configure logging for the Function using LTS Log Group and Log Stream,
  see :github_repo_master:`function.tf </samples-doc/http_minimalWebAPI/terraform/function.tf>`

- configure test events for the Function to be used in the
  Function Graph console,
  see :github_repo_master:`func_testevents.tf </samples-doc/http_minimalWebAPI/terraform/func_testevents.tf>`

To deploy the HTTP Function using terraform follow these steps:

1. Create an API Gateway or use an existing one. (Creating an API Gateway is
   not part of this terraform setup.)
   See `Creating a Gateway <https://docs.otc.t-systems.com/api-gateway/umn/gateway_management/creating_a_gateway.html>`_
   for instructions on how to create an API Gateway.
   Note down the instance ID to be used in terraform configuration.

2. Adjust the ``http.tfvars`` file according to your needs.

   Set the **api_gateway_instance_id** variable to your API Gateway
   instance ID.

   .. literalinclude:: /../../samples-doc/http_minimalWebAPI/terraform/http.tfvars
      :language: hcl

3. To deploy using the terraform/http.tfvars configuration,
   execute the following commands in the project folder of http_minimalWebAPI:

   .. code-block:: bash

      make deploy


.. note::

   To destroy the deployed resources again you can use:

   .. code-block:: bash

      make destroy
