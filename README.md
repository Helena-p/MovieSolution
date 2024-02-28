# Movie Shop Online
## Overview
This project is an assignment during my participation in the C#/.Net program at Lexicon. 
The project are intended to be a ASPNetCore MVC group project. However, I am given the opportunity to tailor the project according to the technology stack at my future employer, Miljödata. 
I therefore carry out the project on my own and develop the application using Blazor Server. I would like to thank Lexicon for giving me this opportunity.
## Project pre-requisites
### Database
- No scaffolding.
- Ef Core code first approach.
- Rows required.
### Application
- Landing page
	- Display 5 most popular (based on orders), most recent, cheapest, and oldest products.
- View all products page
	- Display each product with title, director, release year and price.
	- Button to add to cart.
	- Optional pagination, and search field.
- View product details page
	- Form with fields to add movie to database (title, director, release year, price).
	- Optional to add img url, restricted access (roles) as admin to upload new movies.
- Shoppingcart page
	- Display all items in cart with option to add/remove from cart.
	- Use session storage.
	- Place order button (on click – create order with user email if registered user. If not registered user => register form).
	- On successful order redirect to order confirmation page.
- Customer dashboard page
	- View previous orders (each order displays items ordered + total cost).
	- Total count of orders.
	- The url should look similar to customers/orders/{email address}.
### Deviation from pre-requisites
By implementing ASP.Net Identity roles in the project I found it more suitable to place a 
form for adding new products on the admin page instaed of the product details page. Then show details of the product 
on the details page with the 'Add to cart' button instead of initially on the product card. This descision didn't 
have any negative effect in adding more clicks for the user to navigate to the add-to-cart button. In a production ready
application I would also add a CTA (Call-To-Action) on the landing page.
## Lessons learned
## Built with
- EntityFramework Core
- Blazor Server (.Net 6)
- Microsoft ASPNet Core Identity
- Bootstrap5
