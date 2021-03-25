# Chapter 7: Query Performance

The Azure Digital Twins graphs that are constructed by digital twins and relationships can be queried using a custom SQL-like query language, referred to as the Azure Digital Twins query language. You can use this query language to retrieve digital twins according to their properties, models, relationships (or properties of relationships).

- [Query Language reference and common pitfalls](https://docs.microsoft.com/en-us/azure/digital-twins/concepts-query-language)
- [Query the Azure Digital Twins twin graph](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-query-graph)
- [How to extract query charges incurred from response](https://docs.microsoft.com/en-us/azure/digital-twins/concepts-query-units)

## Key Points

- In Azure Digital Twins, CPU, IOPS and memory are abstracted away. Instead, the relevant number for measuring both performance and query cost is the Azure Digital Twins Query Unit (QU). The QU is essentially a single unit of generalized computation. Thus, when discussing improving performance in an ADT instance, we care about reducing the QU needed for each query.
- QU consumption is affected by:
  - **The complexity of the query.** A query is considered more complex if it has more operations. For instance, up to 5 `JOIN`s are allowed in a single query. Using combination operators (`AND`, `OR`) you can build compound queries of multiple query types (`FROM`, `JOIN`, `WHERE`, etc.)
  - **The size of the result set.** If you ran two queries of equal complexity, but the second returned 50x the results as the first, the second query would consume more QU.
- You can learn the QU's consumed by each query by looking at the response header. Each response from ADT has a field called "query-charge", where this measurement is recorded. A developer may find this very handy for metrics.
- Based on what affects QU, we can deduct that the two best ways to improve query performance are: 1) simplify queries as much as possible, while 2) keeping the results set small.
- There are a few methods of doing this. The second link above gives a great example of how you would avoiding re-rerunning queries or overuse of the `JOIN` operator by using collections and the `IN` operator. For further clarity on this, we use this same example and also show what it would look like without using `IN`. Their example examined finding all the rooms in a building which are too hot.
  1. The first step of this is to get all the floors in the building.

      ```SQL
      SELECT Floor
      FROM DIGITALTWINS Building
      JOIN Floor RELATED Building.contains
      WHERE Building.$dtId = @buildingId
      ```

  2. You then make a list from the response object.

      ```C#
      var floors = "['floor1','floor2', ..'floorN']"; 
      ```

  3. A naive approach would be for each floor, run this query [here we say the ID of the current floor we are iterating through is floorNId]:

      ``` SQL
      SELECT Room
      FROM DIGITALTWINS Floor
      JOIN Room RELATED Floor.contains
      WHERE Floor.$dtId = 'floorNId'
      AND Room. Temperature > 72
      AND IS_OF_MODEL(Room, 'dtmi:com:contoso:Room;1')
      ```

      However, this can be improved by using the IN operator.

      ```SQL
      SELECT Room
      FROM DIGITALTWINS Floor
      JOIN Room RELATED Floor.contains
      WHERE Floor.$dtId IN ['floor1','floor2', ..'floorn']
      AND Room. Temperature > 72
      AND IS_OF_MODEL(Room, 'dtmi:com:contoso:Room;1')
      ```

- You can also reduce the size of the results returned a few ways. By using projections in the `SELECT` statement, you can choose which columns a query will return. Also, you can choose to only return the "top" items in a query using the `TOP` clause.

## Learning Exercises

- Using the resources you have created so far, or the console app that you built in [Learning Exercises in Chapter 3](03-sdks-and-apis.md), practice writing a simple query and ensure it returns expected results. You can use the models, twins and relationships you have created in the previous exercises. Alternatively, you can import the model and graph [examples](https://github.com/Azure-Samples/digital-twins-explorer/tree/master/client/examples) that are available in the ADT Explorer sample you installed in [Chapter 1](01-adt-overview.md).
- Write a relationship-based query, using a `FROM` statement followed by a `JOIN` statement with the keyword `RELATED`. Examples: find all the buildings in a region, find all the employees in a building, etc.
- Use the `IN` operator and a local collection to run a few more specific queries. Using one of the above examples, you could query all the buildings in a region with more than three floors, or all the employees who work in California.
- Practice using the `TOP` clause. Example: return only the top five buildings with the most employees (or other)
- Practice using the `SELECT` statement with a [projection](https://docs.microsoft.com/en-us/azure/digital-twins/how-to-query-graph#filter-results-specify-return-set-with-projections) to return a property of a twin. Example: return only the employee IDs of the employees who work in California (or other).
  > Tip: Ensure the projection property is valid by using the `IS_PRIMITIVE` function.
- Practice using a [Function](https://docs.microsoft.com/en-us/azure/digital-twins/concepts-query-language#functions) in your query, to check the model type of a twin.
- Verify that the results you got from the above exercises match expected results.

## Experiments

- Find the Query Unit (QU) consumption for your queries.
- Adjust the complexity of a query to see the change in the QU consumption.

## Things to Consider

- What are some of the Azure Digital Twins service limits that we should take into consideration?
