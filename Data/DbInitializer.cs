using System.Linq;
using Accountant.Models;

namespace Accountant.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AccountantContext context)
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
                new Category { Name = "Household" }
            };

            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }
    }
}