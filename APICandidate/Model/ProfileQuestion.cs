using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APICandidate.Model
{
    public class ProfileQuestion
    {

        public int Id { get; set; }        
        public int Choice { get; set; }
        public string Opinion { get; set; }

        [JsonIgnore]
        public int ProfileId { get; set; }
        [JsonIgnore]
        public int QuestionId { get; set; }

        //[InverseProperty("Question")]
        public Profile Profile { get; set; }
        public Question Question { get; set; }
    }
}
