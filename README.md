Salary API
This is a web API project developed using ASP.NET Core that allows for the management of employee salary data. It exposes a set of endpoints that can be used to add, update, delete, and retrieve salary data.

Endpoints
POST /{dataType}/Salary/AddSalary
This endpoint is used to add salary data for a specific data type. The data type is specified in the URL path, and the salary data is provided in the request body as a JSON or XML payload. The request body should include the following fields:

EmployeeName: The name of the employee.
EmployeeId: The ID of the employee.
BaseSalary: The base salary of the employee.
StartDate: The start date of the salary period.
EndDate: The end date of the salary period.
OverTimeCalculator: The method used to calculate overtime pay.
Request Body Example
json
Copy code
{
  "EmployeeName": "John Doe",
  "EmployeeId": "12345",
  "BaseSalary": 5000,
  "StartDate": "2023-04-01",
  "EndDate": "2023-04-30",
  "OverTimeCalculator": "Standard"
}
Response
200 OK: Salary data was successfully added.
400 Bad Request: The request was invalid, and the API was unable to process it.
500 Internal Server Error: An error occurred on the server.
PUT /{dataType}/Salary/{id}
This endpoint is used to update existing salary data. The data type and the ID of the salary to be updated are specified in the URL path, and the updated salary data is provided in the request body as a JSON or XML payload. The request body should include the same fields as the AddSalary endpoint.

Request Body Example
json
Copy code
{
  "EmployeeName": "John Doe",
  "EmployeeId": "12345",
  "BaseSalary": 5000,
  "StartDate": "2023-04-01",
  "EndDate": "2023-04-30",
  "OverTimeCalculator": "Standard"
}
Response
200 OK: Salary data was successfully updated.
400 Bad Request: The request was invalid, and the API was unable to process it.
404 Not Found: The specified salary data was not found.
500 Internal Server Error: An error occurred on the server.
DELETE /{dataType}/Salary/{id}
This endpoint is used to delete existing salary data. The data type and the ID of the salary to be deleted are specified in the URL path.

Response
200 OK: Salary data was successfully deleted.
404 Not Found: The specified salary data was not found.
500 Internal Server Error: An error occurred on the server.
GET /{dataType}/Salary/{id}
This endpoint is used to retrieve salary data for a specific ID. The data type and the ID of the salary to be retrieved are specified in the URL path.

Response
200 OK: Salary data was successfully retrieved.
404 Not Found: The specified salary data was not found.
500 Internal Server Error: An error occurred on the server.
GET /{dataType}/Salary?startDate={startDate}&endDate={endDate}
This endpoint is used to retrieve salary data for a specific date range. The data type, start date, and end date are specified as query parameters.

Response
200 OK: Salary data was successfully retrieved.
500 Internal Server Error: An error occurred on the server.
Testing the API
To test the Salary API,
