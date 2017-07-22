using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace WindowsFormsApplication1
{
    class Contact
    {
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("imagePermission")]
        public bool imagePermission { get; set; }
        [JsonProperty("messagePermission")]
        public bool messagePermission { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }

        public Contact(string email, bool imagePermission, bool messagePermission, string name)
        {
            this.email = email;
            this.imagePermission = imagePermission;
            this.messagePermission = messagePermission;
            this.name = name;
        }
    }
}
