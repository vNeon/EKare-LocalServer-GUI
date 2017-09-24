using Newtonsoft.Json;
namespace WindowsFormsApplication1
{
    /**
     * Class Contact:
     * It is the Data Transmittion object for contacts
     * This object is serialized to a Json document and posted to Firebase database
     */
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
        [JsonProperty("numNotifications")]
        public int numNotifications {get;set;}

        public Contact(string email, bool imagePermission, bool messagePermission, string name, string date, string lastMessage, string deviceToken, int numNotifcations)
        {
            this.email = email;
            this.imagePermission = imagePermission;
            this.messagePermission = messagePermission;
            this.name = name;
            this.date = date;
            this.lastMessage = lastMessage;
            this.deviceToken = deviceToken;
            this.numNotifications = numNotifications;
        }

    }
}
