using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICandidate.Model
{
    public class Profile
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public string Bio { get; set; }
        [JsonIgnore]
        public DateTimeOffset? Deleted { get; set; }
        [JsonIgnore]
        public DateTimeOffset? LastUpdate { get; set; }
    }
}
