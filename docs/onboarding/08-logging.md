# Chapter 8: Logging

Within Azure, logging and alerting are the two primary activities used for performance monitoring and troubleshooting a managed service.

- [Azure Monitoring Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/data-platform-logs) - ~6 min.

- [Log Analytics Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-analytics-overview) - ~6 min.

- [Log Query Intro](https://docs.microsoft.com/en-us/azure/azure-monitor/logs/log-query-overview) - ~3 min.

- [Troubleshooting Azure Digital Twins with Logs](https://docs.microsoft.com/en-us/azure/digital-twins/troubleshoot-diagnostics) - ~10 min. Create a Log Analytics Workspace and query Azure Digital Twin Logs

## Key Points

- Per tenant, Log Analytics is a single logging service instance that logs the internal behavior of a managed service.
- Enabling logging for a managed service is as simple as opting in to Log Analytics via the creation of a Log Analytics Workspace.
- Logging for an Azure Digital Twins (ADT) instance primarily means capturing data about the [ADT data plane](https://docs.microsoft.com/en-us/rest/api/digital-twins/dataplane/twins) and sending that data to Log analytics to be analyzed later for diagnostics and troubleshooting. (when enabling logs, they can also be concurrently sent to an event hub or storage account for archiving)
- When accessing logs from within your ADT instance of the Azure portal, example kusto queries related to the service are ready for use. However, custom queries can also be created.

## Learning Exercises

Using the Azure Digital Twins instance created from [Learning Exercises in Chapter 6](06-e2e-sample.md), use kustos queries to query logged data.

Use your existing ADT instance  and Log Analytics workspace to find and run the example kusto query that reports DigitalTwin API Usage for the Last 24 hours. The query should return a history of reads and writes from the sample.

1. Create a Log Analytics Workspace
1. Find and run the example kusto query that reports DigitalTwin API Usage for the last time you ran the e2e sample. The query should return a history of reads and writes.
1. Pending
1. Pending

## Experiments

- pending

## Things to Consider

- pending