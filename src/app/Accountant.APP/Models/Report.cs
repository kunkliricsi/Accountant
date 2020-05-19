using System;
using System.Collections.Generic;
using System.Text;

namespace Accountant.APP.Models
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.15.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Report
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("startDate", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTimeOffset StartDate { get; set; }

        [Newtonsoft.Json.JsonProperty("endDate", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public System.DateTimeOffset EndDate { get; set; }

        [Newtonsoft.Json.JsonProperty("isEvaluated", Required = Newtonsoft.Json.Required.Always)]
        public bool IsEvaluated { get; set; }

        [Newtonsoft.Json.JsonProperty("evaluationDate", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset? EvaluationDate { get; set; }

        [Newtonsoft.Json.JsonProperty("group", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public Group Group { get; set; } = new Group();

        [Newtonsoft.Json.JsonProperty("expenses", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<Expense> Expenses { get; set; }


    }
}
