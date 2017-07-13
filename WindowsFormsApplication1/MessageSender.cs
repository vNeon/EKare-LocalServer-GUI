using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class MessageSender
    {

        public void sendMessageToAllContact(String message)
        {
            Message mes = new Message(message, GlobalValues.user.email);

            foreach(String key in GlobalValues.contacts.Keys)
            {
                String URI = GlobalValues.FBRTDBURI + "/message-log/"+key+"/messages.json?auth=" + GlobalValues.dbSecret;
                FirebaseRequest postMessage = new FirebaseRequest(URI, httpMethod.POST);
                postMessage.executePostRequest(mes);
            }

        }
    }
}
