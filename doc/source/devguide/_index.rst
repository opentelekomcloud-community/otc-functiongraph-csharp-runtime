Building with C#
========================
.. toctree::
   :hidden:

You can run C# code in OpenTelekomCloud FunctionGraph.
FunctionGraph provides runtimes for C# that run your code to process events.

Supported C# Runtimes
------------------------
FunctionGraph currently supports the following C# runtimes for Event Functions:

.. list-table:: Supported C# runtimes for Event Functions
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