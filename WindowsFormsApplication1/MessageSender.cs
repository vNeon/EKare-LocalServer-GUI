using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;

namespace WindowsFormsApplication1
{
    class MessageSender
    {
        public async void sendMessageToAllContact(String message)
        {
            // Get any Stream -it can be FileStream, MemoryStream or any other type of Stream
            var stream = File.Open(@"C:\Users\johnn\Documents\Visual Studio 2015\Projects\WFA-SendMessageToApp\WindowsFormsApplication1\testImage.png", FileMode.Open);

            string clockID = DateTime.Now.ToString();
            long hashValue = clockID.GetHashCode();
            string fileName = GlobalValues.userID + hashValue.ToString();

            // Construct FirebaseStorage, path to where you want to upload the file and Put it there
            var task = new FirebaseStorage("myfirstapplication-5ad99.appspot.com")
                .Child("image")
                .Child(fileName + ".png")
                .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // await the task to wait until upload completes and get the download url
            var downloadUrl = await task;

            Message mes = new Message(message, GlobalValues.user.email,downloadUrl);
            foreach (String key in GlobalValues.contacts.Keys)
            {
                String URI = GlobalValues.FBRTDBURI + "/message-log/" + key + "/messages.json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest postMessage = new FirebaseRequest(URI, httpMethod.POST);
                postMessage.executePostRequest(mes);
            }

        }

    }
}
