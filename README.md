# Salary Web API
This is a web API project developed using ASP.NET Core that allows for the management of employee salary data. It exposes a set of endpoints that can be used to add, update, delete, and retrieve salary data.

## Dependencies
* ASP.NET Core 6

## Installation
1. Clone this repository to your local machine.
2. Build the project using Visual Studio or the .NET Core CLI.
3. Run the project.

## Usage
To use the API, make HTTP requests to the following endpoints:

* `POST /api/{dataType}/salary/add`: Add a new salary record.
* `PUT /api/{dataType}/salary/{id}`: Update an existing salary record.
* `DELETE /api/{dataType}/salary/{id}`: Delete a salary record.
* `GET /api/{dataType}/salary/{id}`: Get a salary record by ID.
* `GET /api/{dataType}/salary?startDate={startDate}&endDate={endDate}`: Get salary records by date range.

## Endpoints
### `POST /{dataType}/Salary/AddSalary`
This endpoint is used to add salary data for a specific data type. The data type is specified in the URL path, and the salary data is provided in the request body as a JSON or XML or Custom payload.

**Request Body**

The request body of the `AddSalary` endpoint should contain a JSON object with the following properties:
* `FirstName (string, required)`: The name of the employee.
* `LastName (string, required)`: The lastName of the employee.
* `BaseSalary(decimal, required)`: The base salary of the employee.
* `Transportation (decimal, required)`: The amount of money allocated for transportation expenses.
* `Allowance (decimal, required)`: The amount of money allocated for other expenses, such as food or housing.
* `PayDate(DateTime, required)`: This field epresents the date when the salary is paid. This field is a required field and should be provided in the format yyyy-MM-dd
* `OverTimeCalculator (string, required)`: The type of overtime calculator to use when calculating the salary. Must be one of "CalculatorA", "CalculatorB", or "CalculatorC".

**Request Body Example**
```json
{
  "data": {
    "firstName": "Yousef",
    "lastName": "Shahrezaie",
    "basicSalary": 28000000,
    "allowance": 2000000,
    "transportation": 800000,
    "payDate": "2023-04-07"
  },
  "overTimeCalculator": "CalculatorA"
}
```

**Response**
* `200 OK`: Salary data was successfully updated.
* `400 Bad Request`: The request was invalid, and the API was unable to process it.
* `404 Not Found`: The specified salary data was not found.
* `500 Internal Server Error`: An error occurred on the server.

### `DELETE /{dataType}/Salary/{id}`
This endpoint is used to delete existing salary data. The data type and the ID of the salary to be deleted are specified in the URL path.

**Response**
* `200 OK`: Salary data was successfully deleted.
* `404 Not Found`: The specified salary data was not found.
* `500 Internal Server Error`: An error occurred on the server.

### `GET /{dataType}/Salary/{id}`
This endpoint is used to retrieve salary data for a specific ID. The data type and the ID of the salary to be retrieved are specified in the URL path.

**Response**
* `200 OK`: Salary data was successfully retrieved.
* `404 Not Found`: The specified salary data was not found.
* `500 Internal Server Error`: An error occurred on the server.

### `GET /{dataType}/Salary?startDate={startDate}&endDate={endDate}`
This endpoint is used to retrieve salary data for a specific date range. The data type, start date, and end date are specified as query parameters.

**Response**
* `200 OK`: Salary data was successfully retrieved.
* `500 Internal Server Error`: An error occurred on the server

## Testing
To test the API, you can use tools like Postman or curl to make HTTP requests to the above endpoints. You can also use unit tests to test individual components of the API.
