Sample for Timer triggered FunctionGraph
================================================

This sample demonstrates how to create a timer triggered FunctionGraph
using C# runtime and deploy it using Terraform.

Source for this sample can be found in:
:github_repo_master:`/samples-doc/event-timer <samples-doc/event-timer>`.

Overview
--------
Following diagram shows components used in this example:


The timer event triggers the FunctionGraph function at specified intervals.

Deployment
----------
This sample can be deployed using Terraform,
see :ref:`ref_terraform_setup` for setup details.

Terraform deployment scripts can be found in:
:github_repo_master:`/samples-doc/event-timer/terraform <samples-doc/event-timer/terraform>`