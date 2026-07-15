.. _ref_deploy_from_zip:

Deploy FunctionGraph Event Function from ZIP
=============================================

.. toctree::
   :maxdepth: 2
   :hidden:

This sample demonstrates how to deploy a simple event function to 
FunctionGraph with **code from ZIP** using terraform.

This approach is used, if your unpacked FunctionGraph deployment package is **less than 40MB**.

Prerequisites
-----------------

- running on Linux / Windows Subsystem for Linux (WSL)
- make installed
- Terraform/OpenTofu installed and  Terraform/OpenTofu configured, see :ref:`Terraform Setup<ref_terraform_setup>`.

Example
-------

An example for deploying a **Event Function** using Terraform can be found in:
:github_repo_master:`samples-doc/event-timer/terraform <samples-doc/event-timer/terraform>`.

This example deploys a Event Function (C# .NET 6) and demonstrates how to:

- create the Function using the code from zip file uploaded in function,
  see: :github_repo_master:`function.tf </samples-doc/event-timer/terraform/function.tf>`

  .. literalinclude:: /../../samples-doc/event-timer/terraform/function.tf
     :language: terraform

  
- configure an Timer Trigger for the Function

  - of type **TIMER**
  - using a **cron expression** triggering **every 3 minutes**

  see: :github_repo_master:`trigger_timer.tf </samples-doc/event-timer/terraform/trigger_timer.tf>`

  .. literalinclude:: /../../samples-doc/event-timer/terraform/trigger_timer.tf
     :language: terraform

  

- configure logging for the Function using LTS Log Group and Log Stream,
  see :github_repo_master:`loggroup.tf </samples-doc/event-timer/terraform/loggroup.tf>`

  .. literalinclude:: /../../samples-doc/event-timer/terraform/loggroup.tf
     :language: terraform

- configure test events for the Function to be used in the
  Function Graph console,
  see :github_repo_master:`testevent.tf </samples-doc/event-timer/terraform/testevent.tf>`

  .. literalinclude:: /../../samples-doc/event-timer/terraform/testevent.tf
     :language: terraform

To deploy the Event Function using terraform follow these steps:

1. Adjust the ``net6.tfvars`` file according to your needs.

   .. literalinclude:: /../../samples-doc/event-timer/terraform/net6.tfvars
      :language: hcl


2. Create a Makefile in the project folder and adjust the variables in the Makefile according to your needs:

   .. literalinclude:: /../../samples-doc/event-timer/Makefile
      :language: make


3. To deploy using the terraform/http.tfvars configuration,
   execute the following commands in the project root folder:

   .. code-block:: bash

      make deploy


.. note::

   To destroy the deployed resources again you can use:

   .. code-block:: bash

      make destroy
