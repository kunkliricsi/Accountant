using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.APP.Models.Web.Helpers
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.15.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class UserGroup
    {
        [Newtonsoft.Json.JsonProperty("userId", Required = Newtonsoft.Json.Required.Always)]
        public int UserId { get; set; }

        [Newtonsoft.Json.JsonProperty("groupId", Required = Newtonsoft.Json.Required.Always)]
        public int GroupId { get; set; }


    }
}
