using System.IO;
using System.Threading.Tasks;

namespace Accountant.APP.Extensions
{
    public static class StreamExtension
    {
        public static async Task<string> ReadAsStringAsync(this Stream stream)
        {
            string result;
            using (var sr = new StreamReader(stream))
            {
                result = await sr.ReadToEndAsync();
            }

            return result;
        }
    }
}
