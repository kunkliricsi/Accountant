using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.APP.Models
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.15.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Expense
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int Amount { get; set; }

        [Newtonsoft.Json.JsonProperty("purchaseDate", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTimeOffset PurchaseDate { get; set; }

        [Newtonsoft.Json.JsonProperty("report", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public Report Report { get; set; } = new Report();

        [Newtonsoft.Json.JsonProperty("category", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Category Category { get; set; }

        [Newtonsoft.Json.JsonProperty("user", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public User User { get; set; } = new User();


    }
}
