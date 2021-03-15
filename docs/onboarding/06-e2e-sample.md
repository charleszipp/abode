# Chapter 6: Deploy and run end-to-end sample

The following docs will introduce an end-to-end solution where an Azure Digital Twins instance is connected to other Azure services for management of devices and data.

## Key Points

- Create an end-to-end scenario that shows Azure Digital Twins being driven by live device data.
- Understand how changes are propagated through the twin graph by processing digital twin notifications.

## Prerequisites

In addition to the Prerequisites defined in the [README](README.md), the following steps are required to complete the learning exercises below:

- [Set up your session](https://docs.microsoft.com/en-us/azure/digital-twins/tutorial-end-to-end#set-up-cloud-shell-session) and register your Azure Subscription with the Azure Digital Twins namespace
- [Add the Azure IoT Extension](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-use-cli#get-the-extension) for Azure CLI
- [Create an Azure Digital Twin](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-set-up-instance-cli#create-the-azure-digital-twins-instance) instance
- Download the sample project [digital-twins-samples](https://docs.microsoft.com/en-us/azure/digital-twins/tutorial-end-to-end#get-required-resources)

## Learning Exercises

The following exercises will help you build an end-to-end solution using the [Tutorial: Build out an end-to-end solution](https://docs.microsoft.com/en-us/azure/digital-twins/tutorial-end-to-end) as a guide.

1. Configure the sample project for your Azure Digital Twin instance
2. Learn about the sample building scenario and run the sample client app
3. Process simulated telemetry from an IoT Hub device using an Azure Function app
4. Propagate changes through the twin graph, by processing digital twin notifications with Azure Functions, endpoints, and routes

## Things to Consider

- What troubleshooting options are available?
- How can we automate the provisioning of the resources and the deployment?
