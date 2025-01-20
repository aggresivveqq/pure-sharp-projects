# csharp virtual store
Virtual Store

Overview

Virtual Store is a console-based application designed to simulate the functionalities of an online shopping platform. It includes authentication, product management, a shopping cart, and dedicated panels for admin, customers, and sellers. Data is stored in JSON format, providing a lightweight and flexible way to manage information.

Features

Authentication: Secure login system for different user roles (admin, seller, customer).

Product Management: CRUD operations for products.

Shopping Cart: Add, update, and remove items.

Panels:

Admin Panel: Manage users and oversee store operations.

Customer Panel: Browse and purchase products.

Seller Panel: Manage products listed by the seller.

Technologies Used

C#

.NET 8.0

Newtonsoft.Json: For JSON serialization and deserialization.

Project Structure:
Repository:CartRepository,OrderRepository,


Installation

Clone the Repository:

git clone <repository-url>
cd VirtualStore

Open the Project:
Open Store.sln in Visual Studio or your preferred IDE.

Restore Dependencies:
Run the following command to restore dependencies:

dotnet restore

Run the Application:
Use the command below to execute the application:

dotnet run --project Store

Configuration

Data Storage: JSON files (Cart.json, Order.json, Product.json) are located in bin/Debug/net8.0/. These files are used to persist application data.

Custom Settings: Modify JSON files to prepopulate data or update configuration as needed.

Usage

Run the Application: Start the program using the dotnet run command.

Authenticate: Log in as an admin, seller, or customer.

Explore Features:

Admin: Manage users and oversee store operations.

Customer: Browse and shop for products.

Seller: Add, update, and delete products.

Contributing

Fork the repository.

Create a feature branch: git checkout -b feature-name.

Commit your changes: git commit -m 'Add feature-name'.

Push to the branch: git push origin feature-name.

Open a pull request.

License

This project is licensed under the MIT License. See the LICENSE file for details.

Acknowledgments

Newtonsoft.Json for seamless JSON handling.

