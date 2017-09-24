using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Representing a user with basic informations
    /// </summary>
    class User
    {
        [JsonProperty("email")]
        public string email { get; set; }
        
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }

        [JsonProperty("role")]
        public string role { get; set; }

        [JsonProperty("contacts")]
        public Dictionary<String, Contact> contacts { get; set; }

        public User()
        {
        }


    }


}
