Example for FunctionGraph C# project
------------------------------------------

The following example shows a simple C# project for FunctionGraph.
(:github_repo_master:`Source on GitHub <samples-doc/simple>`)


Prerequisites
^^^^^^^^^^^^^^^^^^^^^

- .NET SDKs 2.1, 3.1, 6.0 installed.

.. note::

  For more information, see:

  - `Download .NET 6.0 <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>`_
  - `Download .NET 3.1 <https://dotnet.microsoft.com/en-us/download/dotnet/3.1>`_
  - `Download .NET 2.1 <https://dotnet.microsoft.com/en-us/download/dotnet/2.1>`_

  for Ubuntu, see: `Install .NET on Linux <https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install>`_

C# Project structure
^^^^^^^^^^^^^^^^^^^^^

A typical C# FunctionGraph project is typically structured as follows:

.. code-block:: text

  /simple
   ├─ Program.cs
   ├─ handler.txt
   └─ simple.csproj


C# program file
^^^^^^^^^^^^^^^

The main logic for the function resides in C# file Program.cs.

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
      }
    }

C# project file
^^^^^^^^^^^^^^^

.. code-block:: xml
   :caption: simple.csproj

    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <NuGetAudit>false</NuGetAudit>
        <CheckEolTargetFramework>false</CheckEolTargetFramework>
      </PropertyGroup>

      <PropertyGroup>
        <OutputType>Library</OutputType>
        <TargetFrameworks>net6.0;netcoreapp3.1;netcoreapp2.1</TargetFrameworks>
        <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
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

      <ItemGroup Condition="'$(TargetFramework)'=='net6.0'">
        <PackageReference Include="OpenTelekomCloud.Serverless.Function.Common" Version="*-*" />
      </ItemGroup>

      <ItemGroup Condition="'$(TargetFramework)'=='netcoreapp3.1' or '$(TargetFramework)'=='netcoreapp2.1'">
        <PackageReference Include="OpenTelekomCloud.Serverless.Function.Common.legacy" Version="*-*" />
      </ItemGroup>

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


Build the project
^^^^^^^^^^^^^^^^^

Run command:

.. code-block:: bash

   dotnet build

This command builds the project for all target frameworks
defined in **simple.csproj** and creates a zip file for each target framework
in the project folder.

The generated zip files are:

- simple_net6.0.zip
- simple_netcoreapp3.1.zip
- simple_netcoreapp2.1.zip

Deploy the function
^^^^^^^^^^^^^^^^^^^^^
You can deploy the generated zip files to FunctionGraph
as HTTP Function using the console.

