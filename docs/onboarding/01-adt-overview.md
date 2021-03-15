# Chapter 1: What is Azure Digital Twins?

The following docs and videos will introduce what Azure Digital Twins is and what it aims to solve.

- [Overview](https://docs.microsoft.com/en-us/azure/digital-twins/overview) - ~10 min overview.
- [General Availability Announcement](https://azure.microsoft.com/en-us/blog/azure-digital-twins-now-generally-available-create-iot-solutions-that-model-the-real-world/) - ~10 min, Covers the use case, value proposition and platform capabilities
- [High Level Demo](https://www.youtube.com/watch?v=ScmK-bKJ4MI) - ~6 min. High level walk through of the offering.
- *[Customer Deep Dive](https://www.youtube.com/watch?v=Kbv1a_74FC0) - ~1 hour. Dives deep into real-world implementation.

> Strongly encourage spending time watching the customer deep dive as this covers end-to-end development life cycle for real world scenario. This touches on developing models, deploying the models, and more.

## Key Points

- Digital Twins are used to model real-world environments in the digital space.
- Digitizing physical locations allow for real-time, remote, monitoring and analytics of a physical space.

## Learning Exercises

The following exercises will install tooling that will help visualize Digital Twins.

### Install Azure Digital Twins Explorer

Using the [quickstart](https://docs.microsoft.com/en-us/azure/digital-twins/quickstart-adt-explorer) guide as reference.

1. Download the Azure Digital Twins Explorer
2. Create a new Azure Digital Twins Instance
3. Connect the Azure Digital Twins Explorer to your Azure Digital Twins Instance
4. Import existing sample models mentioned in the guide.
5. Query the graph

> Be sure that you have logged into Azure CLI prior to starting the Digital Twins Explorer
> Be sure that your user has been added to the `Azure Digital Twins Data Owner` role at a scope that includes the new digital twins instance.

### Install Azure Digital Twins CLI

Use [this guide](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-use-cli) to install the Azure Digital Twins CLI

## Things to Consider

- What types of projects might have this been useful? What are some past projects that could have leveraged ADT?
- What types of projects would this not be a good fit? What are some past projects where this would not have fit and why?
- What features might be expected from the Azure Digital Twins platform that wasn't mentioned in the reading and videos?
