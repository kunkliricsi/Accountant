using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.APP.Models.Web.Helpers
{

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.15.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class AddExpenseModel
    {
        [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int Amount { get; set; }

        [Newtonsoft.Json.JsonProperty("purchaseDate", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTimeOffset PurchaseDate { get; set; }

        [Newtonsoft.Json.JsonProperty("reportId", Required = Newtonsoft.Json.Required.Always)]
        public int ReportId { get; set; }

        [Newtonsoft.Json.JsonProperty("categoryId", Required = Newtonsoft.Json.Required.Always)]
        public int CategoryId { get; set; }

        [Newtonsoft.Json.JsonProperty("userId", Required = Newtonsoft.Json.Required.Always)]
        public int UserId { get; set; }


    }
}
