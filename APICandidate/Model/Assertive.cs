using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APICandidate.Model
{
    public class Assertive
    {
        
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }

        //[ForeignKey("Question")]
        [JsonIgnore]
        public int QuestionID { get; set; }        
        //public Question Question { get; set; }
    }
}
