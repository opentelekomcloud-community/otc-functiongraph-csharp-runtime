Example for FunctionGraph C# project
=====================================================

The following example shows a simple C# project for FunctionGraph.

Prerequisites
-----------------------------

- .NET SDKs 2.1, 3.1, 6.0 installed.

.. note::

  For more information, see:

  - `Download .NET 6.0 <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>`_
  - `Download .NET 3.1 <https://dotnet.microsoft.com/en-us/download/dotnet/3.1>`_
  - `Download .NET 2.1 <https://dotnet.microsoft.com/en-us/download/dotnet/2.1>`_

  for Ubuntu, see: `Install .NET on Linux <https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install>`_

.. note::

   If you do not have multiple .NET SDKs installed,
   adapt the project file to only include the target framework
   you want to use.

   E.g. to work only with .NET 6.0, change in project file **.csproj**:

    .. code-block:: xml

       <TargetFrameworks>net6.0;netcoreapp3.1;netcoreapp2.1</TargetFrameworks>

    to

    .. code-block:: xml

       <TargetFramework>net6.0</TargetFramework>


Source
-----------------------------
Source for this sample can be found in:
:github_repo_master:`samples-doc/simple <samples-doc/simple>`.


C# project structure
-----------------------------

A typical C# FunctionGraph project is typically structured as follows:

.. code-block:: text

  /simple
   ├─ src
   |   ├─ Program.cs       # C# program file
   |   ├─ handler.txt      # handler file (optional)
   |   └─ simple.csproj    # dotnet project file
   └─ simple.sln           # dotnet solution file (optional)

C# program file
^^^^^^^^^^^^^^^

The main logic for the function resides in C# file **Program.cs**.

