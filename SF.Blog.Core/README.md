# Domain Core level.
This library contains most of the business logic for the blog app, like entities, value objects and aggregates.
I'll try to keep this library free from depending on a database or specific framework.
Having most logic inside the library keeps it testable with unit tests and easy to understand.

Since I try to implement Domain Driven Design most properties of domain objects have private or internal setters, so
only aggregates and domain-level services can change their values.