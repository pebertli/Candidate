using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APICandidate.Model
{
    public class Question
    {

        public int Id { get; set; }        
        public string Title { get; set; }
        public string Info { get; set; }

        //[InverseProperty("Question")]
        public ICollection<Assertive> Assertives { get; set; }        
    }
}
