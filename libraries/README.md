# Runtime Libraries

This package contains core libraries used for FunctionGraph.


## Usage
FunctionGraph functions have following structure:

public RETURN_PARAMETER FUNCTION_NAME(USER_DEFINED_PARAMETER event, IFunctionContext context)

where
* **RETURN_PARAMETER**: user-defined output, which is converted into a character string and returned as an HTTP response.

* **FUNCTION_NAME**:  user-defined function name. The name must be consistent with that you define when creating a function.

* **USER_DEFINED_PARAMETER**: event parameter defined for the function, see
HC.Serverless.Function.Events.* packages

* **context**: Runtime information provided for executing the function. The **HC.Serverless.Function.Common.IFunctionContext** needs to be referenced when you deploy a project in FunctionGraph.

When creating a C# function, you need to define a method as the handler of the function. The method can access the function by using specified IFunctionContext parameters.  

Example:
```cs
using HC.Serverless.Function.Common;
using System;
using System.IO;

public Stream handlerName(Stream input, IFunctionContext context)
{
       // TODO
}
```

For further details see examples in sample folder.

## Build

The core libraries can be build as follows:

```bash
cd ${PROJECT_ROOT}/buildtools

dotnet build

```
