Start ECS Sample
=========================

.. toctree::
   :hidden:

This sample demonstrates how to start an ECS instance using FunctionGraph and:

* :otc_docs:`OpenTelekomCloud Rest API for ECS batch operations <elastic-cloud-server/api-ref/apis_recommended/batch_operations/starting_ecss_in_a_batch.html>` 
* :github_csharp_sign_sdk:`otc-api-sign-sdk-csharp <>`

Prerequisites
^^^^^^^^^^^^^^^^^^^^^
* For this example an ECS instance must exist.
* The function must have permissions to start the ECS instance.

  This can be achieved by creating an agency with a policy
  granting the permission `ecs:StartServers` and
  specifying this agency when creating the function.


Source
-------

Source for this sample can be found in:
:github_repo_master:`/samples-doc/start-ecs</samples-doc/sdk-ecs>`.

.. tabs::

  .. tab:: sdk_ecs.csproj

     .. literalinclude:: /../../samples-doc/sdk-ecs/sdk_ecs.csproj
        :language: xml
        :caption: /sdk_ecs.csproj

  .. tab:: Program.cs

    This files contains the main program.

     .. literalinclude:: /../../samples-doc/sdk-ecs/Program.cs
        :language: csharp
        :caption: /Program.cs

  .. tab:: handler.txt

    The handler name for this function is:

     .. literalinclude:: /../../samples-doc/sdk-ecs/handler.txt
        :language: text
        :caption: /handler.txt


Build the project
-----------------

Run command:

.. code-block:: bash

   dotnet build

This command builds the project for all target frameworks
and creates a zip file for each target framework
in the project folder.

The generated zip files are:

- sdk_ecs_net6.0.zip
- sdk_ecs_netcoreapp3.1.zip
- sdk_ecs_netcoreapp2.1.zip


Deploy the function
-------------------

Use `OpentelekomCloud FunctionGraph console <https://console.otc.t-systems.com/functiongraph/>`_ to create a function with following
settings:

Create function
*******************

**Create With**:  Create from scratch 

**Basic Information**

* **Function Type**  Event Function  
* **Region**  <YOUR REGION>  
* **Function Name** <YOUR FUNCTION NAME>  
* **Agency**  Specify an agency with policy to start ECS instance 
* **Runtime**  C# (.NET 6.0)

Upload code
*******************

Use **Upload** -> **Local ZIP** and upload *start_ecs_net6.0.zip*
from previous step.

Configure function
*******************

* In **Configuration** -> **Basic Settings** -> **Handler**:
  set value to name as defined in **handler.txt**

* In **Configuration** -> **Environment Variables** add following variables:

    .. list-table:: Environment variables
      :widths: 20 20 25
      :header-rows: 1

      * - Environment variable name
        - Value
        - Remarks

      * - ECS_INSTANCE_ID
        - <ID of ecs instance>
        - ID of ECS instance to start

      * - ECS_ENDPOINT
        - <ecs endpoint>
        - Default: ecs.eu-de.otc.t-systems.com,
          see :otc_docs:`Regions and Endpoints<regions-and-endpoints/index.html>`

Test the function
-------------------

Create Test Event
*******************

In **Code** create a Test Event using "Blank Template"
(Event is not used in function).

Test function
*******************

Click **Test** to test function.

