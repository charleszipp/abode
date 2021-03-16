# Chapter 5: Time Series Insights and Azure Digital Twin

- [Overview](https://docs.microsoft.com/en-us/azure/time-series-insights/overview-what-is-tsi) - ~5 min not including video intro, covers basics of IoT data and TSI
- [Getting Started Tutorial](https://docs.microsoft.com/en-us/azure/time-series-insights/tutorial-set-up-environment) - ~20 min including following steps 
- [Storage in TSI](https://docs.microsoft.com/en-us/azure/time-series-insights/concepts-storage ) - ~6 min to read
- [Integrating ADT with TSI](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-integrate-time-series-insights) - ~30 min including following steps, assuming prerequisite of ADT instance has been met

## Key Points
- Time series data is comprised of repeated measurements or events which are collected over time and indexed in time order. 
- This type of data is very valuable when trying to measure trends or changes, and thus is invaluable to people such as data analysts. 
- The second generation of TSI was made public at the end of 2020. It is a PaaS offering that offloads the work surrounding building a telemetry system, including the ability to explore and perform analysis, and is optimized for time series data as well as IoT solutions.
- Some primary use cases for TSI are data exploration, visual anomaly detection, and analytics. 
- TSI is built to allow for warm storage and cold storage. Data fed in from a streaming service or other source is fed into warm and (if enabled) cold storage. This makes long-term storage of data simple. 

## Learning Exercises

### Ingest data from Event Hub
To see the basics of how TSI can be configured, after walking through the getting started tutorial linked above, walk through [this tutorial](https://docs.microsoft.com/en-us/azure/time-series-insights/how-to-ingest-data-event-hub) to learn how to connect a TSI instance to an input sink.

### Using Time Series Insights with Azure Digital Twin
With some manipulation, ADT can be configured to forward events to TSI for storage. You would accomplish this by creating an event route between ADT and an Event Hub Namespace, forwarding twin changes directly to one Hub. This Hub can feed into an Azure function which converts JSON patch to JSON containing the twin updated, which forwards it into another hub. This second and final hub can then forward events to TSI. [This walkthrough](https://docs.microsoft.com/en-us/azure/time-series-insights/tutorials-model-sync) gives you all the information you need if you wanted to accomplish this.


## Things to Consider
- TSI and cold storage are easy to integrate, but how would that work if you wanted to segment data based on some fields in the data? 
- What alternatives are there to TSI, and in what scenarios is TSI more suitable?