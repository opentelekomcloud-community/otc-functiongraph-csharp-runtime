Creating a minimal Web API
==========================

This example shows how to create a simple HTTP function.

.. literalinclude:: ../../../../samples-doc/http_minimalWebAPI/Program.cs
   :caption: Program.cs
   :language: csharp


To run this example locally, follow these steps:

1. Run the following command to create a new ASP.NET Core Web API project:

    .. code-block:: shell

       dotnet new web -o MyHttpFunction --use-program-main --no-https
  
2. Navigate to the project directory:

   .. code-block:: shell

       cd MyHttpFunction
   
3. Replace the contents of the `Program.cs` file with the example code above.

4. Modify the project file (`.csproj`) to include the necessary settings for FunctionGraph deployment.
   You can refer to the provided `http_minimalWebAPI.csproj` file for guidance.

   .. literalinclude:: ../../../../samples-doc/http_minimalWebAPI/http_minimalWebAPI.csproj
      :caption: http_minimalWebAPI.csproj
      :language: xml

5. Run the project:

   .. code-block:: shell

        dotnet run

6. Open your browser and navigate to `http://localhost:8000` to see the "Hello, World!" response.

Deploying the HTTP Function to FunctionGraph
--------------------------------------------

To deploy the HTTP function to FunctionGraph, follow these steps:

1. Following command will create a deployment package in ZIP format:

   .. code-block:: shell

        dotnet publish 

2. In FunctionGraph console, create a new **HTTP Function**
   with **Function Name**: **MyHTTPFunction**

3. In the **Function Code** section, select **Upload ZIP File**
   and upload the generated ZIP file from step 1
   ($(MSBuildProjectName)_net8.0.zip).

4. Create an API Gateway trigger for the function to expose it
   as an HTTP endpoint.

   For details on creating an API Gateway trigger, see
   :otc_fg_umn:`Using an API Gateway Trigger <creating_triggers/using_an_apig_dedicated_trigger.html>`.

5. Configure the API Gateway settings:

    - Set the Security Authentication to: **NONE**
    - Set Protocol to: **HTTPS**.
    - Set Method to: **ANY**.

6. After creation URL of the API Gateway endpoint is displayed.
   This URL will be used in following step.
 
7. You can use this URL to invoke your HTTP function from a web browser
   using following endpoints:

    .. code-block:: shell

      https://<api-gateway-endpoint>/
      https://<api-gateway-endpoint>/greeting/John
      https://<api-gateway-endpoint>/hello?name=John
      https://<api-gateway-endpoint>/test

