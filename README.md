# full-stack-app-demo

I did use AI heavily to quickly create this app. I've attached the prompts I used in "Codex Prompts.txt". I always try to remain the architect and let
the AI build what I want. 

# Quick Start
## Prerequisites
SQL Server 
.NET 8
Entity Framework
Angular v19

## Setup and run instructions
1. Before running application, create a SQL Server database. Run the scripts inside the db folder. First "tables" and then "seed data".
2. Update the appsettings.development.json file with the connection string to the created DB
2. Navigate to the .client folder using Command Prompt and run "npm i"
3. Open solution using Visual Studio and Start

# Architecture
## Overall
.NET 8 MVC API backend leveraging SQL Server as DB solution with an Angular front-end. Uses Entity Framework for DB access 
except for queries where performance is stressed.

## Database Schema
The DB is comprised of two tables: Product and Category. A Product has a many to one relationship with Category.

## Technology Choices
The project leverages mostly all Microsoft technologies because that is what I'm familiar with. I could have used .NET 9 or 10 but I
have the last LTS installation installed on my machine. Angular for the front-end. 

# Design Decisions

## How you applied Single Responsibility and Dependency Inversion
The Single Responsibility Princicple (SRP) was applied by creating small dedicated classes that "only do one thing". The goal is to have
many small classes instead of a few big ones that do a lot of different things. 

Dependency inversion was satisifed by programming against an interface where it makes sense. This ensures our code relies on 
abstractions instead of concrete classes. Because of this, I can easily switch out our implementations when our needs call for it. For example,
instead of using my concrete repositories, I pass in an interface of that repository. If I ever need to change the data access (maybe
a different database or a different source like a file instead of the DB) I can simply swap out the implementation and Update
my DI. If both implementations implement my interface, my code should just work. 

## EF Core approach and query optimization
I are using a db first approach with Entitity framework. In my case I created the tables in my DB directly and just let EF
know what my DB looks like. I have always taken a DB first approach but would have no problem taking a code first approach if
needed. 

Queries are optimized by using .AsNoTracking(). This ensures that less memory is used and overall less overhead to keep EF 
entities in memory. This paired with DTO projection prevents memory leaks, unwanted EF tracking related mishaps and keeps performance
in mind because it results in smaller queries with a SELECT statement of what exactly is needed and not all the available columns.

## Complex endpoint choice and rationale
I chose the Product Search endpoint because it would allow me to showcase my SQL abilities and because it's a type of pattern
that arises frequently in a web app. A query with multiple filters is always something the end user will use. 

I have a lot of experience writing SQL and have found that working with the smallest result set possible is always the best way
to maintain performance. When implementing a lot of filters, it's recommended to work with PK and FK IDs to create a result set that
can be whittled down to necessary results and then JOIN back to tables to "hydrate" the final result set. 

This approach also allows me to leverage my knowledge of indexing (even though I usually defer to the Query Execution Plan analyzer for 
index suggestions). But basically, you want to formulate your indexes to cover the most used filters, 

## Repository pattern decision and Trade-offs
The Repository pattern ensures SRP when it comes to DB access and one of the benefits of that is that you consolidate reads 
in one place. If performance ever becomes a problem, the access is isolated to one place and making changes will have far-reaching
impacts. 

Also, it consolidates knowledge. If data access is done a certain way, it's documented for the whole team to use. 

When using EF, the Repository pattern might be a bit overkill but I still think it's useful because it's a good practice that
leads to clean and deliberate code. 

## Index strategy
Index strategy is simple because of the lack of real world scenarios. The indexes should leverage the filters that are highly used. 

# What i would Do with More Time

## Unimplemented features and approach
1. I needed to add validation to my entities. Would probably do this at the DTO level to keep my business logic separate from my DB logic
2. Need to add multiple word search capability to Product Search query
3. Need to add CRUD functionality to Angular app-demo
4. I would create a SQL Server Data Tools (SSDT) project to better organize my database artifacts. I would also learn about 
EF migrations as I have a feeling that is what the team leverages as their versioning solution to the DB. 
5. Create an AGENTS.md file with all the patterns I would like implemented so interactions with the AI can focus on the business logic
and not so much the implementation details. 

## Refactoring priorities
1. I would rename some of the files. The AI selected names are not exactly what I like (low hanging fruit)
2. I don't particularly like the BaseRepository pattern. I would make it simpler/cleaner by removing some of the methods that I'm yet to use
3. Clean up the structure in the Angular app. I like "feature" modules to keep things organized. Also rename some of the folders because
I don't like what the AI used

## Production considerations
I think the project is a good base to build on. As of now, I can only see performance being an issue if our Products table reaches a couple million
rows. Maybe side partitioning would be a good strategy at that point. Resouces given to the database server would also need to analyzed.
 

# Assumptions & Trade-offs
Nothing of note. 