# BlogPost .Net6

* This project consisting of an ASP.NET Core web API implemented with basic CQRS approach. This project provides a basic implementation of a blog post and comment.

# Features

* Create blog posts and Get blog post by Id.
* Add comments on blog posts.

# Technologies Used

* C# 
* .NET 6
* ASP.NET Core 6
* Entity Framework Core 7
* ASPNET Core RateLimit 
* Azure Redis Caching
* OAuth 2.0 
* MediatR


# Run the Project

* Visual studio 2022
* .NET core 6 or later

# Deployment

* Create an azure app service & publish 
* Setup database on azure and configure connection strings 
* For caching use azure radis caching and configure url in appsetting
* Setup Oauth2.0 with azure active directory service and configure clientID and TenantID in appsetting
