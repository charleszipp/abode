# Chapter 6: Deploy and run end-to-end sample

The following docs will introduce an end-to-end solution where an Azure Digital Twins instance is connected to other Azure services for management of devices and data.

- [Tutorial: Build out an end-to-end solution](https://docs.microsoft.com/en-us/azure/digital-twins/tutorial-end-to-end) - ~25 min to read. We will be using this tutorial in the learning exercises below.
- [Ingest Telemetry from IoT Hub](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-ingest-iot-hub-data) - ~6 min to read. Example that shows how Azure Digital Twins is driven with data from an [IoT Hub](https://docs.microsoft.com/en-gb/azure/iot-hub/about-iot-hub)
- [Manage endpoints and routes in Azure Digital Twins](https://docs.microsoft.com/en-gb/azure/digital-twins/how-to-manage-routes-portal) - ~9 min to read. Example that shows how to route ADT event notifications to connected resources.

## Key Points

- Azure Digital Twins are the digital representation of real-world environments and can be driven by live device data.
- Azure Functions can be used to ingest and route telemetry from an IoT Hub device into digital twin properties.
- Changes can be propagated through the Azure Digital Twin graph by processing digital twin notifications with Azure Functions, endpoints, and routes.

## Learning Exercises

In this section you will build an end-to-end solution using the [Tutorial: Build out an end-to-end solution](https://docs.microsoft.com/en-us/azure/digital-twins/tutorial-end-to-end) as a guide.

1. Use the [client sample app](https://github.com/Azure-Samples/digital-twins-samples/tree/master/AdtSampleApp) to interact with your Azure Digital Twin instance.
2. Digitally represent the component of a building scenario in your Azure Digital Twins Instance.
3. Update your Azure Digital Twins by processing telemetry data from an IoT Hub device.
4. Update your Azure Digital Twins by processing service-internal data through the Azure Digital Twins graph.

## Things to Consider

- How would you automate the provisioning of the resources and the deployment?
- How would you test this solution to prove that it implements the desired capabilities?
- What troubleshooting options are available?
- What steps would you take, if any, to strengthen the security of the solution?
- How can work be divided between a group of developers building an e2e Azure Digital Twins solution?
- Are there any other events (in addition to telemetry and property changes) that we would want to use in an Azure Digital Twins solution?
- What other services could be used in an Azure Digital Twin end-to-end solution (e.g, Azure Monitor)?
