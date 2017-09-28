# mi-share-app

# Digial media library

 -- Borrow Items, Manage items in collection, Manage requests.
 
## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. .

### Prerequisites

What things you need to install the software and how to install them

```
Install Microsoft SQL server


To create application database,
1. Create an empty database in microsoft sql server. 
2. Modify connection string in webconfig to connect to database created in step 1.
3. Using nuget, run code first migration on Mi_Share.Data Class library.
   - enable-migrations
   - add-migration 'Initial database'
   - update-database
```

## Built With

* [ASP.NET MVC](https://www.asp.net/mvc) - The web framework used
* [Autofac](http://docs.autofac.org/en/latest/getting-started/) - Dependency Injection
* [Jquery]



## Acknowledgments

* Generic repository pattern as explained on 'https://chsakell.com/2015/02/15/asp-net-mvc-solution-architecture-best-practices/'

