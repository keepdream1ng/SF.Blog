# Web level.
This is interface level of the app, and it has Program.cs in it.
With the per level extensions for services registration, I don't need to add db related nuget packages here.
App just talks to the needed services with Mediator requests.

SwaggerUI is configured for testing the API.
Result objects from UseCases library are converted to HTTP responses thanks to the Ardalis.Result.AspNetCore library.

Also since in dotnet 8 we got buldin API for authentication,
I'm using it in my project, because this seems safer than something I would come up with.
It shows up in the SwaggerUI as well.
