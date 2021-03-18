# Chapter 8: Logging

Within Azure, logging and alerting are the two primary activities used for performance monitoring and troubleshooting a managed service.

- [Azure Monitoring Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/data-platform-logs) - ~6 min.

- [Log Analytics Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-overview) - ~6 min.

- [Log Query Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-query-overview) - ~3 min.

- [Troubleshooting Azure Digital Twins with Logs](https://docs.microsoft.com/en-us/azure/digital-twins/troubleshoot-diagnostics) - ~10 min. Create a Log Analytics Workspace and query Azure Digital Twin Logs

## Key Points

- Per tenant, Azure Monitor is a shared service instance that uses Log Analytics to log the internal behavior of one or more managed services.
- Enabling logging for a managed service (i.e. Azure Digital Twins) requires opting in to the shared Log Analytics instance via a Log Analytics Workspace.
- Logging for an Azure Digital Twins (ADT) instance primarily means capturing data about the [ADT data plane](https://docs.microsoft.com/en-us/rest/api/digital-twins/dataplane/twins) and sending that data to a Log Analytics Workspace to be queried for diagnostics and troubleshooting. Logs can also be concurrently sent to an Azure Event Hub or an Azure storage account for archiving.
- When visiting logs from within your ADT instance of the Azure portal, example Kusto queries related to the service are presented and ready for use. Furthermore, custom [Kusto queries](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/) are also possible.

## Learning Exercises

Using the Azure Digital Twins instance created from [Learning Exercises in Chapter 6](06-e2e-sample.md), visit the Azure cloud portal to complete the following:

1. Create a Log Analytics Workspace
1. Register the Log Analytics Workspace with the Azure Digital Twins instance by creating a Diagnostic Settings with all category details including metrics.
1. Find and run the example Kusto query that reports DigitalTwin API Usage from the last time you ran the e2e sample. The query should return a history of reads and writes.
1. Update the query to render a bar chart
1. From your Log Analytics Workspace menu, visit logs to find and run the example Kusto query that reports on the Latest metrics
1. pending

## Experiments

- pending

## Things to Consider

- pending
