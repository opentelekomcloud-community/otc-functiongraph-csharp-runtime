HTTP Function
==========================

.. toctree::
   :maxdepth: 1
   :hidden:

   Transfering Secret Keys Through the Request Header <transferringKeys> 
   Minimal Web API <minimalWebApi>

HTTP functions support mainstream Web application frameworks and can be accessed through a browser or called directly by a URL.


Constraints
-----------

Following are the constraints of HTTP functions:

- HTTP functions can only use APIG triggers.
  According to the forwarding protocol between FunctionGraph and APIG,
  a valid HTTP function response must contain:

  - **body(String)**,
  - **statusCode(int)**,
  - **headers(Map)**, and
  - **isBase64Encoded(boolean)**.

  By default, the response is encoded using Base64.
  The default value of **isBase64Encoded** is **true**.
  For details about the constraints, see Base64 Decoding and
  Response Structure.
- The bound IP address is **127.0.0.1.**
- By default, port **8000** is enabled for HTTP functions.
- By default, an account can create a maximum of 400 functions.
  (This quota can be increased upon request.)
- HTTP functions cannot be executed for a long time,
  invoked asynchronously, or retried.
- When a function initiates an HTTP request, the request IP address
  is dynamic for private network access and fixed for public network access.
- The handler must be set in the **bootstrap** file.
  The bootstrap file is the startup file of the HTTP function.
  The HTTP function can only read bootstrap as the startup file name.
  If the file name is not bootstrap, the service cannot be started.

bootstrap file
------------------------
The bootstrap file must be in the root directory of the deployment package.

.. code-block:: bash
   :caption: Example of bootstrap file for project named myHttpFunction

    # functiongraph requires to listen on port 8000
    export ASPNETCORE_URLS=http://localhost:8000/
    # set content root to $RUNTIME_CODE_ROOT
    export ASPNETCORE_CONTENTROOT=$RUNTIME_CODE_ROOT
    # start the application
    $RUNTIME_CODE_ROOT/myHttpFunction

Common Request Headers of HTTP Functions
-----------------------------------------

HTTP request headers are an important part of the HTTP protocol for
passing metadata.
When a function is invoked, specific metadata or configuration information
can be passed. Following Table describes the common request headers carried
by functions by default.

.. list-table:: Common Request Headers of HTTP Functions
   :header-rows: 1

   * - Header Name
     - Description
   * - X-CFF-Request-Id
     - ID of the current request.
   * - X-CFF-Memory
     - Memory allocated to the function.
   * - X-CFF-Timeout
     - Function timeout.
   * - X-CFF-Func-Version
     - Function version.
   * - X-CFF-Func-Name
     - Function name.
   * - X-CFF-Project-Id
     - Project ID of the function.
   * - X-CFF-Package
     - App to which the function belongs.
   * - X-CFF-Region
     - Region where the function is located.


The key information of HTTP functions can be transferred only through request headers.
For details about how to obtain the AK, SK, and token of HTTP functions, 
see Transferring Secret Keys Through the Request Header. :ref:`transferringKeys-ref`