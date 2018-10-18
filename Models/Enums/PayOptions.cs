using System.Runtime.CompilerServices;

namespace Accountant.Models.Enums
{
    public sealed class PayOptions : Base<PayOptions>
    {
        public static readonly PayOptions Credit = new PayOptions();
        public static readonly PayOptions Cash = new PayOptions();

        private PayOptions([CallerMemberName] string name = null) : base(name) { }
    }
}