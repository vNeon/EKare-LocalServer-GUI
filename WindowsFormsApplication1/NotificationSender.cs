using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
namespace WindowsFormsApplication1
{
    /// <summary>
    /// This class send realtime notifications to the android app
    /// </summary>
    class NotificationSender
    {
        //Send notification to a topic 
        public void SendNotification()
        {
            //String message = textBox1.Text;
            String message = "Fall detected on Monday, May 15, 2017 1:45 PM";//+ DateTime.Now;
            String str;
            try
            { 

                string senderId = "843754650351";

                //string deviceId = "ch_G60NPga4:APA9............T_LH8up40Ghi-J";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = "/topics/all",
                    notification = new
                    {
                        body = message,
                        title = "Message:",
                        sound = "Enabled"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
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

    }
}
