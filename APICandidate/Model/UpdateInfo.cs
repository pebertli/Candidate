using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICandidate.Model
{
    public class UpdateInfo
    {
        public int Id { get; set; }       
        public string Info { get; set; }        
        public DateTimeOffset? LastUpdate { get; set; }
       
    }
}
