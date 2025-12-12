Define FunctionGraph function handler in C#
==========================================================

.. toctree::
    :hidden:


Function syntax
----------------

The C# function handler is the method in your function code that
processes events.

When your function is invoked, FunctionGraph runs the handler method.
Your function runs until the handler returns a response, exits, or times out.

C# function syntax:
**[Scope]** **[Return parameter]** **[Function name]** (**[User-defined parameter]**, **[Context]**)

- **Scope**: It must be defined as **public** for the function
  that FunctionGraph invokes to execute your code.

- **Return parameter**: user-defined output, which is converted
  into a character string and returned as an HTTP response. For C# functions,
  the return parameter type must be defined as **Stream**.

- **Function name**: user-defined function name. The name must
  be consistent with that you define when creating a function.

- **User-defined parameter**: parameter defined for the function.
  The type must be defined as **Stream**.

- **context**: runtime information provided for executing the function.

  For details about the IFunctionContext object, see the
  :doc:`Context <./context>` description.

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

    namespace src
    {
        public class Program
        {
            public Stream Handler(Stream inputEvent, IFunctionContext context)
            {
                return "hello world";
            }
        }

        /// <summary>
        /// Main method - not used in FunctionGraph but needed for compilation
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
          Console.WriteLine("This is a FunctionGraph C# runtime program");
        }
    }

.. note::

  As the project output type has to be set in .csproj file to
  ``<OutputType>Exe</OutputType>``, a ``Main`` method is required.

  This method is needed for compilation only and is not invoked by FunctionGraph.

.. note::

  For C# versions

  - prior to .NET 6.0, classes from ``HC.Serverless.Function.Common`` must be used

  - .NET 6.0 and above, classes from ``OpenTelekomCloud.Serverless.Function.Common`` must be used.

Function Handler
----------------

For a C# function, the handler must be named in the format of

.. code-block:: console

   ASSEMBLY::NAMESPACE.CLASSNAME::METHODNAME

**ASSEMBLY**: name of the .NET assembly file for your application,
for example: simple.

**NAMESPACE** and **CLASSNAME**: names of the namespace and class
to which the handler function belongs (as defined in Program.cs: src.Program).

**METHODNAME**: name of the handler function
(as defined in Program.cs: Handler).


.. code-block:: text
   :caption: handler.txt

   simple::src.Program::Handler

Accessing and using the FunctionGraph context object
----------------------------------------------------

The :doc:`Context<./context>` interface allows functions to obtain the
function execution context, such as information about the invocation,
function, execution environment, and so on.

The context is of type ``IFunctionContext``
and is the second argument of the handler function.

* :github_repo_master:`IFunctionContext <libraries/src/OpenTelekomCloud.Serverless.Function.Common/IFunctionContext.cs>`


Logging in FunctionGraph
^^^^^^^^^^^^^^^^^^^^^^^^

To produce logs in OpenTelekomCloud Log Tank Servics (LTS) you can use
``context.Logger`` to get a RuntimeLogger object for logging.

.. code-block:: csharp

  var logger = context.Logger;
  var payload ="World";
  logger.Logf("Hello {0}", string(payload));

For more information about the context object,
see :doc:`Using the FunctionGraph context object to retrieve function information<./context>`.

Accessing environment
^^^^^^^^^^^^^^^^^^^^^

Environment variables defined in ``OpenTelekomCloud`` ->
``Configuration`` -> ``Environment Variables`` can be accessed using:

.. code-block:: java

  // accessing an environment variable named "ENV_VAR1"
  context.GetUserData("ENV_VAR1","");
