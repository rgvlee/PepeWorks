# PepeWorks

This repo is the result of a hands on training exercise to develop a workforce mobilisation aligned minimal API.

## Database

The `DbUp.linq` file contains a LINQPad program that will create the database. Check the literal folder paths and run
it.

If you need to scaffold the DbContext and models again: 

```text
dotnet ef dbcontext scaffold "Server=; Database=PepeWorks; Trusted_connection=True; TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --context-dir Data --output-dir Data --force
```

## appsettings.json

Specify a connection string for the PepeWorks database.

## Implementation features

- .NET 8 Minimal API
- Vertical slice architecture
- CQRS via MediatR
- API documentation via code/Swagger
- EF Core 8 over SQL Server
- Temporal tables
- Working Get, Create, and Delete endpoints for the Location entity
- Add Location validation via FluentValidation/MediatR pipeline behaviour

## TODO

- ~~Upgrade to .NET 8~~
- ~~Upgrade to EF Core 8~~
- Add integration tests using Alba
    - Consider containerisation for integration tests against a real SQL Server database
- Add an API client
- Add support for other entities
- Add bulk actions (e.g., bulk create, bulk delete)
- Add partial updates (e.g., reallocate a room)
- Add automatic auditing (likely via EF Core interceptors)
- Add CI/CD
- Consider a cloud native deployment, including IaC (likely via NUKE/GitHub Actions/Azure)
    - Consider changing the DB to something cheaper than SQL Server (e.g., Cosmos DB)
- Caching? Performance optimisation? Benchmarking to observe improvements overtime?
- Reduce duplication via generic implementations?
- Guard clauses?
- Consider containerisation
- Complete validation
    - This should also surface in the API documentation e.g., required properties, string max length
- OData (or similar)
  - Point in time support?
- Publish events?

## Additional resources

- I used ChatGPT 3.5 to generate the initial database script
- DbUp: https://dbup.readthedocs.io/en/latest/usage/
- Vertical slice architecture example: https://code-maze.com/vertical-slice-architecture-aspnet-core/
