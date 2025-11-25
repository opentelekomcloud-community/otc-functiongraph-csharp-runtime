JSON Serialization/Deserialization in FunctionGraph
=====================================================

.. toctree::
    :hidden:

This document describes how to use JSON serialization and deserialization
in FunctionGraph C# functions using the
:class:`OpenTelekomCloud.Serverless.Function.Common.JsonSerializer` class.


In this example, we will demonstrate how to serialize and deserialize
a simple object with a single property.

.. literalinclude:: /../../samples-doc/serialization_simple/event.json
   :language: json
   :caption: event.json

Source
-------

Source for this sample can be found in:
:github_repo_master:`samples-doc/serialization_simple <samples-doc/serialization_simple>`.

C# project file
^^^^^^^^^^^^^^^^^^^^

To use JSON serialization/deserialization, first add a reference
to the `OpenTelekomCloud.Serverless.Function.Common` library
in your project file:

.. literalinclude:: /../../samples-doc/serialization_simple/src/serialization_simple.csproj
   :language: xml
   :caption: serialization_simple.csproj


C# program files
^^^^^^^^^^^^^^^^^^^^

Define a simple class to represent the object structure of the JSON data:

.. literalinclude:: /../../samples-doc/serialization_simple/src/TestJson.cs
   :language: csharp
   :caption: TestJson.cs

Implement the function handler to perform serialization and deserialization:

.. literalinclude:: /../../samples-doc/serialization_simple/src/Program.cs
   :language: csharp
   :caption: Program.cs

Handler file
^^^^^^^^^^^^^^^^^^^^

.. literalinclude:: /../../samples-doc/serialization_simple/src/handler.txt
   :language: text
   :caption: handler.txt


Build the project
------------------

To build the project, navigate to the project directory and run the following
command:

.. code-block:: bash

   dotnet build -c Release

This command builds the project for all target frameworks defined
in the project file and creates a zip file for each target framework
in the project folder.

Deploy the function
-------------------

Use `OpentelekomCloud FunctionGraph console <https://console.otc.t-systems.com/functiongraph/>`_
to create a function with following settings:

Create function
^^^^^^^^^^^^^^^^^^^^

**Create With**:  Create from scratch

**Basic Information**

* **Function Type**  Event Function
* **Region**  <YOUR REGION>
* **Function Name** <YOUR FUNCTION NAME>
* **Runtime**  C# (.NET 6.0)

Upload code
^^^^^^^^^^^^^^^^^^^^

Use **Upload** -> **Local ZIP** and upload *serialization_simple_net6.0.zip*
from previous step.

Configure function
^^^^^^^^^^^^^^^^^^^^

* In **Configuration** -> **Basic Settings** -> **Handler**:
  set value to name as defined in **handler.txt**

Test the function
-------------------

Create Test Event
^^^^^^^^^^^^^^^^^^^^

In **Code** create a Test Event using "Blank Template"
and add following content:

.. literalinclude:: /../../samples-doc/serialization_simple/event.json
   :language: json
   :caption: TestEvent

Test function
^^^^^^^^^^^^^^^^^^^^

Click **Test** to test function.

The function execution result is displayed in the
**Execution Result** section.
