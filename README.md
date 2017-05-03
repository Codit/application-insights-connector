# Logic App Connector - Azure Application Insights

![Build Status](https://tomkerkhove.visualstudio.com/_apis/public/build/definitions/c8608c00-3475-43b1-944b-c86b95825768/7/badge)

Deploy this Web API as an Azure API App and use it in your Azure Logic Apps to send traces, metrics and custom events to Azure Application Insights.

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/) 

This is a workaround for this [UserVoice item](https://feedback.azure.com/forums/287593-logic-apps/suggestions/16833526-supporting-ai-for-logic-apps)

-----------------------------------------------------------------

**Pricing** - *Azure Logic Apps bills for each execution of a connector. Finding a balance in using this connector is crucial as it might have a big impact on your Azure bill.*

-----------------------------------------------------------------

## Using the Application Insights connector
Want to use this connector yourself? you can!



### Installation
All you have to do is host this connector as an **Azure API App** that you can use in your Logic App. 
More information can be found [here](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-custom-hosted-api).

Don't want to go through it yourself? Just deploy it with a wizard!

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/) 

### Configuration
TBW

```
<appSettings>
    <add key="ApplicationInsights.InstrumentationKey" value="_Appliation-Insights-Instrumentation-Key_" />
    <add key="ApplicationInsights.Runtime.InstrumentationKey" value="_Appliation-Insights-for-API-App-Instrumentation-Key_" />
</appSettings>
```

## Features
Here is a list of the current features:

- **Support for Traces**
	- Specify severity level. *(Optional)*
- **Support for Metrics**
	- Support for simple metrics as well as sampling
- **Support for Custom Events**
- **Support for providing context** *(optional*)
	- Provide more information about the current context of each trace, custom event & metric
- **Support for cross Application Insights usage**
	- Specify a fixed telemetry id to being used on the API App itself for all requests from the Logic App
	- Specify the telemetry id for each request to be more flexible and re-use one API App for multiple Logic App instances

You can explore the Swagger of this connector [here](https://application-insights-connector.azurewebsites.net/swagger/).