# TTB.Assignment.Api.

# Setup to run project

1.Download and install docker desktop  
2.Open project , set docker-compose as Startup Project  
3.Run docker-compose and Swagger should shown up  


# How to call API

1. First, get access token by call httpPost at https://localhost:8081/login with body
{
    "Username" : "apiuser",
    "Password" : "P@ssw0rd"
}
2. Use access token from response to add bearer token to call others API by clicking at
"Authorize" button in the top right of Swagger page. 

3. Create account first, Call API createSavingAccount. Then you can do other transaction with that account number.

# Decisions and trade-offs
In a limited time, 3 nights , I chose to do an API structure that was easy to use.
and can expand the size of the app in the future. I decided to use serilog for its ability to write logs and 
provide an easy-to-use log structure, and chose to use SqlServer via docker compose for ease of use on other machines. 

And if I had more time, What I might do is
- Unit testing
- Core Banking Service
- Redis caching server
- Improve log structure
- Pipeline code coverage
- Custom exception