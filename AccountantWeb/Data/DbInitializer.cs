using System.Linq;
using Accountant.Models;
using Accountant.Models.Enums;
using System;

namespace Accountant.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AccountantContext context)
        {
            InitializeChanges(context);
            InitializeUsers(context);
            InitializeCategories(context);
            InitializeReports(context);
            InitializeExpenses(context);
        }

        private static void InitializeChanges(AccountantContext context)
        {
            var change = new Changes()
            {
                Category = DateTime.UtcNow,
                Expense = DateTime.UtcNow,
                Report = DateTime.UtcNow,
                ShoppingListItem = DateTime.UtcNow,
                User = DateTime.UtcNow
            };

            if (context.Changes.Any())
            {
                context.Changes.Update(change);
            }
            else
            {
                context.Changes.Add(change);
            }
            context.SaveChanges();
        }

        private static void InitializeUsers(AccountantContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User { Name = "Ricsi", Email = "kunkli.ricsi@gmail.com" }
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
                new Category { Name = "Food", Description = "If someone orders food for others too." },
                new Category { Name = "Grocery" },
                new Category { Name = "Electronic" },
                new Category { Name = "Household" },
                new Category { Name = "Internet", Description = "Bill for the service provider." },
                new Category { Name = "Vape", Description = "Buying vape stuff." }
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
                    Evaluated = true, DateOfEvaluation = new DateTime(2018, 11, 10) }
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
                new Expense {  PurchaserID = ricsi.ID, Amount = 1539, CategoryID = category.ID,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 07), 
                    ReportID = report.ID },
                    
                new Expense {  PurchaserID = ricsi.ID, Amount = 4423, CategoryID = category.ID,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 15), 
                    ReportID = report.ID },

                new Expense {  PurchaserID = ricsi.ID, Amount = 4400, CategoryID = category.ID,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 01), 
                    ReportID = report.ID },  

                new Expense { Amount = 2723, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 04),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID },

                new Expense { Amount = 7835, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 02),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID },

                new Expense { Amount = 2508, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 17),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID },

                new Expense { Amount = 1385, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 28),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID },

                new Expense { Amount = 3260, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 28),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID }
            };

            foreach (var e in expenses)
            {
                context.Expenses.Add(e);
            }
            context.SaveChanges();
        }
    }
}