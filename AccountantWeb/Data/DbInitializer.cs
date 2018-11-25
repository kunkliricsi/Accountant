using System.Linq;
using Accountant.Models;
using Accountant.Models.Enums;
using System;

namespace Accountant.Data
{
    public static class DbInitializer
    {
        static DateTime now;

        public static void Initialize(AccountantContext context)
        {
            now = DateTime.UtcNow;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Kind);

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
                Category = now,
                Expense = now,
                Report = now,
                ShoppingListItem = now,
                User = now,
                lastModified = now,
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
                new User { Name = "Ricsi", Email = "kunkli.ricsi@gmail.com", lastModified = now }
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
                new Category { Name = "Food", Description = "If someone orders food for others too.", lastModified = now},
                new Category { Name = "Grocery", lastModified = now },
                new Category { Name = "Electronic", lastModified = now },
                new Category { Name = "Household", lastModified = now },
                new Category { Name = "Internet", Description = "Bill for the service provider.", lastModified = now },
                new Category { Name = "Vape", Description = "Buying vape stuff.", lastModified = now }
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
                    Evaluated = true, DateOfEvaluation = new DateTime(2018, 11, 10), lastModified = now }
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
                    PayOption = PayOption.Cash, DateOfPurchase = new DateTime(2018, 11, 07), 
                    ReportID = report.ID, lastModified = now },
                    
                new Expense {  PurchaserID = ricsi.ID, Amount = 4423, CategoryID = category.ID,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 15), 
                    ReportID = report.ID, lastModified = now },

                new Expense {  PurchaserID = ricsi.ID, Amount = 4400, CategoryID = category.ID,
                    PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 01), 
                    ReportID = report.ID, lastModified = now },  

                new Expense { Amount = 2723, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 04),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID, lastModified = now },

                new Expense { Amount = 7835, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 11, 02),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID, lastModified = now },

                new Expense { Amount = 2508, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 17, 16, 05, 00),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID, lastModified = now },

                new Expense { Amount = 1385, PayOption = PayOption.Cash, DateOfPurchase = new DateTime(2018, 10, 28, 10, 55, 53),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID, lastModified = now },

                new Expense { Amount = 3260, PayOption = PayOption.Credit, DateOfPurchase = new DateTime(2018, 10, 28, 18, 26, 13),
                     PurchaserID = ricsi.ID,
                    ReportID = report.ID,
                    CategoryID = category.ID, lastModified = now }
            };

            foreach (var e in expenses)
            {
                context.Expenses.Add(e);
            }
            context.SaveChanges();
        }
    }
}