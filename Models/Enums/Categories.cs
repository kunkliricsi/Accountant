using System.Runtime.CompilerServices;

namespace Accountant.Models.Enums
{
    public sealed class Categories : Base<Categories>
    {
        public static readonly Categories Food = new Categories();
        public static readonly Categories Groceries = new Categories();
        public static readonly Categories Electronics = new Categories();
        public static readonly Categories House = new Categories();

        private Categories([CallerMemberName] string name = null) : base(name) { }
    }
}