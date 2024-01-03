# Ifrastructure level.
This library contains things that let the app work, like data access stuff,
mapping and mediator commands and queries that depend on infrastructure.

Since I implemented domain-level restrictions on entities with DDD and it is not recommended
to implement authentication and authorization yourself - I used dotnet Identity system,
with mapping to domain entities using automapper.
