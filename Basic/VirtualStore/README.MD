# csharp virtual store
## Virtual Store

## Overview

Virtual Store is a console-based application designed to simulate the functionalities of an online shopping platform. It includes authentication, product management, a shopping cart, and dedicated panels for admin, customers. Data is stored in JSON format, providing a lightweight and flexible way to manage information.

## Features

- Authentication: Secure login system for different user roles (admin, customer).

- Registration: Different user's with user information(carts,orders).

- Product Management: CRUD operations for products.

- Shopping Cart: Add, update, and remove items.

- Orders: Add,update,confirm orders.

- Order status:Have order status,which can easily modified.

## Panels:

Admin Panel: Manage users and oversee store operations.

Customer Panel: Browse and purchase products.


## Technologies Used

- C#

- .NET 8.0

- Newtonsoft.Json: For JSON serialization and deserialization.

## Project Structure:

# Interfaces
- Purpose: Defines the contract for classes that interact with the data (repositories) and business logic in your application.
- Task: Interfaces provide an abstraction for the interaction between different layers of the application (data access, business logic, etc.). They make it easier to manage dependencies and allow for mock testing.
# Repositories
- Purpose: Classes that implement the interfaces defined above to interact with the data source (e.g., database, in-memory storage).
- Task: These classes perform the actual operations to manage data (e.g., saving and retrieving data).
# Admin
- Purpose: Provides functionalities that only an admin can access, such as managing products, users, and viewing reports.
- Task: Allows administrators to perform operations that manage the store, including CRUD operations for products and users, as well as administrative tasks such as generating reports or approving new users.
# Menu
- Purpose: Manages the user interface and provides navigation between different sections of the application.
- Task: Displays a menu for the user to interact with and choose various options, such as logging in, registering, accessing the admin panel, or viewing products. It handles the flow of control based on the user's input.


# Installation

- Clone the Repository:

- git clone <repository-url>

## Install Newtonsoft.json:

- Install-Package Newtonsoft.Json 

- Open the Project:
- Open Store.sln in Visual Studio or your preferred IDE.

- Run the following command to restore dependencies:

- dotnet restore

## Run the Application:

- dotnet run --project Store

## Configuration

Data Storage: JSON files (Cart.json, Order.json, Product.json) are located in bin/Debug/net8.0/. These files are used to persist application data.

Custom Settings: Modify JSON files to prepopulate data or update configuration as needed.

## Usage

Run the Application: Start the program using the dotnet run command.

Authenticate: Log in as an admin or customer.

## Explore Features:

Admin: Manage users and oversee store operations.
Operations:
- CRUD products.
- Show user list(Orders and Carts included).
- Can modify users order.

Customer:Adding products to cart.Create orders.
- Operations:
- Add products to cart.
- Delete products from cart.
- Update amount of products in cart.
- Create orders.



## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgments

Newtonsoft.Json for seamless JSON handling.

