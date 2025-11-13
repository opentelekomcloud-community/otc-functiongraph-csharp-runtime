Using the FunctionGraph context interface to retrieve C# function information
=============================================================================

When FunctionGraph runs your function, it passes a context object to the
handler.
This object provides context attributes that provide information about
the invocation, function, and execution environment.

Context interface
-----------------

.. list-table:: **Table 1** Context interface methods
   :widths: 10 25
   :header-rows: 1

   * - Attribute
     - Description

   * - string RequestId
     - Get the request ID.
  
   * - string ProjectId()
     - Get the project id

   * - string PackageName
     - Get name of package.

   * - string FunctionName
     - Get the function name.

   * - string FunctionVersion
     - Get the version of the function.

   * - int MemoryLimitInMb
     - Allocated memory.

   * - int CPUNumber
     - Get the CPU resources used by the function.

   * - string AccessKey
     - Get the AccessKey (valid for 24 hours) with an agency.

       To use this method, you need to configure **agency** for the function.

       .. note::

         FunctionGraph has stopped maintaining the **GetAccessKey()** API in the Runtime
         SDK. You cannot use this API to obtain a temporary AK.

   * - string SecretKey
     - Get the SecretKey (valid for 24 hours) with an agency.

       To use this method, you need to configure the **agency** for the function.

       .. note::

         FunctionGraph has stopped maintaining the **GetSecretKey()** API in the Runtime
         SDK. You cannot use this API to obtain a temporary SK.

   * - string Token
     - Get the token (valid for 24 hours) with an agency.

       To use this method, you need to configure the **agency** for the function.

   * - string SecurityAccessKey
     - Get the SecurityAccessKey (valid for 24 hours) with an agency.

       To use this method, you need to configure a **agency** for the function.

   * - string SecuritySecretKey
     - Get the SecuritySecretKey (valid for 24 hours) with an agency.

       To use this method, you need to configure the **agency** for the function.

   * - string SecurityToken
     - Get the SecuritySecretToken (valid for 24 hours) with an agency.

       To use this method, you need to configure the **agency** for the function.

   * - string GetUserData(string key, string defaultValue="")
     - Uses keys to obtain the values passed by environment variables.

   * - int RemainingTimeInMilliSeconds
     - Get the remaining running time of a function.

   * - IFunctionLogger Logger
     - Get the logger method provided by the context (by default, it will output information such as time and request ID).

   * - WorkflowID
     -

   * - WorkflowRunID
     -

   * - WorkflowStateID
     -

.. warning::
  Results returned by using the ``Token``, ``AccessKey``, and
  ``SecretKey`` or ``SecurityToken``, ``SecurityAccessKey``, and
  ``SecuritySecretKey`` methods contain sensitive information.
  Exercise caution when using these methods.

Logging
-------------

FunctionGraph provides a logging interface through the context object.

The logger can be accessed by using the **Logger** property of the
context object.

The methods provided by the interface outputs logs in the format of *Time in UTC* *Request ID* *Output*
for example, **2017-10-25T09:10:03.328Z 473d369d-101a-445e-a7a8-315cca788f86 test log output.**

The logging interface provides the following methods:

**Log(string message)**.

The following example shows how to use the logging interface:

.. code-block:: csharp
   :caption: Example

    var logger = context.Logger
    logger.Log("Hello world")

The preceding code outputs the following log:

**2017-10-25T09:10:03.328Z 473d369d-101a-445e-a7a8-315cca788f86 Hello world**


**Logf(string format, params object[] args)**.
The following example shows how to use the logging interface:

.. code-block:: csharp
   :caption: Example

    var logger = context.Logger
    var version = "v1.0.2"
    logger.Logf("Hello world {0}", version)

The preceding code outputs the following log:

**2017-10-25T09:10:03.328Z 473d369d-101a-445e-a7a8-315cca788f86 Hello world v1.0.2**

