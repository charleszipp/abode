# Chapter 2: Digital Twin Definition Language (DTDL)

DTDL is the schema by which models are described. Its important to understand how models are described and what constructs are supported. DTDL is to Azure Digital Twins what DDL (Data Definition Language) is to SQL.

- [Concepts](https://docs.microsoft.com/en-us/azure/digital-twins/concepts-models) - ~11 min. Covers basic concepts of DTDL language
- [Marker tags](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-use-tags) ~10 min. Covers how to define tags, how to provision tag markers to your twins and how to query filtering by tags.
- [Model Management](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-manage-model) - ~11 min. Covers how to deploy models to ADT including creating, updates, and removal.

> Model management is especially important for understanding what a CI/CD pipeline for ADT models might need to include.

- [Ontologies](https://docs.microsoft.com/en-us/azure/digital-twins/concepts-ontologies) - ~3 min. Covers definition and use cases for ontologies: domain-specific sets of models.
- [Brick](https://docs.brickschema.org/intro.html) - Alternative to DTDL and some other aspects of Azure Digital Twins.

## Key Points

- Using Object Oriented analogy, model is a class/type. A twin is an instance of a given model.
- Models are immutable. Once uploaded, they cannot be modified. Changes are made by creating a **new version**.
- Models are versioned. The id contains the version number after the semi-colon
- Models can be grouped/namespaced. The id can contain path segments (delimited by colon) to group models.
- Marker tags are simply properties using a key/value Map as a schema, but they provide a more flexible way to add information at runtime when a twin is created. To accomplish this, a tag property of type map is defined in the model. Then, each twin can define any key/value pair within the tag property. And you can perform queries filtering by these tags. 
- Ontologies are peer-reviewed standardized models for common entities such as buildings, factories, and other physical spaces.

## Learning Exercises

The following exercises involve creating and modifying models using DTDL. The models can be updated via the [Azure Digital Twins Explorer](https://docs.microsoft.com/en-us/azure/digital-twins/quickstart-adt-explorer) or [Azure Digital Twins CLI](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-use-cli).

### Create a new model

1. Create a new model of any device in your home. Include some properties on the model that describe the device. Include telemetry that may be sent from the device.
2. Validate your model using the [DTDL validator](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-parse-models#use-the-dtdl-validator-sample).
3. Upload the model to an Azure Digital Twin instance.
4. Create a new twin from the new model

### Update the model

1. Modify the first model to include a new property.
2. Upload the updated model as a new version.
3. Create a new twin using the updated model.

### Add a marker tag
1. Modify your first model to include a marker tag.
2. Upload the updated model as a new version. 
3. Create new twin using the updated model.
4. Update the tag value of the twin.
5. Perform a query to get results filtering by your tag.

### Create a Relationship

1. Create a new second model.
2. Update the models such that the first and second model are related
3. Upload the models that were updated to establish the relationship.
> Tip: The keyword `target` enables you to target a specific class for that relationship "name". For instance, for model RoundHole, if you set the following:
> ```json
> {
>   "@id": "dtmi:RoundHole;1",
>   "@type": "Interface",
>   "displayName": "RoundHole",
>   "contents": [
>     {
>       "@type": "Relationship",
>       "name": "contains",
>       "target": "dtmi:RoundPeg;1"
>     }
>    ]
> }
> ```
> then you could not set RoundHole to contain an object of type SquarePeg. If RoundHole should be able to contain SmallSquarePeg, then you could represent this as follows (pay attention to the "name" field):
> ```json
> {
>   "@id": "dtmi:RoundHole;1",
>   "@type": "Interface",
>   "displayName": "RoundHole",
>   "contents": [
>     {
>       "@type": "Relationship",
>       "name": "contains_roundpeg",
>       "target": "dtmi:RoundPeg;1"
>     },
>     {
>       "@type": "Relationship",
>       "name": "contains_smallsquarepeg",
>       "target": "dtmi:SmallSquarePeg;1"
>     }
>    ]
> }
> ```

## Experiments

- What happens if you try to create the same model twice without changing the version?
- Can multiple models be uploaded or only one at a time?
- Try following the tip in 'Create a Relationship' to give your model a relationship that targets a specific class. Are you able to create a relationship of that "name" with an object that is not that "target" class?

## Things to Consider

- How often should a new version of the model be uploaded? Should each deployment create new versions?
- What is the appropriate use of Property vs Telemetry?
- When are relationships necessary? What capability do they introduce from a query standpoint?
- How can we leverage marker tags when there is a need to reuse common attributes?