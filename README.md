# BigLarryAPI
A RESTful API which access the SQL database and get information about specific type of loans and their current processing dates.

## Install
.NET 5 https://dotnet.microsoft.com/download/dotnet/5.0 \
ASP.NET Core

## Get List of Loans of and Dates
### Request
>api/calhfapi/get\
curl -X GET "https://localhost:5001/api/Calhfa/Get" -H  "accept: text/plain"

### Response
 HTTPS 200 OK\
 Content-Type: application/json;\
 Date: Wed08 Dec 2021 05:26:24 GMT \
 
 ## Connection String
 Connection String is located in appsettings.json at line 11
 
 ##SQL Command
 SQL command is located in calhfaServices.cs at line 22
 
 
## Descripton
The API, when prompted with a GET command, will query the SQL database through the connection string (located in the appsettings.json file), sending an sql command that returns 8 items, the loans and their subsequent dates. The API will then assign those values to variables in an object named Output (in the Output.cs file) and serialize that object using JsonSerializer, then return the data requested in json format.
 
In Output, the loan counts are of type int, and the dates are of type String, so they can be converted to a chosen format more easily. The date format is located in a string variable below the SQL command and can be changed freely; the default we're using is MM/dd/yyyy
 
 
