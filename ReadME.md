 # Task Description
 To implement an API with 2 endpoints:
 - that allows users to create a payment and persist in a datastore.
 - that returns the payment together with associated order.

# Assumption(s)
- order have to be created before a payment request is made. thus the need for implementing endpoints for order workflow
- amount to accept in base currency such as cents, penny, kobo, etc thus the need for saving as integer
- currency code should not be more than 7 characters
- default payment status is Created, given the scope from the description service/criteria to change payment status not defined 

# Implementation
With clean architecture in mind, injecting different parts to achieve the workflow.
This approach spells `easy maintainence` from the get-go(loosely coupled parts),

- Approach with response messages
    - the service layer do the heavy lifting for this project and responses.
    `code` in the `ServiceResponse` class is to enable in-house team member tell right away what an issue is, this helps in resolving customer related issues, trust me.

- Approach with unit testing
    -  initially, I had resulted to `in-memory` and mocking for unit testing, but for this project  <br/>
        I decided to use an actual db instance, reasons being that, I wanted to test relationship constraints <br/>
        between `orders` and `payment`
    - trade offs
        - testing is heavily dependent on db
    - advantages
        - tests can be configured to run on different environment - dev, staging

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
- [x] setup postman collection for endpoints <br/>
- [ ] setup github workflows for CI <br/>

## How to run
- [ ] configure connection strings in `appsettings.json` in API project and `test.json` in Test project
- navigate to `BezCepay.API` run `dotnet run`

- using docker

- db configuration in `docker-compose.yml` and/or `dockerfile` should correspond with connection string below
- connection string should look like: `"Host=db;Port=5432;Database=<dbname>;Username=<username>;Password=<password>"`
- Ensure `docker` and `docker-compose` is installed
- run `docker-compose up`


## Testing
- Unit testing
    - [ ] Ensure `test.json` is configured appropriately
    - Navigate to `BezCepay.Tests` run `dotnet test`
- E2E (postman collection below)
    - [https://www.getpostman.com/collections/586e327ddc57d4afc5ae](https://www.getpostman.com/collections/586e327ddc57d4afc5ae)


## Improvement
- More test coverage
- Pagination: add support to paginate responses and self linking
- CI : we'd need to configure a remote db instance that the test cases can run on, afterwards CI can be done