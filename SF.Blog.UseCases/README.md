# Domain Use Cases level.
This library contains use cases for the core project, it implements the CQRS pattern with the Mediatr library.
In the outside levels of the app, all you do is send needed command or query to mediator and it will return a requested result,
which may be unsuccessful as well. 
