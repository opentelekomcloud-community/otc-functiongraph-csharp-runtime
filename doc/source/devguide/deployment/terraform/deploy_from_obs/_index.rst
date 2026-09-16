.. _ref_deploy_from_obs:

Deploy FunctionGraph Event Function from OBS
=============================================

.. toctree::
   :maxdepth: 2
   :hidden:

This sample demonstrates how to deploy a simple event function to 
FunctionGraph with **code from OBS** using terraform.

This approach is used, if your unpacked FunctionGraph deployment package is **less than 40MB**.

Source code of this sample is available on :github_repo_master:`GitHub <samples-doc/deploy-from-obs>`.

Prerequisites
-----------------

- running on Linux / Windows Subsystem for Linux (WSL)
- make installed
- Terraform installed and configured, see :ref:`Terraform Setup<ref_terraform_setup>`.

Example
-------

An example for deploying a **HTTP Function** using Terraform can be found in:
:github_repo_master:`samples-doc/http_minimalWebAPI/terraform </samples-doc/http_minimalWebAPI/terraform>`.

This example deploys a minimal C# HTTP Function and demonstrates how to:

- upload the Function code as zip file to an OBS bucket (and update on changes),
  see: :github_repo_master:`code_from_obs_bucket.tf </samples-doc/http_minimalWebAPI/terraform/code_from_obs_bucket.tf>`

  .. literalinclude:: /../../samples-doc/http_minimalWebAPI/terraform/code_from_obs_bucket.tf
     :language: terraform


- create the Function using the code from the OBS bucket,
  see: :github_repo_master:`function.tf </samples-doc/http_minimalWebAPI/terraform/function.tf>`

  .. literalinclude:: /../../samples-doc/http_minimalWebAPI/terraform/function.tf
     :language: terraform

- configure the API Trigger for the Function using

  - API-Group,
  - API and
  - publishment to an environment,

  .. literalinclude:: /../../samples-doc/http_minimalWebAPI/terraform/api_trigger.tf
     :language: terraform

- configure logging for the Function using LTS Log Group and Log Stream,
  see :github_repo_master:`loggroup.tf </samples-doc/http_minimalWebAPI/terraform/loggroup.tf>`

  .. literalinclude:: /../../samples-doc/http_minimalWebAPI/terraform/loggroup.tf
     :language: terraform

- configure test events for the Function to be used in the
  Function Graph console,
  see :github_repo_master:`func_testevents.tf </samples-doc/http_minimalWebAPI/terraform/func_testevents.tf>`

  .. literalinclude:: /../../samples-doc/http_minimalWebAPI/terraform/func_testevents.tf
     :language: terraform

To deploy the HTTP Function using terraform follow these steps:

1. Create an API Gateway or use an existing one. (Creating an API Gateway is
   not part of this terraform setup.)
   See `Creating a Gateway <https://docs.otc.t-systems.com/api-gateway/umn/gateway_management/creating_a_gateway.html>`_
   for instructions on how to create an API Gateway.
   Note down the instance ID to be used in terraform configuration.

2. Adjust the ``http.tfvars`` file according to your needs.

   Set the **API_GATEWAY_INSTANCE_ID** variable to your API Gateway
   instance ID (or define it as environment variable `TF_VAR_API_GATEWAY_INSTANCE_ID`).

   .. literalinclude:: /../../samples-doc/http_minimalWebAPI/terraform/http.tfvars
      :language: hcl

3. Create a Makefile in the project folder and adjust the variables in the Makefile according to your needs:

   .. literalinclude:: /../../samples-doc/http_minimalWebAPI/Makefile
      :language: make

4. To deploy using the terraform/http.tfvars configuration,
   execute the following commands in the project root folder:

   .. code-block:: bash

      make deploy


.. note::

   To destroy the deployed resources again you can use:

   .. code-block:: bash

      make destroy
