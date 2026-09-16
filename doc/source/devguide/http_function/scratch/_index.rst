.. _devguide_http_function_scratch_index:

Building FunctionGraph HTTP Functions with C# from scratch
==========================================================================

.. toctree::
   :hidden:


Constraints for building HTTP functions from scratch
----------------------------------------------------

Additional to the constraints described in :ref:`building_with_csharp`, when building
HTTP functions from scratch, the following constraints apply:

- The handler must be set in the **bootstrap** file.
  The bootstrap file is the startup file of the HTTP function.
  The HTTP function can only read bootstrap as the startup file name.
  If the file name is not bootstrap, the service cannot be started.

bootstrap file
^^^^^^^^^^^^^^^^^^^^^^^^^^^^

The bootstrap file must be in the root directory of the deployment package.

.. code-block:: bash
   :caption: Example of bootstrap file for project named myHttpFunction

    # functiongraph requires to listen on port 8000
    export ASPNETCORE_URLS=http://localhost:8000/
    # set content root to $RUNTIME_CODE_ROOT
    export ASPNETCORE_CONTENTROOT=$RUNTIME_CODE_ROOT
    # start the application
    $RUNTIME_CODE_ROOT/myHttpFunction


Example
------------

See: :github_repo_master:`HTTP Minimal Web API Sample <samples-doc/http_minimalWebAPI>`
for an example of creating an HTTP function with C#.