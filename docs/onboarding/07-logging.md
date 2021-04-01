# Chapter 7: Logging

Within Azure, logging and alerting are the two primary activities used for performance monitoring and troubleshooting a managed service. This chapter focuses on logging and logging tracked Metrics for Azure Digital Twins

- [Azure Monitoring Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/data-platform-logs) - ~6 min.
- [Log Analytics Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-overview) - ~6 min.
- [Log Query Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-query-overview) - ~3 min.
- [Troubleshooting Azure Digital Twins with Logs](https://docs.microsoft.com/en-us/azure/digital-twins/troubleshoot-diagnostics) - ~10 min.
- [Troubleshooting Azure Digital Twins with Metrics](https://docs.microsoft.com/en-us/azure/digital-twins/troubleshoot-metrics) - ~7 min.

## Key Points

- Per tenant, Azure Monitor is a shared service instance that uses Log Analytics to log the internal behavior of one or more managed services.
- Enabling logging for a managed service (e.g., Azure Digital Twins) requires opting in to the shared Log Analytics instance via a Log Analytics Workspace.
- Logging for an Azure Digital Twins (ADT) instance primarily means capturing data about the [ADT data plane](https://docs.microsoft.com/en-us/rest/api/digital-twins/dataplane/twins) and sending that data to a Log Analytics Workspace to be queried for diagnostics and troubleshooting.
- When visiting logs from within your ADT instance of the Azure portal, example Kusto queries related to the service are presented and ready for use. Furthermore, custom [Kusto queries](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/) are also possible.
- Logs can also be concurrently sent to an Azure Event Hub and then routed to an alternative analytical service like [Time Series Insights](https://docs.microsoft.com/en-us/azure/time-series-insights/overview-what-is-tsi) (TSI). As a result, you don't have to solely rely on Kusto queries to analyze your data.
- It's also possible to log tracked Metrics that come with the ADT platform. Metrics can be routed to and queried from the Log Analytics workspace, however, because many Metrics concerns themselves with data that's tracked over time, they are also great candidates for being routed to a time-series database like TSI's.

## Learning Exercises

Using the Azure Digital Twins instance created from [Learning Exercises in Chapter 5](05-e2e-sample.md), visit the Azure cloud portal to complete the following:

1. Create a Log Analytics Workspace and enable diagnostics in your ADT resource
1. Create or find a Log Analytics query that reports the read and write counts for the history of twins within your ADT instance
1. Update the query to render different visualizations (e.g., barchart, piechart, etc.)
1. Find and run a Log Analytics query that returns a reported twin count from your ADT instance (_Hint:_ Metrics live in a table called `AzureMetrics`.)

## Experiments

- Create a query that reports the billing costs of each operation
- Create a query that checks for a property value anomaly or threshold for a twin entity
- Route logs and/or logged ADT Metrics to TSI and run a time-series analysis query (_Note:_ Use the TSI instance, Event Hub and Azure Function created from the [previous chapter](06-time-series-insights.md).)

## Things to Consider

- What would you expect to show up in the logs and metrics of a healthy ADT instance and the digital twins contained within?
- Which logged information would make the most sense for an Azure Workbook?
- What various scenarios might require a need for Log Analytics to capture ADT Metrics?
- Api calls made to operate on digital twin entities trigger [event notifications](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-interpret-event-data) that can be routed to external services for later analytics (e.g. TSI). However, enabling diagnostics will achieve the same thing by logging information about api calls to Azure Log Analytics. What's the difference between the data reported in api call event notifications and the data reported when logging api calls for log analytics?
