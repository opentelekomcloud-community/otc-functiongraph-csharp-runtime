Deploying an Event Function using Terraform
===========================================

This section describes on how to deploy an Event Function using Terraform.


Prerequisite
------------

* Terraform configured according to :ref:`ref_terraform_setup`


Example
-------

An example for deploying a **Event Function** using Terraform can be found in:
:github_repo_master:`samples-doc/event-timer/terraform <samples-doc/event-timer/terraform>`.

This example deploys a Event Function (C# .NET 6) and demonstrates how to:

- create the Function using the code from zip file uploaded in function,
  see: :github_repo_master:`function.tf </samples-doc/event-timer/terraform/function.tf>`

  .. code-block:: terraform

      resource "opentelekomcloud_functiongraph_function_v2" "MyEventFunction" {
        ...
        code_type     = "zip"
        func_code     = filebase64(var.zip_file_local)
        code_filename = basename(var.zip_file_local)
        ...
      }

- configure an Timer Trigger for the Function

  - of type **TIMER**
  - using a **cron expression** triggering **every 3 minutes**

  see: :github_repo_master:`api_trigger.tf </samples-doc/event-timer/terraform/function.tf>`

- configure logging for the Function using LTS Log Group and Log Stream,
  see :github_repo_master:`function.tf </samples-doc/event-timer/terraform/function.tf>`

- configure test events for the Function to be used in the
  Function Graph console,
  see :github_repo_master:`func_testevents.tf </samples-doc/event-timer/terraform/function.tf>`

To deploy the Event Function using terraform follow these steps:

1. Adjust the ``net6.tfvars`` file according to your needs.

   .. literalinclude:: /../../samples-doc/event-timer/terraform/net6.tfvars
      :language: hcl

2. To deploy using the terraform/http.tfvars configuration,
   execute the following commands in the project folder of http_minimalWebAPI:

   .. code-block:: bash

      make deploy


.. note::

   To destroy the deployed resources again you can use:

   .. code-block:: bash

      make destroy
