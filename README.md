# Salary Web API
This is a web API project developed using ASP.NET Core that allows for the management of employee salary data. It exposes a set of endpoints that can be used to add, update, delete, and retrieve salary data.

## Dependencies
* ASP.NET Core 6

## Installation
1. Clone this repository to your local machine.
2. Build the project using Visual Studio or the .NET Core CLI.
3. Run the project.

## Endpoints
### `POST /{dataType}/Salary/AddSalary`
This endpoint is used to add salary data for a specific data type. The data type is specified in the URL path, and the salary data is provided in the request body as a JSON or XML or Custom payload.

## Request Body
The request body of the `AddSalary` endpoint should contain a JSON object with the following properties:
* `FirstName (string, required)`: The name of the employee.
* `LastName (string, required)`: The lastName of the employee.
* `BaseSalary(decimal, required)`: The base salary of the employee.
* `Transportation (decimal, required)`: The amount of money allocated for transportation expenses.
* `Allowance (decimal, required)`: The amount of money allocated for other expenses, such as food or housing.
* `PayDate(DateTime, required)`: This field epresents the date when the salary is paid. This field is a required field and should be provided in the format yyyy-MM-dd
* `OverTimeCalculator (string, required)`: The type of overtime calculator to use when calculating the salary. Must be one of "CalculatorA", "CalculatorB", or "CalculatorC".

