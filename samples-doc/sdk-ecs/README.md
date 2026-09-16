# ECS samples

Sample on how to start/stop an ECS instance using FunctionGraph with Timer Event and Cron expression
(start every day at 8:00, stop every day at 17:00 in timezone Europe/Berlin).


For reference see:

- [ECS API reference for Batch Operations](https://docs.otc.t-systems.com/elastic-cloud-server/api-ref/apis_recommended/batch_operations/index.html) in API reference.
  
- [Using a Timer Trigger](https://docs.otc.t-systems.com/function-graph/umn/configuring_triggers/using_a_timer_trigger.html) in user manual.

- [Cron Expressions for a Function Timer Trigger](https://docs.otc.t-systems.com/function-graph/umn/configuring_triggers/cron_expressions_for_a_function_timer_trigger.html#functiongraph-01-0908) in user manual.
- 

## Program.cs

This samples demonstrates how to call API using signed requests with SecurityAccessKey/SecuritySecretKey/SecurityToken obtained from an agency.


## ProgramToken.cs

This samples demonstrates how to call API using Token obtained from an agency.


## Deploy to T Cloud Public FunctionGraph

### Prepare deployment

Create deployment package using:

```
dotnet build -c Release
```

This will create deployment package `src/sdk_ecs_net6.0.zip`.

### Deploy to FunctionGraph

Create following FunctionGraph function using FunctionGraph console:

Create function:
- Create with: **Create from scratch**
 - Function Type: **Event Function**
 - Region: **your region**
 - FunctionName: **start_ecs_csharp**
 - Enterprise Project: **default**
 - Runtime: **C' (.NET Core 6.0)**
 - Agency: **specify an agency with ecs start permission**  
   (e.g. Permission `ECS User`)

In **Code** tab, section **Code Source** click `Upload -> Local Zip` and upload `sdk_ecs_net6.0.zip` from previous step on the code page.

Configure function:

- `Basic configuration`
    - Handler for AK/SK/ST: **sdk_ecs::src.Program::HandlerECS**
    - or Handler for Token: **sdk_ecs::src.ProgramToken::HandlerECS**

- `Environment variables`
    - **INSTANCE_ID** = **instance id of ECS instance to start**
    - **REGION** = **your region**
    - **ECS_ENDPOINT** = **https://ecs.eu-de.otc.t-systems.com**

- `Triggers` - create stop trigger to stop instance every day at 17:00:
    - Click `Create Trigger`:
    - Trigger Type: **Timer**
    - Timer Name: **stop**
    - Rule: **Cron expression**,  
      Value: **CRON_TZ=Europe/Berlin 0 0 17 \* \* \***
    - Enable Trigger **enable**
    - Additional Information: **stop**

- `Triggers` - create start trigger to start instance every day at 08:00:
    - Click `Create Trigger`:
    - Trigger Type: **Timer**
    - Timer Name: **start**
    - Rule: **Cron expression**,  
      Value: **CRON_TZ=Europe/Berlin 0 0 8 \* \* \***
    - Enable Trigger **enable**
    - Additional Information: **start**  
   
To test the function create Test events:

- Name: **Start**

    ```json
    {
        "version": "v1.0",
        "time": "2026-04-01T08:00:00+02:00",
        "trigger_type": "TIMER",
        "trigger_name": "TEST_START",
        "user_event": "start"
    }
    ```
    using this event will start the configured ECS instance.  

- Name: **Stop**

    ```json
    {
        "version": "v1.0",
        "time": "2026-04-01T17:00:00+02:00",
        "trigger_type": "TIMER",
        "trigger_name": "TEST_STOP",
        "user_event": "stop"
    }
    ```
  using this event will stop the configured ECS instance.  
