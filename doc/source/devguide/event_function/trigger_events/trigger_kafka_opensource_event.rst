Kafka (OpenSource) Event Source
=======================================

DMS for Kafka is a message queuing service that provides Kafka premium
instances.

If you use an OpenSource Kafka trigger for a function, FunctionGraph
periodically polls messages from a specific topic in Kafka and passes
the messages as an input parameter to invoke the function.

For details, see
`Using an OpenSource Kafka Trigger <https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_an_open-source_kafka_trigger.html>`_.

OpenSource Kafka example event
-------------------------------

.. literalinclude:: /../../samples-doc/event-opensource-kafka/resources/opensource_kafka_event.json
    :language: json
    :caption: :github_repo_master:`opensource_kafka_event <samples-doc/event-opensource-kafka/resources/opensource_kafka_event.json>`


Parameter description
---------------------

.. list-table::
   :header-rows: 1
   :widths: 20 15 40

   * - Parameter
     - Type
     - Description
   * - event_version
     - String
     - Event version
   * - event_time
     - String
     - Time when an event occurs
   * - trigger_type
     - String
     - Event type: **KAFKA**
   * - region
     - String
     - Region where a Kafka instance resides
   * - instance_id
     - String
     - Kafka instance ID
   * - messages
     - String[]
     - Message content
   * - topic_id
     - String
     - Message ID

Example
-------

.. .. literalinclude:: /../../samples-doc/event-opensource-kafka/src/Program.cs
    :language: csharp
    :caption: :github_repo_master:`Program.cs <samples-doc/event-opensource-kafka/src/Program.cs>`
