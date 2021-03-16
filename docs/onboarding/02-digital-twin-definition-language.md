# Chapter 2: Digital Twin Definition Language (DTDL)

DTDL is the schema by which models are described. Its important to understand how models are described and what constructs are supported. DTDL is to Azure Digital Twins what DDL (Data Definition Language) is to SQL.

- [Concepts](https://docs.microsoft.com/en-us/azure/digital-twins/concepts-models) - ~11 min, Covers basic concepts of DTDL language
- [Model Management](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-manage-model) - ~11 min, Covers how to deploy models to ADT including creating, updates, and removal.

> Model management is especially important for understanding what a CI/CD pipeline for ADT models might need to include.

- [Brick](https://docs.brickschema.org/intro.html) - Alternative to DTDL and some other aspects of Azure Digital Twins.

## Key Points

- Using Object Oriented analogy, model is a class/type. A twin is an instance of a given model.
- Models are immutable. Once uploaded, they cannot be modified. Changes are made by creating a **new version**.
- Models are versioned. The id contains the version number after the semi-colon
- Models can be grouped/namespaced. The id can contain path segments (delimited by colon) to group models.
- Ontologies are peer-reviewed standardized models for common entities such as buildings, factories, and other physical spaces.

## Learning Exercises

The following exercises involve creating and modifying models using DTDL. The models can be updated via the [Azure Digital Twins Explorer](https://docs.microsoft.com/en-us/azure/digital-twins/quickstart-adt-explorer) or [Azure Digital Twins CLI](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-use-cli).

### Create a new model

1. Create a new model of any device in your home. Include some properties on the model that describe the device. Include telemetry that may be sent from the device.
2. Upload the model to an Azure Digital Twin instance.
3. Create a new twin from the new model

### Update the model

1. Modify the first model to include a new property.
2. Upload the updated model as a new version.
3. Create a new twin using the updated model.

### Create a Relationship

1. Create a new second model.
2. Update the models such that the first and second model are related
3. Upload the models that were updated to establish the relationship.

## Experiments

- What happens if you try to create the same model twice without changing the version?
- Can multiple models be uploaded or only one at a time?

## Things to Consider

- How often should a new version of the model be uploaded? Should each deployment create new versions?
- What is the appropriate use of Property vs Telemetry?
- When are relationships necessary? What capability do they introduce from a query standpoint?
