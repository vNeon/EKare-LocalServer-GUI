using System;
using Newtonsoft.Json;
namespace WindowsFormsApplication1
{
    /// <summary>
    ///  This class represents a message object
    ///  This object is serialized to a Json document and posted to Firebase database
    /// </summary>
    class Message
    {
        [JsonProperty("sender")]
        public string sender { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("time")]
        public string time { get; set; }
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("imageURL")]
        public string imageURL { get; set; }
        [JsonProperty("senderSeen")]
        public Boolean senderSeen { get; set; }
        [JsonProperty("recieverSeen")]
        public Boolean recieverSeen { get; set; }

        public Message(string message, string sender, string imageURL)
        {
            this.message = message;
            this.time = DateTime.Now.ToString("hh:mm tt");
            this.date = DateTime.Now.ToString("MMM dd");
            this.sender = sender;
            this.imageURL = imageURL;
            this.senderSeen = false;
            this.recieverSeen = false;
        }

    }
}
