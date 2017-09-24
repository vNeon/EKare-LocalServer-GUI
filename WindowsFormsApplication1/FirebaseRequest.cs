using System;
using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Enumeration of the http methods
    /// </summary>
    public enum httpMethod
    {
        GET, 
        POST, 
        PUT, 
        DELETE,
        PATCH // For updating partial database nodes
    }


    /*
     * This class create and execute the requests to the realtime Firebase database. 
     * The responses are handdled here and return the the main fall message sender class.
     * 
     */ 

    /// <summary>
    /// This class is used to interact with the Firebase RT database and storgage
    /// Also encapsulates the Rest API usage and Http requests.
    /// </summary>
    class FirebaseRequest
    {
        // URI
        private String endPoint { get; set; }
        private httpMethod httpMethod { get; set; }
        private HttpWebRequest request = null;
        // This HttpClient is shared and used for asynchronous operations.
        private static readonly HttpClient client = new HttpClient(); 

        public FirebaseRequest(String URI , httpMethod method)
        {
            this.endPoint = URI;
            this.httpMethod = method;
        }

        /// <summary>
        /// This method initializes the request and the method for the HttpWebRequest
        /// </summary>
        public void makeRequest()
        {
            this.request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();
        }

        /// <summary>
        /// This method execute get requests and return a string in JSON format
        /// </summary>
        /// <returns>String result</returns>
        public String executeGetRequest()
        {
            String responseString = String.Empty;
            try
            {
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Request Failed. Error Code " + res.StatusCode.ToString());
                }
                else
                {
                    // get the response stream for the response
                    using (Stream resStream = res.GetResponseStream())
                    {
                        if (resStream == null)
                        {
                            throw new Exception("Failed to get a response stream");
                        }
                        else
                        {
                            using (StreamReader reader = new StreamReader(resStream))
                            {
                                responseString = reader.ReadToEnd();
                            }
                        }
                    }
                }

            }
            catch (WebException we)
            {
                Console.WriteLine(we.Message);
            }
            return responseString;
        }


        /// <summary>
        /// This asynchronous method serializes an object to JSON doc and create a node for it in Firebase RT database
        /// </summary>
        /// <param name="obj"></param>
        public async void executePostRequest(Object obj)
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(obj);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endPoint, content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// This asynchronous method recieves an string of the lastmessage and the corresponding timestamp
        /// and update the node in Firebase RT database specified by the endpoint
        /// </summary>
        /// <param name="updateMessage"></param>
        public async void updateNode(String updateMessage)
        {

            //Update request 
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), endPoint)
            {
                Content = new StringContent(updateMessage, Encoding.UTF8, "application/json")
            };

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                // POTENTIALLY WRITE TO A LOG FILE
                Console.WriteLine(ex.Message);
            }
        }

    }
}
