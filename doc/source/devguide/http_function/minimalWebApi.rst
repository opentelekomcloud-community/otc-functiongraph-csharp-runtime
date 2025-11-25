Creating a minimal Web API
==========================

This example shows how to create a simple HTTP function
exposing RestAPI endpoints using FunctionGraph.

Prerequisites
-----------------------------

- .NET SDK 8.0 installed.


Source
-----------------------------
Source for this sample can be found in:
:github_repo_master:`samples-doc/http_minimalWebAPI <samples-doc/http_minimalWebAPI>`.


C# project structure
-----------------------------

The project structure is as follows:

.. code-block:: text
   :caption: Project Structure

   /http_minimalWebAPI/
    ├── src/
    |   ├── Properties
    |   │   └── launchSettings.json
    |   ├── wwwroot
    |   │   └── favicon.ico
    |   ├── appsettings.json
    |   ├── appsettings.Development.json
    |   ├── http_minimalWebAPI.csproj
    │   └── Program.cs
    (└── http_minimalWebAPI.sln)

C# Project file
^^^^^^^^^^^^^^^^^^^^

To create a minimal Web API project for FunctionGraph,

1. Run the following command to create a new ASP.NET Core Web API project:

    .. code-block:: shell

       dotnet new web -o http_minimalWebAPI --use-program-main --no-https

2. Navigate to the project directory:

   .. code-block:: shell

       cd http_minimalWebAPI

3. Replace the contents of the `Program.cs` file with the code of next section.

4. Modify the project file (`.csproj`) to include
   the necessary settings for FunctionGraph deployment.
   You can refer to the provided `http_minimalWebAPI.csproj` file for guidance.

   .. literalinclude:: ../../../../samples-doc/http_minimalWebAPI/src/http_minimalWebAPI.csproj
      :caption: http_minimalWebAPI.csproj
      :language: xml

   Using above settings ensures that the project is built correctly
   for deployment to FunctionGraph:

   - bootstrap file is created,
   - output is packaged in a ZIP file.


C# Program file
^^^^^^^^^^^^^^^^^^^^

.. literalinclude:: ../../../../samples-doc/http_minimalWebAPI/src/Program.cs
   :caption: Program.cs
   :language: csharp


To run this example locally, follow these steps:


Run the HTTP Function locally
--------------------------------

To run the project, execute the following step:

   .. code-block:: shell

        dotnet run

Open your browser and navigate to http://localhost:8000 to
see the "Hello, World!" response.

Other endpoints are:

      - http://localhost:8000/greeting/John
      - http://localhost:8000/hello?name=John
      - http://localhost:8000/test


Deploying the HTTP Function
---------------------------

To deploy the HTTP function to FunctionGraph, follow these steps:

Build deployment package
^^^^^^^^^^^^^^^^^^^^^^^^

Following command will create a deployment package in ZIP format:

   .. code-block:: shell

        dotnet publish

Deploy the function to FunctionGraph
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Use `OpentelekomCloud FunctionGraph console <https://console.otc.t-systems.com/functiongraph/>`_
to create a function with following settings:

Create function
^^^^^^^^^^^^^^^^^^^^

**Create With**:  Create from scratch

**Basic Information**

* **Function Type**  HTTP Function
* **Region**  <YOUR REGION>
* **Function Name** <YOUR FUNCTION NAME>

Upload code
^^^^^^^^^^^^^^^^^^^^

Use **Upload** -> **Local ZIP** and upload the generated ZIP file from step 1
   ($(MSBuildProjectName)_net8.0.zip).


Configure function
^^^^^^^^^^^^^^^^^^^^


Create an API Gateway trigger for the function to expose it
as an HTTP endpoint.

For details on creating an API Gateway trigger, see
:otc_fg_umn:`Using an API Gateway Trigger <creating_triggers/using_an_apig_dedicated_trigger.html>`.

Configure the API Gateway settings:

    - Set the Security Authentication to: **NONE**
    - Set Protocol to: **HTTPS**.
    - Set Method to: **ANY**.

After creation URL of the API Gateway endpoint is displayed.
This URL will be used as **<api-gateway-endpoint>** in following step.


Test the function
-------------------

Using a browser
^^^^^^^^^^^^^^^^
You can use this URL to invoke your HTTP function from a web browser
using following endpoints:

    .. code-block:: shell

      https://<api-gateway-endpoint>/
      https://<api-gateway-endpoint>/greeting/John
      https://<api-gateway-endpoint>/hello?name=John
      https://<api-gateway-endpoint>/test

Using Test in FunctionGraph console
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Testing the "root" endpoint
"""""""""""""""""""""""""""

Create a Test Event in **Code** section of the FunctionGraph console
with following content:

.. literalinclude:: /../../samples-doc/http_minimalWebAPI/resources/apievent_root.json

Select this Test Event and click **Test** to test the function.

The function execution result is displayed in the
**Execution Result** section.


.. code-block:: json

  {
      "body": "SGVsbG8gV29ybGQh",
      "headers": {
          "Content-Type": [
              "text/plain; charset=utf-8"
          ],
          "Date": [
              "Tue, 25 Nov 2025 12:20:11 GMT"
          ],
          "Server": [
              "Kestrel"
          ]
      },
      "statusCode": 200,
      "isBase64Encoded": true
  }

The response body is Base64 encoded.

Decode the body to see the actual response:

.. code-block:: shell

   echo "SGVsbG8gV29ybGQh" | base64 --decode

This displays:

.. code-block:: text

    Hello World!


Testing the "hello" endpoint
"""""""""""""""""""""""""""""

Create a Test Event in **Code** section of the FunctionGraph console
with following content:

.. literalinclude:: /../../samples-doc/http_minimalWebAPI/resources/apievent_hello.json

Select this Test Event and click **Test** to test the function.

The function execution result is displayed in the
**Execution Result** section.


.. code-block:: json

  {
      "body": "SGVsbG8sIEpvaG4h",
      "headers": {
          "Content-Type": [
              "text/plain; charset=utf-8"
          ],
          "Date": [
              "Tue, 25 Nov 2025 12:31:19 GMT"
          ],
          "Server": [
              "Kestrel"
          ]
      },
      "statusCode": 200,
      "isBase64Encoded": true
  }

The response body is Base64 encoded.

Decode the body to see the actual response:

.. code-block:: shell

   echo "SGVsbG8sIEpvaG4h" | base64 --decode

This displays:

.. code-block:: text

    Hello, John!
