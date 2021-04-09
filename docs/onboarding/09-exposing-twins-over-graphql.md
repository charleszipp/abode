# Chapter 9: Exposing Digital Twins over GraphQL

- [Why GraphQL?](https://www.apollographql.com/docs/intro/benefits/) - Explains the value proposition of GraphQL
- [GraphQL Tutorial](https://www.apollographql.com/docs/tutorial/introduction/) - Walks through coding a GraphQL service. **STOP after [Chapter 4 (Write Mutation Resolvers)](https://www.apollographql.com/docs/tutorial/mutation-resolvers/)**. Apollo Explorer is not necessary to complete the tutorial. Browsing to localhost address provides means to execute requests.

## Key Points

- GraphQL is a declarative model for exposing data from multiple sources over a common protocol.
- Data Sources facilitate operations against a specific source of data such as a database or api.
- Resolvers leverage one or more data sources to fulfill a requested operation
- Schema defines both the model/state (e.g. Room, Floor) exposed as well as the supported operations (GetRoomsByFloor)
- Clients must leverage an SDK that translates GraphQL operation into a protocol specific implementation (i.e. HTTP) as opposed to directly invoking an HTTP endpoint.
- GraphQL is a standard specification while Apollo Server is one implementation of the GraphQL standard.

> Since Azure Digital Twins also follows a declarative model for defining twins and relationships and, twins are likely to co-exist with other non-twin application data; GraphQL is a natural fit for exposing the aggregate of this information over a common protocol.

## Learning Exercises

### Prerequisites

Javascript/Node will need to be installed/used to complete the exercises using Apollo GraphQL framework. VS Code is suggested IDE for completing these challenges.

1. [NodeJS 14.16.1](https://nodejs.org/dist/v14.16.1/node-v14.16.1-x64.msi)
2. [Azure Digital Twins Data Plane Javascript SDK](https://github.com/Azure/azure-sdk-for-js/tree/master/sdk/digitaltwins/digital-twins-core)
3. [Apollo Server](https://www.npmjs.com/package/apollo-server)

### Exercises

1. Create a GraphQL service that exposes twins of at least one model. For example, given a `Floor` model, implement a `GetFloors` query that returns all `Floor` twins.
2. Add another model that has a relationship to the first. Update the query implemented in #1 to include the related twins. Using the previous example, update `GetFloors` to return all floors and their related `Rooms`.
3. Implement a `Mutation` that adds a new twin. For example, given a `Floor` model, implement a `AddFloor` mutation that adds a new `Floor` twin.

## Experiments

1. Containerize the GraphQL service
2. Deploy to a container orchestrator such as Azure Container Instances, App Services, or Azure Kubernetes Service.

## Things to Consider

1. What would be the responsibilities of the GraphQL service in a Digital Twins solution?
2. How would we reconcile the GraphQL Schema with the Digital Twin Models (DTDL)?
    1. Would these be 1:1?
        1. If so, how might we keep them in sync?
        2. If not, why are they different?
3. How would we test the GraphQL service?
    1. Do unit tests apply? If so, what might those look like?
