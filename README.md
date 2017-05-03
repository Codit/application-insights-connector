# Logic App Connector - Azure Application Insights

![Build Status](https://tomkerkhove.visualstudio.com/_apis/public/build/definitions/c8608c00-3475-43b1-944b-c86b95825768/7/badge)

Deploy this Web API as an Azure API App and use it in your Azure Logic Apps to send traces, metrics and custom events to Azure Application Insights.

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/) 

This is a workaround for this [UserVoice item](https://feedback.azure.com/forums/287593-logic-apps/suggestions/16833526-supporting-ai-for-logic-apps)

-----------------------------------------------------------------

**Pricing** - *Azure Logic Apps bills for each execution of a connector. Finding a balance in using this connector is crucial as it might have a big impact on your Azure bill.*

-----------------------------------------------------------------

# Features
Here is a list of the current features:

- **Support for writing traces**
	- Specify severity level. *(Optional)*
- **Support for metrics**
	- Support for simple metrics as well as sampling
- **Support for custom events**
- **Support for providing context** *(optional*)
	- Provide more information about the current context of each trace, custom event & metric
- **Support for cross Application Insights usage**
	- Specify a fixed telemetry id on the API App itself for all requests
	- Specify the telemetry id for each request to be more flexible and re-use one API App for multiple instances

You can explore the Swagger of this connector [here](https://application-insights-connector.azurewebsites.net/swagger/).

# Impact on pricing
Bear in mind that each execution of a connector in Azure Logic Apps is billed. Finding a balance in using this connector is crucial as it might have a big impact on your Azure bill.