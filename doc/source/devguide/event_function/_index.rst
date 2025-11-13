Event Function
==========================

.. toctree::
   :hidden:

   Handler <handler>
   Context <context>
   Trigger Events <trigger_events/_index>

Event functions can be configured with event triggers and integrate
a variety of OpenTelekomCloud products (such as object storage service OBS,
distributed messaging service RabbitMQ version, cloud log service LTS, etc.).

FunctionGraph C# libraries
--------------------------------
The FunctionGraph C# runtime SDK provides the following libraries
to help you develop C# event functions:

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common </libraries/src/OpenTelekomCloud.Serverless.Function.Common>`

  This library provides the Context structure, which contains
  the runtime information of the function and provides
  methods to obtain the runtime information. (For .NET 6.0 and later versions)

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.legacy </libraries/src/OpenTelekomCloud.Serverless.Function.Common.legacy>`

  This library provides the Context structure, which contains
  the runtime information of the function and provides
  methods to obtain the runtime information. (prior to .NET 6.0 versions)

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events </libraries/src/OpenTelekomCloud.Serverless.Function.Events>`

  These libraries provide type definitions for trigger events.


OpenTelekomCloud community provides following libraries for C# development:

* The community edition of `OTC SDK for API signing in C# <https://github.com/opentelekomcloud-community/otc-api-sign-sdk-csharp>`_
  provides utility methods to handle request signing.
