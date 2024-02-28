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
By implementing ASP.Net Identity roles in the project I found it more appropriate to place a 
form for adding new products on the admin page instead of on the details page. Then, show the product details 
on the details page with the 'Add to cart' button instead of as initially on the product card. This descision didn't 
have any negative effect by adding more mouse clicks for the user to navigate to a page to make a purchase. In a production
application I would, in addition, add a CTA (Call-To-Action) on the landing page.
## Lessons learned
### LINQ
Developing the user dashboard I received a bug that had the effect of creating duplicate orders whenever there were more than 
two products in the order. Troubleshooting with the help of one of my teachers, Robert, I learned that <code>Join()</code> can in some 
situations create duplicates. I also unneccessarily imported the <code>orderItems</code> twice into the query. By refactoring the code 
and use <code>Select</code> instead the bug was resolved.<br>
I had great difficulty in constructing a query to display 'Most popular products'. In this query I had to sum the quantity for 
the individual orderItems, sort by highest quantity, and based on productId get the products from the database and convert each to 
productModel in an iteration prior to render them to the user on the index page. The biggest challenge for me was solving how to group 
the orderItems first, and then sum the quantity in order not to render duplicate products and correctly calculate the order quantity. By 
searching online for constructing LINQ queries I realised I could create new objects on the fly to temporarily hold values in the query.
### Blazor WASM vs Blazor SSR
I learned how user interaction differs between client vs server side rendered applications. In this project (Blazor SSR) I discovered this first 
hand when I initially didn't understand why session storage wasn't properly cleared after the user placed his/hers order and was redirected to the 
order confirmation page. By adding <code>StateHasChanged()</code> to the calling method the page refreshed and the execution to clear session storage 
was also visualised in the UI.
## Built with
- EntityFramework Core
- Blazor Server (.Net 6)
- Microsoft ASPNet Core Identity
- Bootstrap5