.. code-block:: csharp
   :caption: Program.cs

    namespace src
    {

    #if NET6_0_OR_GREATER
      using OpenTelekomCloud.Serverless.Function.Common;
    #else
      using HC.Serverless.Function.Common;
    #endif

      using System;
      using System.IO;
      using System.Text;

      class Program
      {

        public Stream Handler(Stream input, IFunctionContext context)
        {
          string payload = "";
          if (input != null && input.Length > 0)
          {
            byte[] buffer = new byte[input.Length];
            input.Read(buffer, 0, (int)(input.Length));
            payload = Encoding.UTF8.GetString(buffer);
          }
          var ms = new MemoryStream();
          using (var sw = new StreamWriter(ms))
          {
            sw.WriteLine("CSharp runtime test");
            sw.WriteLine("=====================================");
            sw.WriteLine("Request Id:       {0}", context.RequestId);
            sw.WriteLine("Function Name:    {0}", context.FunctionName);
            sw.WriteLine("Function Version: {0}", context.FunctionVersion);
            sw.WriteLine("Project:          {0}", context.ProjectId);
            sw.WriteLine("Package:          {0}", context.PackageName);

            sw.WriteLine("User data(ud-a):  {0}", context.GetUserData("ud-a"));
            sw.WriteLine("User data(ud-notexist): {0}", context.GetUserData("ud-notexist", ""));
            sw.WriteLine("User data(ud-notexist-default): {0}", context.GetUserData("ud-notexist", "default value"));
            sw.WriteLine("=====================================");

            var logger = context.Logger;

            logger.Logf("Request Id: {0}", context.RequestId);
            logger.Log("Hello CSharp runtime test");
            sw.WriteLine(payload);
          }
          return new MemoryStream(ms.ToArray());
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
    }

C# project file
^^^^^^^^^^^^^^^

.. code-block:: xml
   :caption: simple.csproj

    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <!-- Disable NuGet package audit and EOL target framework check -->
        <NuGetAudit>false</NuGetAudit>
        <CheckEolTargetFramework>false</CheckEolTargetFramework>
      </PropertyGroup>

      <PropertyGroup>
         <!-- OutputType EXE is needed (empty Main method is needed) -->
        <OutputType>EXE</OutputType>
        <!-- Target multiple frameworks -->
        <TargetFrameworks>net6.0;netcoreapp3.1;netcoreapp2.1</TargetFrameworks>
        <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
        <!-- Define the assembly name, if not set, the project file name is used -->
        <AssemblyName>simple</AssemblyName>
      </PropertyGroup>

      <ItemGroup>
        <!-- Include handler.txt in package if file exists -->
        <Content Include="handler.txt" Condition="Exists('handler.txt')">
          <Pack>true</Pack>
          <PackagePath>\</PackagePath>
          <IncludeInPackage>true</IncludeInPackage>
          <CopyToOutput>true</CopyToOutput>
          <BuildAction>Content</BuildAction>
          <copyToOutput>true</copyToOutput>
          <CopyToOutputDirectory>Always</CopyToOutputDirectory>
          <CopyToPublishDirectory>Always</CopyToPublishDirectory>
        </Content>
      </ItemGroup>

      <ItemGroup>
        <PackageReference Include="OpenTelekomCloud.Serverless.Function.Common" Version="*-*" />
      </ItemGroup>

      <!-- Create zip package after build to be uploaded to FunctionGraph -->
      <Target Name="ZipOutputPath" Condition="'$(TargetFramework)'!=''" AfterTargets="Build">
        <ZipDirectory
          SourceDirectory="$(OutputPath)"
          DestinationFile="$(MSBuildProjectDirectory)\$(MSBuildProjectName)_$(TargetFramework).zip"
          Overwrite="true" />
      </Target>

    </Project>

Handler file
^^^^^^^^^^^^

This file is not necessary but useful when deploying the
function using the console.

In this case Function Handler Name is build:

* **AssemblyName**: simple
* **Namespace**: src
* **ClassName**: Program
* **MethodName**: Handler

.. code-block:: text
   :caption: handler.txt

   simple::src.Program::Handler


Build the project
------------------

Project packaging rules for C#
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
When packaging a C# project for FunctionGraph,
the ZIP file must contain the following files:

* **AssemblyName**.deps.json,
* **AssemblyName**.dll,
* **AssemblyName**.runtimeconfig.json,
* **AssemblyName**.pdb,
* OpenTelekomCloud.Serverless.Function.Common.dll

.. note::
   **AssemblyName** is the name defined in the project file, as `<AssemblyName>`,
   if this is not defined, the project file name is used


Example directory of a C# project package

.. tabs::

  .. tab:: Directory structure for .NET 6.0

      .. code-block:: text
        :caption: Directory structure for .NET 6.0

          simple_net6.0.zip                                   # Example project package
          ├─ simple.deps.json                                 # File generated after project compilation
          ├─ simple.dll                                       # File generated after project compilation
          ├─ simple.pdb                                       # File generated after project compilation
          ├─ simple.runtimeconfig.json                        # File generated after project compilation
          ├─ handler.txt                                      # Help file, which can be directly used
          └─ OpenTelekomCloud.Serverless.Function.Common.dll  # .dll file provided by this runtime package

  .. tab:: Directory structure for prior to .NET 6.0

      .. code-block:: text
        :caption: Directory structure for prior to .NET 6.0

          simple_event_timer_netcoreapp3.1.zip                       # Example project package
          ├─ simple.deps.json                                        # File generated after project compilation
          ├─ simple.dll                                              # File generated after project compilation
          ├─ simple.pdb                                              # File generated after project compilation
          ├─ simple.runtimeconfig.json                               # File generated after project compilation
          ├─ handler.txt                                             # Help file, which can be directly used
          └─ HC.Serverless.Function.Common.legacy.dll                # .dll file provided by this runtime package


This deployment zip will be created automatically
when you build the project using the following command:

.. code-block:: bash

   dotnet build -c Release

This command builds the project for all target frameworks
defined in the project file and creates a zip file for each target framework
in the project/src folder.

The generated zip files are:

- simple_net6.0.zip
- simple_netcoreapp3.1.zip
- simple_netcoreapp2.1.zip

Deploy the function
--------------------

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

Use **Upload** -> **Local ZIP** and upload *simple_net6.0.zip*
from previous step.

Configure function
^^^^^^^^^^^^^^^^^^^^

* In **Configuration** -> **Basic Settings** -> **Handler**:
  set value to name as defined in **handler.txt**

Test the function
-------------------

Create Test Event
^^^^^^^^^^^^^^^^^^^^

In **Code** create a Test Event using "Blank Template".

Test function
^^^^^^^^^^^^^^^^^^^^

Click **Test** to test function.

The function execution result is displayed in the
**Execution Result** section.
