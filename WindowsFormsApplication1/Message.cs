using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace WindowsFormsApplication1
{
    class Message
    {
        [JsonProperty("sender")]
        public string sender { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("timeDate")]
        public string timeDate { get; set; }
        [JsonProperty("imageURL")]
        public string imageURL { get; set; }

        public Message(string message, string sender, string imageURL)
        {
            this.message = message;
            this.timeDate = DateTime.Now.ToString();
            this.sender = sender;
            this.imageURL = imageURL;
        }

    }
}
