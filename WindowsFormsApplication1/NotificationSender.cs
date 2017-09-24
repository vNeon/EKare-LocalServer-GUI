using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// This class send realtime notifications to the android app
    /// </summary>
    class NotificationSender
    {
        //Send notification to a topic 
        public string SendNotification()
        {
            //String message = textBox1.Text;
            String message = "Fall detected on Monday, May 15, 2017 1:45 PM";//+ DateTime.Now;
            String str;
            try
            { 

                string senderId = "843754650351";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                string s6 = "cI5LMmD20So:APA91bFVkSZLrqYDiicul7_VPPeNHQZ-cl4DlbIWbpzwq9_fZ-8l3OOWDjgFMfR1R0os1NgMnZizEdMczAUmZgiqCHqTgG_aSxHAG8Izv0ybcgQqa2SiHY6L39gPTxJV2zNIYIT2Pj5l";
                string em = "fmdMKGiiYWU:APA91bE5HG7c_QtSx88DmC8g-KcKZgQO77N-xvzOq3MmkpZoUUnH_5k9OVEMl808OYlDl8BCbBr2PVun7FMYe-Zr6ZNTWNJvFsKckZOHLSMsFRbgZJdRekvjt0MYlMGkuBdNwwTo5CyW";

                 var notification = new
                {
                    to = s6,
                    data = new
                    {
                        message = message,
                        title = "Message:",
                        img_url = "https://firebasestorage.googleapis.com/v0/b/myfirstapplication-5ad99.appspot.com/o/image%2F5s9kRal7NpU2taXF3TeQRplVtPC3-248524188.png?alt=media&token=77a54874-93c7-4042-9f21-d5d26c75408d"
                        
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(notification);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GlobalValues.applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                //
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                str = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                str = ex.Message;
            }

            return str;
        }

        public string SendNotification(string message, string imgName)
        {
            String str = "no contacts";

            getContactList();
            foreach (String key in GlobalValues.contacts.Keys)
            {
                //String message = textBox1.Text;
                DateTime thisDay = DateTime.Today;
                //Console.WriteLine(thisDay.ToString("D"));
                message = "Fall detected on " + thisDay.ToString("D");//+ DateTime.Now;

                try
                {

                    string senderId = "843754650351";
                    WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = "application/json";
                    var notification = new
                    {
                        to = GlobalValues.contacts[key].deviceToken,
                        data = new
                        {
                            message = message,
                            title = "Fall Detected!",
                            img_name = imgName
                        }
                    };
                    var serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(notification);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", GlobalValues.applicationID));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                    tRequest.ContentLength = byteArray.Length;

                    //
                    using (Stream dataStream = tRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse tResponse = tRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    str = sResponseFromServer;
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    str = ex.Message;
                }
            }
            return str;
        }


        public void getContactList()
        {
            FirebaseRequest contactsRequest = new FirebaseRequest(GlobalValues.FBRTDBURI + "/" + Table.users.ToString()
                                                                    + "/" + GlobalValues.userID + "/contacts.json" + "?auth=" + GlobalValues.dbSecret, httpMethod.GET);
            contactsRequest.makeRequest();
            String res = contactsRequest.executeGetRequest();
            //Console.WriteLine(res);
            if (res != null && res != "")
            {
                Dictionary<String, Contact> jsonTemp = JsonConvert.DeserializeObject<Dictionary<String, Contact>>(res);
                GlobalValues.contacts = jsonTemp;
            }
        }

    }
}
