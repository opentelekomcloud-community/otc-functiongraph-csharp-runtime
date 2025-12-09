Event Function
==========================

.. toctree::
   :hidden:

   Handler <handler>
   Context <context>
   Sample Project <sampleProject>
   JSON Handling <json>
   Trigger Events <trigger_events/_index>

Event functions can be configured with event triggers and integrate
a variety of OpenTelekomCloud products (such as object storage service OBS,
distributed messaging service RabbitMQ version, cloud log service LTS, etc.).

FunctionGraph C# libraries
--------------------------------
The FunctionGraph C# runtime SDK provides the following libraries
to help you develop C# event functions.

Core libraries
^^^^^^^^^^^^^^^^

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common </libraries/src/OpenTelekomCloud.Serverless.Function.Common>`

  This library provides the Context structure, which contains
  the runtime information of the function and provides
  methods to obtain the runtime information. (**For .NET 6.0 and later versions**)

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.legacy</libraries/src/OpenTelekomCloud.Serverless.Function.Common.legacy>`

  This library provides the Context structure, which contains
  the runtime information of the function and provides
  methods to obtain the runtime information. (**prior to .NET 6.0 versions**)

Event libraries
^^^^^^^^^^^^^^^^

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.APIG </libraries/src/OpenTelekomCloud.Serverless.Function.Events.APIG>`

  Event library for API Gateway events.

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.CTS </libraries/src/OpenTelekomCloud.Serverless.Function.Events.CTS>`

  Event library for Cloud Trace Service (CTS) timer events.

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.DMS4Kafka </libraries/src/OpenTelekomCloud.Serverless.Function.Events.DMS4Kafka>`

  Event library for Distributed Messaging Service for Kafka events.

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.LTS </libraries/src/OpenTelekomCloud.Serverless.Function.Events.LTS>`

  Event library for Log Tank Service (LTS) events.

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.OBSS3 </libraries/src/OpenTelekomCloud.Serverless.Function.Events.OBSS3>`

  Event library for Object Storage Service (OBS) S3 events.

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.OOpenSourceKafka </libraries/src/OpenTelekomCloud.Serverless.Function.Events.OpenSourceKafka>`

  Event library for Open Source Kafka events.

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.SMN </libraries/src/OpenTelekomCloud.Serverless.Function.Events.SMN>`

  Event library for Simple Message Notification (SMN) events.

- :github_repo_master:`OpenTelekomCloud.Serverless.Function.Common.Events.Timer </libraries/src/OpenTelekomCloud.Serverless.Function.Events.CTS>`

  Event library for Timer events.


Additional libraries
^^^^^^^^^^^^^^^^^^^^

OpenTelekomCloud community provides following libraries for C# development:

* The community edition of `OTC SDK for API signing in C# <https://github.com/opentelekomcloud-community/otc-api-sign-sdk-csharp>`_
  provides utility methods to handle request signing.
