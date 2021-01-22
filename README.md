# Masivian
Practice test for the role as backend developer.

## Requirements
1. SQL Server 2008 or higher
2. SDK .Net Core 3.1
3. Postman

## Preparation

### Database
1. From SQL Server Management Studio or VSCode, execute the T-SQL that is stored in the Masivian.sql file
2. Create user 'dev' with password 'D3v', with SQL Server authentication method
3. Allow user 'dev', use database 'Masivian'

> - Verify that SQL Server has mixed authentication
> - From the appsettings.json files, the connection string can be altered
> - By importing the file 'Masivian.postman_collection.json' into Postman, it is possible to access the configured endpoints and run them easily.

## DTOs

### StateDto

| Property | Type |
| - | - |
| Id | Guid |
| Name | String |

### RouletteDto

| Property | Type | OrderBy | Filter |
| - | - | - | - |
| Id | Guid | True | False | False | 
| StateId | Guid | False | False | 
| State | StateDto | False | False |
| Bets | List<BetDto> | False | False |

### BetDto

| Property | Type | OrderBy | Filter | SearhQuery |
| - | - | - | - | - |
| Id | Guid | True | False | False |
| Number | Int | True | False | False |
| IsColor | Bool | True | True | False | 
| Money | Double | True | True (valor exacto) | True |
| UserId | Guid | False | False | False |
| RouletteNumber | Int | True | True | False |
| Prize | Double/NULL | True | True (valor exacto) | False |
| RouletteId | Guid | False | False | False |
| StateId | Guid | False | False | False |
| Roulette | RouletteDto | False | False | False |
| State | StateDto | False | False | False |

### BetForAdditionDto

| Property | Type |
| - | - |
| Money | Double |
| Number | Int |
| IsColor | Bool |
| RouletteId | Guid |

## Launch

> - The web API supports versioning; To demonstrate it, version 0.5 has been marked as obselate and version 1.0 is the only one in operation
> - The web API supports multiple response formats (application / json and application / xml), so a required format can be defined in the 'Accept' header
> - The web API supports CORS, so in a production environment, it will not respond to requests made from localhost
> - The web API supports the sending of error messages, when the data sent is not suitable for the data model
> - The web API will only show the trace of an error if it is being executed from a development environment; in another environment, the message is preset.
> - The 'To list' and 'Pagination list' endpoints are able to sort the objects by some of the fields
> - All query endpoints are able to select the properties to show if it is found in the DTO
> - The 'To list' and 'Pagination list' endpoints are capable of filtering. (Only for 'Bet' endpoints)

### Roulette [Creation]
To create a roulette, it is only necessary to make the HTTP request with the verb 'POST'; by default the status 'close' is assigned

| HTTP verb | URL |
| - | - |
| POST | https://localhost:44322/api/roulettes |

- **Expected response** => 201 Created 

### Roulette [To list]
To list the roulettes created, it is only necessary to make the HTTP request with the verb 'GET'.

| Endpoint | HTTP verb | URL |
| - | - | - |
| Basic | GET | https://localhost:44322/api/roulettes/list |
| OrderBy | GET | https://localhost:44322/api/roulettes/list?orderBy={property} asc/desc,{property} asc/desc,... |
| Fields | GET | https://localhost:44322/api/roulettes/list?fields={property},{property},... |

- **Expected response** => 200 Ok

### Roulette [Pagination list]
To page the registered roulette wheels, it is only necessary to make the HTTP request with the verb 'GET'.

| Endpoint | HTTP verb | URL |
| - | - | - |
| Basic | GET | https://localhost:44322/api/roulettes |
| OrderBy | GET | https://localhost:44322/api/roulettes?orderBy={property} asc/desc,{property} asc/desc,... |
| Fields | GET | https://localhost:44322/api/roulettes?fields={property},{property},... |

- **Expected response** => 200 Ok

### Roulette [By id]
To consult a roulette in specific, it is only necessary to make the HTTP request with the verb 'GET' and know the GUID.

| Endpoint | HTTP verb | URL |
| - | - | - |
| Basic | GET | https://localhost:44322/api/roulettes/{GUID} |
| Fields | GET | https://localhost:44322/api/roulettes/{GUID}?fields={property},{property},... |

- **Expected response** => 200 Ok

### Roulette [Close and Open]
To close and open a roulette, it is only necessary to make the HTTP request with the verb 'PATCH'. If the roulette is open, it closes and if it is closed, it will open.

| HTTP verb | URL |
| - | - |
| PATCH | https://localhost:44322/api/roulettes/{GUID}/switchstate |

- **Expected response** => 204 Not Content

### Bet [Creation]
To create a bet, it is only necessary to make the HTTP request with the verb 'POST'; by default the status 'open' is assigned and in the HTTP request, it is necessary to send the header 'UserId' with the user's Guid. For practical purposes, you can use the following Guid 'a306a086-5fb8-4c58-af4c-d339a50382dd' or generate one on the following [web page](https://www.guidgenerator.com/).

If the bet is by color, it is necessary to set the 'IsColor' property to 'true' and in the 'Number' property define the value in (1) if it is black and (2) if it is red. If you define another number, it will be taken into account if it is even or odd, to define its color.

In the body of the HTTP request, it is necessary to send the BetForAdditionDto data model in application / json (JSON) format.

| HTTP verb | URL |
| - | - |
| POST | https://localhost:44322/api/bets |

- **Expected response** => 201 Created 

### Bet [To list]
To list the bets created, it is only necessary to make the HTTP request with the verb 'GET'.

| Endpoint | HTTP verb | URL |
| - | - | - |
| Basic | GET | https://localhost:44322/api/bets/list |
| Search | GET | https://localhost:44322/api/bets/list?searchQuery={value} |
| OrderBy | GET | https://localhost:44322/api/bets/list?orderBy={property} asc/desc,{property} asc/desc,... |
| Fields | GET | https://localhost:44322/api/bets/list?fields={property},{property},... |
| Filter | GET | https://localhost:44322/api/bets/list?{property}={value}&{property={value},... |

- **Expected response** => 200 Ok

### Bet [Pagination list]
To page the registered bet wheels, it is only necessary to make the HTTP request with the verb 'GET'.

| Endpoint | HTTP verb | URL |
| - | - | - |
| Basic | GET | https://localhost:44322/api/bets |
| Search | GET | https://localhost:44322/api/bets?searchQuery={value} |
| OrderBy | GET | https://localhost:44322/api/bets?orderBy={property} asc/desc,{property} asc/desc,... |
| Fields | GET | https://localhost:44322/api/bets?fields={property},{property},... |
| Filter | GET | https://localhost:44322/api/bets?{property}={value}&{property={value},... |

**Expected response** => 200 Ok

### Bet [By id]
To consult a bet in specific, it is only necessary to make the HTTP request with the verb 'GET' and know the GUID.

| Endpoint | HTTP verb | URL |
| - | - | - |
| Basic | GET | https://localhost:44322/api/bets/{GUID} |
| Fields | GET | https://localhost:44322/api/bets/{GUID}?fields={property},{property},... |

**Expected response** => 200 Ok
