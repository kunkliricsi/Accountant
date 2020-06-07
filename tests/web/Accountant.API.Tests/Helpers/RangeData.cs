using System.Collections.Generic;
using System.Linq;

namespace Accountant.API.Tests.Helpers
{
    public class RangeData
    {
        public static IEnumerable<object[]> Data => Enumerable.Range(1, 10).Select(i => new object[] { i });
    }
}
