using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    class User
    {
        [JsonProperty("email")]
        public string email { get; set; }
        
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }

        //[JsonProperty("contacts")]
        //private List<KeyValuePair<string,string>> contacts { get; set; }
    }

}
