using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;
namespace WindowsFormsApplication1
{
    /// <summary>
    /// This class construct and sending messages to the Firebase RT database when an accident occured
    /// </summary>
    class MessageSender
    {
        public async void sendMessageToAllContact(String message)
        {
            // Get any Stream -it can be FileStream, MemoryStream or any other type of Stream
            var stream = File.Open(@"C:\Users\johnn\Documents\Visual Studio 2015\Projects\WFA-SendMessageToApp\WindowsFormsApplication1\testImage.png", FileMode.Open);

            string clockID = DateTime.Now.ToString();
            long hashValue = clockID.GetHashCode();
            string fileName = GlobalValues.userID + hashValue.ToString();

            // Create a png file in 'image' folder of the firebase bucket
            var task = new FirebaseStorage(GlobalValues.firebaseStorageBucket)
                .Child("image")
                .Child(fileName + ".png")
                .PutAsync(stream);

            // REMOVE LATER
            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // await the task to wait until upload completes and get the download url
            var downloadUrl = await task;
            Message mes = new Message(message, GlobalValues.user.email, downloadUrl);

            string lastMessage = message;
            int maxMessageLen = 20; // the screen can display messages that are shorter than 20 characters
            if (lastMessage.Length > maxMessageLen)
            {
                lastMessage = lastMessage.Trim().Substring(0, lastMessage.Substring(0, 20).LastIndexOf(" "));
            }

            // construct the update message in Json format
            string updateInfo = "{\"lastmessage\":\""+lastMessage+"\",\"date\":\""+mes.date+"\"}";
            foreach (String key in GlobalValues.contacts.Keys)
            {
                //TODO HAVE TO CHANGE MESSAGE-LOG TO MESSAGE_LOG
                String URI = GlobalValues.FBRTDBURI + "/message-log/" + key + "/messages.json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest postMessage = new FirebaseRequest(URI, httpMethod.POST);
                postMessage.executePostRequest(mes);

                //Update the latest message and timestamp
                String URImessageLog = GlobalValues.FBRTDBURI + "/message-log/"+key+".json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest postMessageLog = new FirebaseRequest(URImessageLog, httpMethod.POST);

                postMessageLog.updateMesssageLog(updateInfo);

            }

        }

    }
}
