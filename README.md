# SkillFactory final project, so-called virtual internship.
No handholding on this one, all you get is bullet-points for the features.
4 sprints 2 weeks each.
To do something new I decided to implement DDD and clean architecture on this blog website.

Stages:
1. Backend design and development.

App has buldin SQLite database for demonstration purposes. On its creation it is also seeded with Identity Roles and Users:

| Roles      | Admin          | Moderator      | User          |
|------------|----------------|----------------|---------------|
| Id         | 0              | 1              | 2             |
| Emails     | Admin@mail.com | Moder@mail.com | User@mail.com |
| Passwords  | 1234Admin!     | 1234Moder!     | 1234User!     |

2. Designing the Front End.

It feels kind of wrong doing just frontend without wiring it up with view controllers and data, so I did both.
I'm locked to MVC project type by Skillfactory assignment, but thanks to having API controllers ready from the
previous stage I managed to add some interactivity to the pages with AJAX calls to them.
Examples are TagEditView.cshtml and RolesViewAndEdit.cshtml.
Now I can read and debug jQery syntax, that's the way to be job ready in 2024, right?)

3. Validation, logging and global error handling.

I added validation along with the wiring up views and controllers on stage 2.
Logging by assignment needs to be done with NLog, so I configured it with appsetting.json.
Maybe the older versions of ASP.net apps didn't have much logging, but in dotnet 8 it feels that most of it is taken care of.
For this stage, I added Mediatr logging pipeline behavior
and a new type of IExceptionHandler middleware from the latest version of the runtime.

4. Adding an API to a Project.

I had an API with a SwaggerUI on stage 1, it also has a great description for the responses thanks to [Ardalis.Result](https://github.com/ardalis/Result) library.
At this stage, I enabled XML documentation and added summaries and remarks to API endpoints shown in Swagger.
Also added a couple of essential endpoints, queries for which I added while doing frontend on stage 2.