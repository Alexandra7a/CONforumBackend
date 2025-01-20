

# CON Forum
## Overview

The project leverages a social network format to create an interactive forum. It is dedicated to our university's students to facilitate interactions, learn new information about upcoming events 
administrative requirements, tips for student life. 

## Technical Details 

### RESTful Properties
| **Property**               | **Status**       | **Notes**                                                                 |
|----------------------------|------------------|---------------------------------------------------------------------------|
| Client-Server Architecture | ✅               | Clear separation of concerns.                                            |
| Statelessness              | ✅               | Each request is independent.                                             |
| Uniform Interface          | ✅               | Consistent use of URIs, HTTP methods, and status codes.                  |
| Layered System             | ✅               | Clear separation of layers (controller, services,repository).                       |
| Resource Naming            | ✅               | Follows RESTful conventions.  e.g /api/post/{id}/comments                                          |
| HTTP Status Codes          | ✅               | Appropriate status codes used.                                           |


## Database 
### Why MongoDB
We chose it because of the nature of our task. A social network type of application can be well suited for the flexibility of a noSQL database regarding types, the usage of lists(we needed them for multiple category for a single post for example) 
### Structure
The database is split in more collections, one for each entity. For example, we have:
- posts - keep inside all details about a specific post
- comments - has a parent-child approach for knowing to which comment to answer (another approach would be to keep a list of objects/ids of reply comments, but it would have came with a great overhead)
### Observations
We kept our database remotely in MongoDB Atlas (MongoDB's cloud service), connecting to them using MongoDB Compass. It is a GUI tool that allows you to interact with MongoDB databases, whether they are local(running on your machine) or remote (e.g., hosted on MongoDB Atlas or another cloud provider)

## Technologies
- C#, ASP.NET Core
- MongoDB, Compass MongoDB

## Diagrams
- In *doc/DomainClassDiagram.png*  is shown how the domain entities work together, how the user is in center of all the tasks
- In `doc/LayeredArchitectureClassDiagram.png` the layered architecture is shown and methods contained are mentioned 
