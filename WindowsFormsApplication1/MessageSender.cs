using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;
using Newtonsoft.Json;
using System.Drawing;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// This class construct and sending messages to the Firebase RT database when an accident occured
    /// </summary>
    class MessageSender
    {


        public async void sendMessageToAllContact(String message, Bitmap imageAccident)
        {
            // Get any Stream -it can be FileStream, MemoryStream or any other type of Stream
            //var stream = File.Open(Directory.GetCurrentDirectory() + "\\Resources\\n.png", FileMode.Open);

            string clockID = DateTime.Now.ToString();
            long hashValue = clockID.GetHashCode();
            string fileName = GlobalValues.userID + hashValue.ToString() + ".png";
            using (MemoryStream ms = new MemoryStream())
            {
                imageAccident.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                var stream = File.Open( fileName, FileMode.Open);
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
            }

            Message messageToContacts = new Message(message, GlobalValues.user.email, fileName);

            string lastMessage = message;
            int maxMessageLen = 20; // the screen can display messages that are shorter than 20 characters
            if (lastMessage.Length > maxMessageLen)
            {
                lastMessage = lastMessage.Trim().Substring(0, lastMessage.Substring(0, 20).LastIndexOf(" "));
            }

            // Construct the update message in Json format
            string updateInfo = "{\"lastMessage\":\""+lastMessage+"\",\"date\":\""+ messageToContacts.date+"\",\"numNotifications\":\"";
            
            //Get the contact lists again before sending the notification
            getUserList();
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
                
                // Update the latest message and timestamp for the current user
                String URImessageLog = GlobalValues.FBRTDBURI + "/" + Table.users.ToString() + "/" +GlobalValues.userID+
                                       "/"+Table.contacts.ToString()+"/"+ key+".json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest updateMessageLogPostRequest = new FirebaseRequest(URImessageLog, httpMethod.POST);
                int numNotifications1 = GlobalValues.contacts[key].numNotifications + 1;
                string updateUserContactJson=updateInfo + numNotifications1.ToString()+"\"}";
                updateMessageLogPostRequest.updateNode(updateUserContactJson);

                // Update the lastest message +timestamp + notfications for users contacts
                String otherContactID = searchContact(GlobalValues.contacts[key].email.ToLower().Trim());
                String otherContactURI = GlobalValues.FBRTDBURI + "/" + Table.users.ToString() + "/" + otherContactID +
                                       "/" + Table.contacts.ToString() + "/" + key + ".json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest updateContactFieldPostRequest = new FirebaseRequest(otherContactURI, httpMethod.POST);
                int numNotifications2 = GlobalValues.userList[otherContactID].contacts[key].numNotifications + 1;
                string updateOtherContactJson = updateInfo + numNotifications2.ToString() + "\"}";
                updateContactFieldPostRequest.updateNode(updateOtherContactJson);
            }

        }

        /**
         * Getting all users from the database, might be inefficient but works for now
         */
        private void getUserList()
        {
            FirebaseRequest emailRequest = new FirebaseRequest(GlobalValues.FBRTDBURI + "/" + Table.users.ToString()
                                                                + ".json" + "?auth=" + GlobalValues.dbSecret, httpMethod.GET);
            emailRequest.makeRequest();
            String res = emailRequest.executeGetRequest();
            if (res != null && res != "")
            {
                GlobalValues.userList = JsonConvert.DeserializeObject<Dictionary<string, User>>(res);
            }
        }

        /**
         * Get user list of contacts
         */ 

        private void getContactList()
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

        /**
         * Find the current user in other user's contact
         */
        private String searchContact(String email)
        {
            String otherContactID = null;
            foreach( String userID in GlobalValues.userList.Keys)
            {
                if (GlobalValues.userList[userID].email.ToLower().Trim().Equals(email))
                {
                    otherContactID = userID;
                }
            }

            return otherContactID;
        }
    }
}
