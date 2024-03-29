﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;
using System.Collections.Generic;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            ProblemEighteen();
            //ProblemNineteen();
            ProblemTwenty();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var totalUsers = _context.Users.ToList().Count;
            Console.WriteLine(totalUsers);
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;
            var expensiveProduct = products.Where(m => m.Price > 150);
            foreach (var product in expensiveProduct)
            {
                Console.WriteLine(product.Name);
            }

            }

            private void ProblemFour()
            {

            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            string name = "David";
            bool containsLetter = name.Contains("e");


            var filterProductName = _context.Products.Where(p => p.Name.Contains("s"));
            foreach (var product in filterProductName)
            {
                Console.WriteLine(product.Name);
            }



        }

        private void ProblemFive()
            {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            DateTime cutOffDate = new DateTime(2016, 01, 01);
            var userOnBoard = _context.Users.Where(u => u.RegistrationDate < cutOffDate);
            foreach (var user in userOnBoard)
            {
                Console.WriteLine(user.Email);
            }

        }

            private void ProblemSix()
            {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            DateTime after2016 = new DateTime(2017, 01, 01);
            DateTime befor2018 = new DateTime(2018, 01, 01);
            var userOnBoard = _context.Users.Where(u => u.RegistrationDate > after2016 && u.RegistrationDate < befor2018);
            foreach (var user in userOnBoard)
            {
                Console.WriteLine(user.Email);
            }
        }

            // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

            private void ProblemSeven()
            {
                // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
                // Then print the users email and role name to the console.
                var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
                foreach (UserRole userRole in customerUsers)
                {
                    Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
                }
            }

            private void ProblemEight()
            {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var customerProducts = _context.ShoppingCarts.Include(sc => sc.Product).Where(u => u.User.Email == "afton@gmail.com");
            foreach (var product in customerProducts)
            {
                Console.WriteLine($"Product Name: {product.Product.Name} Product Price: {product.Product.Price} Quantity: {product.Quantity}");
            }

            }

            private void ProblemNine()
            {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var sumProducts = _context.ShoppingCarts.Include(sc => sc.Product).Where(u => u.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine($"Cart Total: {sumProducts}");

            }

            private void ProblemTen()
            {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var usersInRole = _context.UserRoles.Where(ur => ur.Role.RoleName == "Employee").Select(u => u.User.Id);
            var userShoppingCartProducts = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => usersInRole.Contains(sc.UserId));

            foreach (var shoppingCart in userShoppingCartProducts)
            {
                Console.WriteLine($"Email: {shoppingCart.User.Email}\n Product Name: { shoppingCart.Product.Name}\n { shoppingCart.Product.Price}\n {shoppingCart.Quantity}/n");
            }


            }

            // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

            // <><> C Actions (Create) <><>

            private void ProblemEleven()
            {
                // Create a new User object and add that user to the Users table using LINQ.
                User newUser = new User()
                {
                    Email = "david@gmail.com",
                    Password = "DavidsPass123"
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }

            private void ProblemTwelve()
            {
            // Create a new Product object and add that product to the Products table using LINQ.
                Product newProduct = new Product()
                {
                    Name = "Samsung Galaxy",
                    Description = "Phone",
                    Price = 1000
                };
                _context.Products.Add(newProduct);
                _context.SaveChanges();
            }

    

            private void ProblemThirteen()
            {
                // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
                var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
                var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
                UserRole newUserRole = new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                };
                _context.UserRoles.Add(newUserRole);
                _context.SaveChanges();
            }

            private void ProblemFourteen()
            {
            // Add the product you created to the user we created in the ShoppingCart junction table using LINQ.
            //var addProduct = _context.ShoppingCarts.Where(r => r.ProductId == 8).Select(r => r.ProductId).SingleOrDefault();
            var addProduct = _context.Products.Where(r => r.Id == 8).Select(r => r.Id).SingleOrDefault();
            var userCreated = _context.Users.Where(u => u.Id == 6).Select(u => u.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userCreated,
                ProductId = addProduct
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();

        }

            // <><> U Actions (Update) <><>

            private void ProblemFifteen()
            {
                // Update the email of the user we created to "mike@gmail.com"
                var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
                user.Email = "mike@gmail.com";
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            private void ProblemSixteen()
            {
            // Update the price of the product you created to something different using LINQ.
                var productPriceUpdate = _context.Products.Where(u => u.Id == 8).SingleOrDefault();
                productPriceUpdate.Price = 500;
                _context.Products.Update(productPriceUpdate);
                _context.SaveChanges();
        }

            private void ProblemSeventeen()
            {
                // Change the role of the user we created to "Employee"
                // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
                var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
                _context.UserRoles.Remove(userRole);
                UserRole newUserRole = new UserRole()
                {
                    UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                    RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
                };
                _context.UserRoles.Add(newUserRole);
                _context.SaveChanges();
            }

        // <><> D Actions (Delete) <><>

            private void ProblemEighteen()
            {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            // See problem Seventeen as an example of removing a role relationship

                var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
                _context.UserRoles.Remove(userRole);
                _context.SaveChanges();

        }

        private void ProblemNineteen()
            {
                // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
                // HINT: Loop
                var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
                foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
                {
                    _context.ShoppingCarts.Remove(userProductRelationship);
                }
                _context.SaveChanges();
            }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.

            var removeUser = _context.Users.Where(u => u.Email == "oda@gmail.com");

            foreach (User userToRemove in removeUser)
            {
                _context.Users.Remove(userToRemove);
            }
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
            {
                // Prompt the user to enter in an email and password through the console.
                // Take the email and password and check if the there is a person that matches that combination.
                // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            }

            private void BonusTwo()
            {
                // Write a query that finds the total of every users shopping cart products using LINQ.
                // Display the total of each users shopping cart as well as the total of the toals to the console.
            }

            // BIG ONE
            private void BonusThree()
            {
                // 1. Create functionality for a user to sign in via the console
                // 2. If the user succesfully signs in
                // a. Give them a menu where they perform the following actions within the console
                // View the products in their shopping cart
                // View all products in the Products table
                // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
                // Remove a product from their shopping cart
                // 3. If the user does not succesfully sing in
                // a. Display "Invalid Email or Password"
                // b. Re-prompt the user for credentials

            }

        }
    }
