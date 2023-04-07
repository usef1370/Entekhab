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
This endpoint is used to add salary data for a specific data type. The data type is specified in the URL path, and the salary data is provided in the request body as a JSON or XML payload. The request body should include the following fields:

* EmployeeName: The name of the employee.
* EmployeeId: The ID of the employee.
* BaseSalary: The base salary of the employee.
* StartDate: The start date of the salary period.
* EndDate: The end date of the salary period.
* OverTimeCalculator: The method used to calculate overtime pay.

