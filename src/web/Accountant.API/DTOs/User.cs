using System.Collections.Generic;

namespace Accountant.API.DTOs
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
