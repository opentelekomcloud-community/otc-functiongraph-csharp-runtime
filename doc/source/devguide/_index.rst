.. _building_with_csharp:

Building with C#
========================
.. toctree::
   :hidden:
   :maxdepth: 1

You can run C# code in OpenTelekomCloud FunctionGraph.
FunctionGraph provides runtimes for C# that run your code to process events.

FunctionGraph Types
-------------------

FunctionGraph provides 2 types of functions:

* **Event Functions**

  Event functions can be configured with event triggers and integrate
  a variety of OpenTelekomCloud products
  (such as object storage service OBS, distributed messaging service
  RabbitMQ version, cloud log service LTS, etc.).

  See :doc:`Event Function <event_function/_index>`

* **HTTP Functions**

  HTTP functions support mainstream Web application frameworks and can
  be accessed through a browser or called directly by a URL.

  See :doc:`HTTP Functions <http_function/_index>`

Both types of functions can be built either **from scratch** or by using **container image**.


Supported C# Runtimes for building from scratch
-----------------------------------------------

FunctionGraph currently supports the following C# runtimes
for building functions from scratch:

.. list-table:: Supported C# runtimes
   :header-rows: 1

   * - Runtime
     - Description
     - Identifier
   * - C# 2.1
     - Supports .NET Core 2.1 applications.
     - C#(.NET Core 2.1)
   * - C# 3.1
     - Supports .NET Core 3.1 applications.
     - C#(.NET Core 3.1)
   * - C# 6.0
     - Supports .NET 6.0 applications.
     - C#(.NET 6.0)
   * - C# 8.0 (expected 1Q 2026)
     - Supports .NET 8.0 applications.
     - C#(.NET 8.0)

Supported C# Runtimes for building using container image
---------------------------------------------------------

For building functions using container image, you can use any
C# version that meets the requirements of your custom container image.

Set up development environment
---------------------------------
To build and run the C# runtime for FunctionGraph, you need to set up your development environment
by installing the C# programming language.


Operating system
^^^^^^^^^^^^^^^^^^^^

This guide assumes that you are using a Unix-like operating system such as

- Windows Subsystem for Linux (WSL)
  see `How to install Linux on Windows with WSL <https://learn.microsoft.com/en-us/windows/wsl/install>`_,
- Linux,
- macOS.

Install C# and IDE
^^^^^^^^^^^^^^^^^^^^

Follow the installation instructions provided on the `Microsoft .NET website <https://learn.microsoft.com/en-us/dotnet/core/install/>`_
to install C# and an IDE on your system.

.. note::
   Examples in this documentation were created using:

   - WSL and
   - Visual Studio Code.
