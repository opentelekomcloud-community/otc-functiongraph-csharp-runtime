.. _creating_an_event_function_using_a_container_image_built:

Creating an Event Function using a container image built with C#
======================================================================

.. toctree::
   :maxdepth: 1
   :hidden:

For general details about how to use a container image
to create and execute an event function,
see :otc_fg_umn:`Creating an Event Function Using a Container Image and executing the Function <getting_started/creating_an_event_function_using_a_container_image_and_executing_the_function.html>`.

This chapter introduces how to create an image using C# for event functions.

.. note::

  You need to implement an **HTTP server** in the image listening to port **8000** to receive requests.

  Following request path is required:

  * **POST /invoke** is the function **execution** entry where trigger events are processed.

  Following request path is optional:

  * **POST /init** is the function **initialization** entry where you can perform
    initialization operations such as loading dependencies and preparing runtime environment.
    This entry is optional, and you can choose to implement it based on your needs.
    If you do not implement this entry, FunctionGraph will directly execute the function
    without initialization.


Example
-------------------

See: :github_repo_master:`Container Event Timer Sample <samples-doc/container-event-timer>`
for an example of creating an event function using a container image built with C#.


Terraform deployment
---------------------

To deploy the function using Terraform adapt the MakefileTF and
the terraform configuration files in the sample folder according to your needs
and execute the following commands in the project root folder:

.. code-block:: bash

   make -f MakefileTF tf_apply


To update code changes use:

.. code-block:: bash

   make -f MakefileTF update_image

.. note::
   To clean up the resources created by Terraform, execute the following command in the project root folder:
   
   .. code-block:: bash
     
      make -f MakefileTF tf_destroy