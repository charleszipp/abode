# Chapter 3: Digital Twin SDK's and API's

Azure digital twins provides an SDK for multiple languages for programmatic management of models and twins. The following covers the capabilities of the C# SDKs and APIs

- [Use the APIs and SDKs](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-use-apis-sdks)
- [Create Graph with the APIs](https://docs.microsoft.com/en-us/azure/digital-twins/concepts-twins-graph#create-with-the-apis)
- [Model Management](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-manage-model) - ~11 min. Covers how to deploy models to ADT including creating, updates, and removal.

> Model management is especially important for understanding what a CI/CD pipeline for ADT models might need to include.

## Key Points

- The SDK allows programmatic management of models and twins as well as querying the twin graph.
- The SDK is backed by Azure Digital Twins API. The API could be leveraged when a client SDK is not available.
- The SDK authenticates using the Azure.Identity library (DefaultAzureCredentials preferred).
- Twins can be represented in type-safe code as POCOs and JSON Serialization.

## Learning Exercises

Using the models created from [Learning Exercises in Chapter 2](02-digital-twin-definition-language.md), create a console app that is able to do the following operations.

1. Create a model if it does not yet exist
2. Validate your model using the [DTDL Validator](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-parse-models) or the [.NET parser library](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-parse-models#use-the-net-parser-library)
3. Create a twin using the model from #1
4. Update a property of the twin created in #3
5. Read a property from the twin created in #3 and writes the value to the console.

The [Tutorial: Coding with the Azure Digital Twins APIs](https://docs.microsoft.com/en-us/azure/digital-twins/tutorial-code) can be used as a reference during the exercise.

## Experiments

- Write a unit test that validates a model is only created if it doesn't already exist.

## Things to Consider

- What would need to be added, if anything, to facilitate unit testing?
- What configuration is necessary to perform SDK operations? (i.e. ADT instance url)
- What level of access is needed to perform SDK operations?
- How do we ensure the code is consistent with the model schema expressed in the DTDL json files?
