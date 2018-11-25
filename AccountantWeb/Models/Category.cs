using System.ComponentModel.DataAnnotations;
using System;

namespace Accountant.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime lastModified { get; set; }
    }
}