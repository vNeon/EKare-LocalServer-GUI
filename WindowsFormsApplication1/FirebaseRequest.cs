using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;


namespace WindowsFormsApplication1
{
    /*
     * Http request methods 
     */
    public enum httpMethod
    {
        GET, 
        POST, 
        PUT, 
        DELETE
    }


    /*
     * This class create and execute the requests to the realtime Firebase database. 
     * The responses are handdled here and return the the main fall message sender class.
     * 
     */ 
    class FirebaseRequest
    {
        private String endPoint { get; set; }
        private httpMethod httpMethod { get; set; }
        private HttpWebRequest request = null;
        private static readonly HttpClient client = new HttpClient();

        public FirebaseRequest(String URI , httpMethod method)
        {
            this.endPoint = URI;
            this.httpMethod = method;
        }

        public void makeRequest()
        {
            this.request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();
        }

        public String executeGetRequest()
        {
            String responseString = String.Empty;
           try  
            {
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Request Failed. Error Code " + res.StatusCode.ToString());
                }else
                {
                    //get the response stream for the response
                    using (Stream resStream = res.GetResponseStream())
                    {
                        if(resStream == null)
                        {
                            throw new Exception("Failed to get a response stream");
                        }else
                        {
                            using (StreamReader reader = new StreamReader(resStream)) {
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


        public async void executePostRequest(Message message)
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(message);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endPoint, content);
            var responseString = await response.Content.ReadAsStringAsync();
        }



    }
}
