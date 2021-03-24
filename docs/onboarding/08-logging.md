# Chapter 8: Logging

Within Azure, logging and alerting are the two primary activities used for performance monitoring and troubleshooting a managed service.

- [Azure Monitoring Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/data-platform-logs) - ~6 min.
- [Log Analytics Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-overview) - ~6 min.
- [Log Query Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-query-overview) - ~3 min.
- [Troubleshooting Azure Digital Twins with Logs](https://docs.microsoft.com/en-us/azure/digital-twins/troubleshoot-diagnostics) - ~10 min. Create a Log Analytics Workspace and query Azure Digital Twin Logs
- [Troubleshooting Azure Digital Twins with Metrics](https://docs.microsoft.com/en-us/azure/digital-twins/troubleshoot-metrics) - ~7 min.

## Key Points

- Per tenant, Azure Monitor is a shared service instance that uses Log Analytics to log the internal behavior of one or more managed services.
- Enabling logging for a managed service (e.g., Azure Digital Twins) requires opting in to the shared Log Analytics instance via a Log Analytics Workspace.
- Logging for an Azure Digital Twins (ADT) instance primarily means capturing data about the [ADT data plane](https://docs.microsoft.com/en-us/rest/api/digital-twins/dataplane/twins) and sending that data to a Log Analytics Workspace to be queried for diagnostics and troubleshooting. Logs can also be concurrently sent to an Azure Event Hub or an Azure storage account for archiving.
- When visiting logs from within your ADT instance of the Azure portal, example Kusto queries related to the service are presented and ready for use. Furthermore, custom [Kusto queries](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/) are also possible.
- It's also possible to track Metrics that come with the Azure Digital Twins platform. Metrics are usually sent to a time-series database but can also be routed to Log Analytics for logging.

## Learning Exercises

Using the Azure Digital Twins instance created from [Learning Exercises in Chapter 6](06-e2e-sample.md), visit the Azure cloud portal to complete the following:

1. Create or find a Log Analytics query that reports the read and write counts for the history of twins within your ADT instance
1. Update the query to render different visualizations (e.g., barchart, piechart, etc.)
1. Find and run a Log Analytics query that returns a reported twin count from your ADT instance (_Hint:_ Metrics live in a table called `AzureMetrics`.)

## Experiments

- Create a query that reports the costs of each operation
- Create a query that checks for a property anomaly or threshold for a twin entity

## Things to Consider

- What various scenarios might require a need for Log Analytics to capture ADT Metrics?
- Which logged information would make the most sense for an Azure Portal Dashboard or Azure Workbook?
