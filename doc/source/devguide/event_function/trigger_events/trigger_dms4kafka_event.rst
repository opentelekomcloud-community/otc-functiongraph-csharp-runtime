DMS for Kafka Event Source
=======================================

DMS for Kafka is a message queuing service that provides Kafka premium
instances.

If you use an Kafka trigger for a function, FunctionGraph
periodically polls messages from a specific topic in Kafka and passes
the messages as an input parameter to invoke the function.

For details, see
`Using a Kafka Trigger <https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_a_kafka_trigger.html>`_.

Kafka example event
-------------------

.. literalinclude:: /../../samples-doc/event-kafka/resources/kafka_event.json
    :language: json
    :caption: :github_repo_master:`kafka_event.json <samples-doc/event-kafka/resources/kafka_event.json>`


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

.. .. literalinclude:: /../../samples-doc/event-kafka/Program.cs
    :language: csharp
    :caption: :github_repo_master:`Program.cs <samples-doc/event-kafka/Program.cs>`
