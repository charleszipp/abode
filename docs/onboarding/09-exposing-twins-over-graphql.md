# Chapter 9: Exposing Digital Twins over GraphQL

- [GraphQL in 100 Seconds](https://www.youtube.com/watch?v=eIQh02xuVw4) - Explains value prop and core concepts in concise video
- [Why GraphQL?](https://www.apollographql.com/docs/intro/benefits/) - Explains the value proposition of GraphQL
- [GraphQL Tutorial](https://www.apollographql.com/docs/tutorial/introduction/) - Walks through coding a GraphQL service. **STOP after [Chapter 4 (Write Mutation Resolvers)](https://www.apollographql.com/docs/tutorial/mutation-resolvers/)**. Apollo Explorer is not necessary to complete the tutorial. Browsing to localhost address provides means to execute requests.

## Key Points

- GraphQL is a declarative model for exposing data from multiple data sources, such as databases and API's, over a common protocol such as HTTP.
- GraphQL mitigates under-fetching and over-fetching by allowing the consumer to declare what data should be returned.
- Data Sources facilitate operations against a specific source of data such as a database or api.
- Resolvers leverage one or more data sources to fulfill a requested operation.
- Schema defines both the model/state (e.g. Room, Floor) exposed as well as the supported operations (GetRoomsByFloor).
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

1. Create a NodeJS application that queries a twin of at least one model. For example, given a `Floor` model, get all `Floor` twins and output them to the console.
   1. Twins and Models created from previous chapters can be used. Alternatively, the ADT Explorer could be used to create a model and twins for this exercise
   2. Samples are available [here](https://github.com/Azure/azure-sdk-for-js/tree/master/sdk/digitaltwins/digital-twins-core/samples/v1/javascript) that demonstrate the use of the ADT Javascript SDK
2. Define a GraphQL schema that represents the GraphQL equivalent of your Digital Twin model. For example, given a model of `Floor`, create a GraphQL schema type for `Floor`.
3. Implement the logic from the previous step as a function in a [`DataSource`](https://www.apollographql.com/docs/apollo-server/data/data-sources/) class. When the application is executed from the console, have it execute the data source function directly.
   1. This [tutorial](https://www.apollographql.com/docs/tutorial/data-source/) and [these docs](https://www.apollographql.com/docs/apollo-server/data/data-sources/) can be used as recap on Data Sources

```javascript
//index.js example

import { MyDataSource } from './my-data-source';

const data = new MyDataSource();
const floors = data.GetFloors();

console.log('Floors: ', floors);

// async example
//data.GetFloors()
//    .then((floors) => console.log('Floors: ', floors));
```

4. Modify the code from the previous step to retrieve the twins from a [Resolver](https://www.apollographql.com/docs/apollo-server/data/resolvers/) rather than the data source. The Resolver should utilize the DataSource class from the previous step.
   1. This [tutorial](https://www.apollographql.com/docs/tutorial/resolvers/) and [these docs](https://www.apollographql.com/docs/apollo-server/data/resolvers/) can be used to recap on Resolvers.

```javascript
import { MyResolver } from './my-resolver'

const floors = MyResolver.Query.floors();
```

5. Using the Schema, Data Source, and Resolver from the previous steps, setup a [GraphQL Server](https://www.apollographql.com/docs/tutorial/resolvers/#add-resolvers-to-apollo-server). At this point it should be possible to execute a GraphQL query from http://localhost:4000/ that retrieves the twins.

## Experiments

1. Implement a `Mutation` that adds a new twin. For example, given a `Floor` model, implement a `AddFloor` mutation that adds a new `Floor` twin.
2. Add another model that has a relationship to the first. Update the query implemented in the exercises to include the related twins. Using the previous example, update `GetFloors` to return all floors and their related `Rooms`.

## Things to Consider

1. What would be the responsibilities of the GraphQL service in a Digital Twins solution?
2. How would we reconcile the GraphQL Schema with the Digital Twin Models (DTDL)?
    1. Would these be 1:1?
        1. If so, how might we keep them in sync?
        2. If not, why are they different?
3. How would we test the GraphQL service?
    1. Do unit tests apply? If so, what might those look like?
