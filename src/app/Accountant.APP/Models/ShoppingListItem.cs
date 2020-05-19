using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.APP.Models
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.15.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class ShoppingListItem
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("isTicked", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? IsTicked { get; set; }

        [Newtonsoft.Json.JsonProperty("shoppingList", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ShoppingList ShoppingList { get; set; }


    }
}
