Define FunctionGraph function handler in C#
==========================================================

The C# function handler is the method in your function code that
processes events.
When your function is invoked, FunctionGraph runs the handler method.
Your function runs until the handler returns a response, exits, or times out.


Setting up the C# handler project
---------------------------------

A typical C# FunctionGraph project is typically structured as follows:

.. code-block:: console

  /project-root
   ├─ Program.cs
   └─ program.csproj

The main logic for the function resides in C# file **Program.cs**.
When deploying to FunctionGraph make sure to specify the correct handler:

For a C# function, the handler must be named in the format of

.. code-block:: console

   ASSEMBLY::NAMESPACE.CLASSNAME::METHODNAME

**ASSEMBLY**: name of the .NET assembly file for your application,
for example, HelloCsharp.

**NAMESPACE** and **CLASSNAME**: names of the namespace and class
to which the handler function belongs.

**METHODNAME**: name of the handler function. 

Example:

.. code-block:: console

   HelloCsharp::Example.Hello::Handler


Example code for C# FunctionGraph function
------------------------------------------

TBD

Example C# FunctionGraph function code
--------------------------------------

The following example shows a simple FunctionGraph function written in C#.

.. literalinclude:: /../../samples-doc/simple/Program.cs
    :language: csharp
    :caption: :github_repo_master:`program.cs <samples-doc/simple/Program.cs>`


.. literalinclude:: /../../samples-doc/simple/handler.txt
    :language: csharp
    :caption: The handler name for this example is:


Accessing and using the FunctionGraph context object
----------------------------------------------------

The :doc:`Context<./context>` interface allows functions to obtain the
function execution context, such as information about the invocation,
function, execution environment, and so on.

The context is of type ``IFunctionContext``
and is the second argument of the handler function.

* :github_repo_master:`IFunctionContext <libraries/src/OpenTelekomCloud.Serverless.Function.Common/IFunctionContext.cs>`

To produce logs in OpenTelekomCloud Log Tank Servics (LTS) you can use
``context.Logger`` to get a RuntimeLogger object for logging.

.. code-block:: csharp

  var logger = context.Logger;
  var payload ="World";
  logger.Logf("Hello {0}", string(payload));

For more information about the context object,
see :doc:`Using the FunctionGraph context object to retrieve function information.<./context>`

Accessing environment variables
-------------------------------

Environment variables defined in ``OpenTelekomCloud`` ->
``Configuration`` -> ``Environment Variables`` can be accessed using:

.. code-block:: java

  // accessing an environment variable named "ENV_VAR1"
  context.GetUserData("ENV_VAR1","");
