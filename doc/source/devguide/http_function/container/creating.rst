.. _creating_an_http_function_using_a_container_image_built_with_csharp:

Creating an HTTP Function Using a Container Image Built with C#
================================================================

.. toctree::
   :maxdepth: 1
   :hidden:

For general details about how to use a container image
to create and execute an HTTP function,
see :otc_fg_umn:`Creating an HTTP Function Using a Container Image and executing the Function <getting_started/creating_an_http_function_using_a_container_image_and_executing_the_function.html>`.

This chapter introduces how to create an image using C# for HTTP functions.

.. note::

  You need to implement an **HTTP server** in the image listening to port **8000** to receive requests.


Example
-------------------

See: :github_repo_master:`container-http <samples-doc/container-http>`
for an example of creating an http function using a container image built with C#.

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