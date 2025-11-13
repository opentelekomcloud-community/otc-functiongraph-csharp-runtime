Document Database Service DDS
=============================

Using DDS triggers, each time a table in the database is updated, a
Functiongraph function can be triggered to perform additional work. For more
information about how to use DDS triggers, see `Using a DDS Trigger <https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_a_dds_trigger.html>`__.

DDS example event
-----------------

.. literalinclude:: /../../samples-doc/event-dds/resources/dds_event.json
    :language: json
    :caption: :github_repo_master:`dds_event.json <samples-doc/event-dds/resources/dds_event.json>`


Parameter description
---------------------

.. list-table::
   :header-rows: 1
   :widths: 20 15 40

   * - Parameter
     - Type
     - Description
   * - region
     - String
     - The region where the DDS instance is located
   * - event_version
     - String
     - Event protocol version
   * - event_source
     - String
     - Source of the event: **dds**
   * - event_name
     - String
     - Event name
   * - size_bytes
     - Int
     - The number of bytes in the message
   * - token
     - String
     - Base64 encoded data
   * - full_document
     - String
     - Complete file information
   * - ns
     - String
     - Column Name
   * - event_source_id
     - String
     - Event source unique identifier

Example
-------

.. .. literalinclude:: /../../samples-doc/event-dds/Program.cs
    :language: csharp
    :caption: :github_repo_master:`Program.cs <samples-doc/event-dds/Program.cs>`
