using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;
using Newtonsoft.Json;

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
            string fileName = GlobalValues.userID + hashValue.ToString()+".png";

            // Create a png file in 'image' folder of the firebase bucket
            var task = new FirebaseStorage(GlobalValues.firebaseStorageBucket)
                .Child("image")
                .Child(fileName)
                .PutAsync(stream);

            // REMOVE LATER
            //// Track progress of the upload
            //task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // Await the task to wait until upload completes and get the download url
            //var downloadUrl = await task;
            Message messageToContacts = new Message(message, GlobalValues.user.name, fileName);

            string lastMessage = message;
            int maxMessageLen = 20; // the screen can display messages that are shorter than 20 characters
            if (lastMessage.Length > maxMessageLen)
            {
                lastMessage = lastMessage.Trim().Substring(0, lastMessage.Substring(0, 20).LastIndexOf(" "));
            }

            // Construct the update message in Json format
            string updateInfo = "{\"lastMessage\":\""+lastMessage+"\",\"date\":\""+ messageToContacts.date+"\"}";
            getContactList();
            foreach (String key in GlobalValues.contacts.Keys)
            {
                messageToContacts.imageURL = fileName;
                // URL of the message log and the Firebase request 
                String URI = GlobalValues.FBRTDBURI + "/"+Table.message_log.ToString()+"/" + key + "/messages.json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest postMessage = new FirebaseRequest(URI, httpMethod.POST);

                // Initialize message object depend of the contact's permissions.
                if (!GlobalValues.contacts[key].imagePermission)
                {
                    messageToContacts.imageURL = String.Empty;
                }

                postMessage.executePostRequest(messageToContacts);
                
                //Update the latest message and timestamp
                String URImessageLog = GlobalValues.FBRTDBURI + "/" + Table.users.ToString() + "/" +GlobalValues.userID+
                                       "/"+Table.contacts.ToString()+"/"+ key+".json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest postMessageLog = new FirebaseRequest(URImessageLog, httpMethod.POST);

                postMessageLog.updateMesssageLog(updateInfo);

            }

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
