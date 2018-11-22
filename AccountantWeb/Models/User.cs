using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Accountant.Models
{
    public class User
    {
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}