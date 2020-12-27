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
- Log in and access the Cooks' interface
- Create a recipe with the needed ingredients, price and description.
- See all his recipes and his selling.

### Admins' interface :
- Access the weekly dashboard including the following informations : best selling cook of the week, top 5 recipes and best selling cook since the start of the company.
- Resupplying the recipes' product with quantity updates and list of products to buy after clicking on a button.
- Delete a recipe
- Delete a cook from the database

> ## Solution :
**We created a database using MySQL and multiple tables to store our data and created the intefaces using C#**

Our database is based on 8 tables : Client(customer), CreateurdeRecettes(cooks), Commande(order), Recette(recipe), Details_Commande(order details), Fournisseur(supplier), Produit(product) and Ingredients_Recettes(recipe's ingredients)
