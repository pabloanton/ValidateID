# ValidateID
## Api Test

### The application uses net core 3.1 with EF (in memory database).
### Designed under a simple microservices architecture
### It has two methods: /api/Products/add POST and /api/Products/basket GET

## Initialization
### The application registers in the context of the database the user with Id "1"
### The application registers the user with Id "1" in the database context. Then, to carry out the tests, this user must be used. Another user with Id <> 1 will return BadRequest.
### The same logic applies to products. At the beginning of the application, a series of products will be registered (those that are described in the test statement). Any used product that is not registered in the database will return BadRequest
