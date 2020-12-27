# Cooking
Creation of a computer application for a fictional company named Cooking which core business is to sell dishes online. The project had multiple objectives :
- Create an interface for Cooking's customers on which they can order food.
- Create an interface for Cooking's cooks to register new recipes in the company's Menu.
- Create an interface for Cooking's administrators on which they have access to customers' orders, manage the available recipes and check products' stock.
- Save all the data and informations in a database 


> ## Details of the Subject :

### Customers' interface :
A customer must be able to :
- Log in to their account or to Sign in if they don't have an account yet.
- Place an order which implies look over the available recipes, pick one or multiples recipes.
- Pay their order (a fictional currency called 'Cooks' is used here) using their Cooking wallet that they can refill if needed.

### Cooks' interface :
A recipe creator must be able to :
- Log in and access the Cooks' interface.
- Create a recipe with the needed ingredients, price and description.
- See all his recipes and his selling (for each recipe sold, his wallet is credited).

### Admins' interface :
- Access the weekly dashboard including the following informations : best selling cook of the week, top 5 recipes and best selling cook since the start of the company.
- Resupplying the recipes' product with quantity updates and list of products to buy after clicking on a button.
- Delete a recipe
- Delete a cook from the database

> ## Solution :
**We created a database using MySQL and multiple tables to store our data and created the intefaces using C#**
To create our interfaces we used WPF to make them more aesthetically pleasing for the customers.

### Database :
Our database is based on 8 tables : Client(customer), CreateurdeRecettes(cooks), Commande(order), Recette(recipe), Details_Commande(order details), Fournisseur(supplier), Produit(product) and Ingredients_Recettes(recipe's ingredients) that are linked according to the database structure.

### Application :
We created multiple windows using WPF and C# with each its specificities and functions. 

Let's take a look at our homepage on which we can log in, sign up or access a demo mode (asked by the module assessor):
![homepage](https://github.com/JackyKch/Cooking/blob/main/assets/welcome_page.png)

When logged in, we access the customer interface. (N.B.: to access admin mode enter 'Cooking' as ID and 'admin' as password on homepage) with our wallet, the recipes, a button to order and the option to register as a cook.
![customer](https://github.com/JackyKch/Cooking/blob/main/assets/customer_interface.png)

Here we see the order interface with the available recipes and our shopping basket. We type a recipe's name and the amount needed :
![order](https://github.com/JackyKch/Cooking/blob/main/assets/order_interface.png)

As a cook we have a button on the customer interface that takes us to the cook interface on which a cook can see its recipes and their amount sold.
![cook](https://github.com/JackyKch/Cooking/blob/main/assets/cook_interface.png)

Obviously a cook can create recipes and therefore we built an interface on which we can create a recipe out of the available products :
![recipe](https://github.com/JackyKch/Cooking/blob/main/assets/recipe_interface.png)

