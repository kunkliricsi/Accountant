using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.APP.Models.Helpers
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.15.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class UpdateModel
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("email", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty("password", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Password { get; set; }


    }
}
