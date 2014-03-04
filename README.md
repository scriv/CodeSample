What is this?
==========

This is a small sample application created to demonstrate my ability to write good quality code and show understanding of ASP.NET MVC, REST APIs (implemented with Web API), front-end frameworks and design patterns.

It's also my first attempt at applying the [Command Query Responsibility Segregation (CQRS) pattern](http://martinfowler.com/bliki/CQRS.html) in an application (using the [SimpleCQRS](https://github.com/tyronegroves/SimpleCQRS) framework).

The solution consists of three components (and a test project):

- Domain: This contains the domain 'entities', commands, command handlers, events and denormalizers
- Data: This contains the Entity Framework read model
- Site: The ASP.NET MVC webisite and Web API

How to run it
------------

Assuming SQL Express is installed and you are running Visual Studio as a user who is a database owner simply running the site from Visual Studio will create the required databases and the site will be usable.
