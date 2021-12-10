 # Task Description

 To implement an API with 2 endpoints:
 - that allows users to create a payment and persist in a datastore.
 - that returns the payment together with associated order.

# Implementation

## Technologies used
- C# .NET Core
- Postgres
- Docker
- ORM - Entity Framework
- Testing  - xunit, Postman


## Checklist
- [x] service layer <br/>
- [x] tests for service <br/>
- [x] endpoints <br/>
- [x] tests for endpoints <br/>
- [x] dockerize app <br/>
- [ ] setup postman collection for endpoints <br/>
- [ ] setup github workflows for CI <br/>

## How to run
- [] configure connection strings in appsettings.json in API project and `test.json` in Test project
- navigate to `BezCepay.API` run `dotnet run`
- using docker