# Back-end of "Present connection" 2023 Internship task
Back-end was made with ASP.NET (.NET 6 version), Swagger (only for development environment) & EF Core in-memmory databse.

Project is based on service-oriented architecture, with toppings and orders having their own service methods. 

Endpoints are handled by toppings and orders controllers. 

Pizza order and topping entities have many-to-many relationship. All toppings must be unique.

## Running production
CI pipeline was set up with Github Actions & Azure.

Deployed API is currently being used by [deployed front-end](https://github.com/JustasBan/WebFrontendPresentConn#running-production).

## Running development

#### Run the project in Visual Studio IDE at `PizzaOrderApi`
#### Run it in console by `dotnet run --environment Development`

Swagger runs at `https://localhost:7088/swagger/index.html`

## Test data for development environment:

### Multiple toppings:
[
  {
    "id": 0,
    "name": "Cheddar"
  },
  {
    "id": 0,
    "name": "Mozzarella"
  },
  {
    "id": 0,
    "name": "Chorizo"
  },
  {
    "id": 0,
    "name": "Kumato"
  },
  {
    "id": 0,
    "name": "Salami"
  },
  {
    "id": 0,
    "name": "Cucumber"
  }
]

### Multiple orders:
[
  {
    "id": 0,
    "size": 0,
    "totalCost": 0,
    "name": "Order for John A.",
    "toppingIds": [
      1, 1, 3, 1
    ]
  },
  {
    "id": 0,
    "size": 1,
    "totalCost": 0,
    "name": "Order for Katherine L.",
    "toppingIds": [
      4, 2
    ]
  },
  {
    "id": 0,
    "size": 2,
    "totalCost": 0,
    "name": "Order for Peter C.",
    "toppingIds": [
      1
    ]
  }
]

## Unit tests
Unit tests test functionality of price estitmation (with and without discount). Testing project uses it's own in-memory database. Made with Xunit.

#### Running tests in Visual Studio IDE: `Run Tests` at `PizzaOrderApi.Tests`
#### Running tests in console: `dotnet test .\PizzaOrderApi.Tests\`
