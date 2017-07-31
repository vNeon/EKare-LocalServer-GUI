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
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("lastMessage")]
        public string lastMessage { get; set; }
        [JsonProperty("deviceToken")]
        public string deviceToken { get; set; }

        public Contact(string email, bool imagePermission, bool messagePermission, string name, string date, string lastMessage, string deviceToken)
        {
            this.email = email;
            this.imagePermission = imagePermission;
            this.messagePermission = messagePermission;
            this.name = name;
            this.date = date;
            this.lastMessage = lastMessage;
            this.deviceToken = deviceToken;
        }

    }
}
