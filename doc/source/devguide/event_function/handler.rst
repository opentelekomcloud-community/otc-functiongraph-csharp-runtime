Define FunctionGraph function handler in C#
==========================================================

.. toctree::
    :hidden:

The C# function handler is the method in your function code that
processes events.
When your function is invoked, FunctionGraph runs the handler method.
Your function runs until the handler returns a response, exits, or times out.

C# function syntax: **[Scope]** **[Return parameter]** **[Function name]** (**[User-defined parameter]**, **[Context]**)

- **Scope**: It must be defined as public for the function
  that FunctionGraph invokes to execute your code.
- **Return parameter**: user-defined output, which is converted
  into a character string and returned as an HTTP response.
- **Function name**: user-defined function name. The name must
  be consistent with that you define when creating a function.
- **User-defined parameter**: parameter defined for the function.
- **context**: runtime information provided for executing the function.

.. note::
  - For target frameworks prior to .NET 6.0,
    the **HC.Serverless.Function.Common.legacy**
    library needs to be referenced,
  - for target frameworks .NET 6.0 and later versions,
    the **OpenTelekomCloud.Serverless.Function.Common** library
    needs to be referenced.

  For details about the IFunctionContext object, see the :doc:`Context <./context>` description.

When creating a C# function, you need to define a method as the handler
of the function.

Example:

.. code-block:: csharp
   :caption: Example of a C# function handler

    #if NET6_0_OR_GREATER
      using OpenTelekomCloud.Serverless.Function.Common;
    #else
      using HC.Serverless.Function.Common;
    #endif

    namespace Example
    {
        public class Hello
        {
            public string Handler(string input, IFunctionContext context)
            {
                return "Hello";
            }
        }
    }

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
see :doc:`Using the FunctionGraph context object to retrieve function information<./context>`.

Accessing environment variables
-------------------------------

Environment variables defined in ``OpenTelekomCloud`` ->
``Configuration`` -> ``Environment Variables`` can be accessed using:

.. code-block:: java

  // accessing an environment variable named "ENV_VAR1"
  context.GetUserData("ENV_VAR1","");
