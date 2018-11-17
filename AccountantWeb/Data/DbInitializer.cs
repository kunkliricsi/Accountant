using System.Linq;
using System.Collections.Generic;
using Accountant.Models;
using Accountant.Models.Enums;
using System;

namespace Accountant.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AccountantContext context)
        {
            InitializeUsers(context);
            InitializeCategories(context);
            InitializeReports(context);
            InitializeExpenses(context);
        }

        private static void InitializeUsers(AccountantContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User { Name = "Ricsi", Email = "kunkli.ricsi@gmail.com", Expenses = new List<Expense>() }
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }

        private static void InitializeCategories(AccountantContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category { Name = "Food", Description = "If someone orders food for others too.", Expenses = new List<Expense>() },
                new Category { Name = "Grocery", Expenses = new List<Expense>() },
                new Category { Name = "Electronic", Expenses = new List<Expense>() },
                new Category { Name = "Household", Expenses = new List<Expense>() },
                new Category { Name = "Internet", Description = "Bill for the service provider.", Expenses = new List<Expense>() },
                new Category { Name = "Vape", Description = "Buying vape stuff.", Expenses = new List<Expense>() }
            };

            foreach (var c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }

        private static void InitializeReports(AccountantContext context)
        {
            if (context.Reports.Any())
            {
                return;
            }

            var reports = new Report[]
            {
                new Report { Start = new DateTime(2018, 10, 11), End = new DateTime(2018, 11, 10), 
                    Evaluated = true, DateOfEvaluation = new DateTime(2018, 11, 10),
                    Expenses = new List<Expense>() }
            };

            foreach (var r in reports)
            {
                context.Reports.Add(r);
            }
            context.SaveChanges();
        }

        private static void InitializeExpenses(AccountantContext context)
        {
            if (context.Expenses.Any())
            {
                return;
            }

            var ricsi = context.Users.Find(1);
            var report = context.Reports.Find(1);
            var category = context.Categories.Find(1);

            var expenses = new Expense[]
            {
                new Expense { Purchaser = ricsi, Amount = 1539, Category = category,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 07), 
                    Report = report,
                    ItemsPurchased = new List<ShoppingListItem>() },
                    
                new Expense { Purchaser = ricsi, Amount = 4423, Category = category,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 15), 
                    Report = report,
                    ItemsPurchased = new List<ShoppingListItem>() },

                new Expense { Purchaser = ricsi, Amount = 4400, Category = category,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 01), 
                    Report = report,
                    ItemsPurchased = new List<ShoppingListItem>() },  

                new Expense { Amount = 2723, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 04),
                    Purchaser = ricsi, 
                    Report = report,
                    Category = category,
                    ItemsPurchased = new List<ShoppingListItem>() }
            };

            foreach (var e in expenses)
            {
                context.Expenses.Add(e);
                report.Expenses.Add(e);
                category.Expenses.Add(e);
                ricsi.Expenses.Add(e);
            }
            context.Reports.Update(report);
            context.Categories.Update(category);
            context.Users.Update(ricsi);
            context.SaveChanges();
        }
    }
}