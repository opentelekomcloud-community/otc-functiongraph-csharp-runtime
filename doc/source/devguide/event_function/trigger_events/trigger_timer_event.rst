Timer Event Source
==================

FunctionGraph's timer trigger (Timer Trigger) is a trigger type based
on time scheduling that supports both `Cron expression` and `fixed interval
scheduling` methods.

Through timer triggers, you can implement scheduled tasks,
periodic data processing, scheduled backups, scheduled monitoring,
and other functions.

Timer triggers are particularly suitable for tasks that need to be executed at
fixed time intervals or specific time points, such as data cleanup,
report generation, system monitoring, scheduled notifications, etc.

For details, see
`Using a Timer Trigger <https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_a_timer_trigger.html>`_.

Timer example event
-------------------

.. literalinclude:: /../../samples-doc/event-timer/resources/timer_event.json
    :language: json
    :caption: :github_repo_master:`timer_event.json <samples-doc/event-timer/resources/timer_event.json>`


Parameter description
---------------------

.. list-table::
   :header-rows: 1
   :widths: 20 15 35

   * - Parameter
     - Type
     - Description
   * - version
     - String
     - Event version
   * - time
     - String
     - Time when an event occurs.
   * - trigger_type
     - String
     - Trigger type: **TIMER**
   * - trigger_name
     - String
     - Trigger name
   * - user_event
     - String
     - Additional information of the trigger

Example
-------

.. literalinclude:: /../../samples-doc/event-timer/src/Program.cs
    :language: csharp
    :caption: :github_repo_master:`Program.cs <samples-doc/event-timer/src/Program.cs>`
