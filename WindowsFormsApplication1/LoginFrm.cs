using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
namespace WindowsFormsApplication1
{
    public partial class LoginFrm : Form
    {
        String userName = String.Empty;
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            // TO DO :Obfusticate the password
            var email = emailTb.Text.Trim().ToLower();
            var password = passwordTb.Text.Trim();
            if (!verfifyAccount(email, password))
            {
                MessageBox.Show("Incorrect email or password. Please try again!");
                emailTb.Text = "";
                passwordTb.Text = "";
            }
            else
            {
                this.Hide();
                MainFrm mainForm = new MainFrm(userName);
                mainForm.Show();
            }
        }

        /**
         * Check if there is any user with the same email. 
         */ 
        private bool verfifyAccount(String email, String password)
        {
            Dictionary<string, User> users = getUserList();

            foreach(String s in users.Keys)
            {
                User user = users[s];
                if (user.email.ToLower().Equals(email))
                {
                    GlobalValues.userID = s;
                    userName = user.name;
                    GlobalValues.user = user;
                    getUserContacts();
                    return true;
                }
            }
            return false    ;
        }

        /**
         * Getting all users from the database, might be inefficient but works for now
         */ 
        private Dictionary<string,User> getUserList()
        {
            FirebaseRequest emailRequest = new FirebaseRequest(GlobalValues.FBRTDBURI + "/" + Table.users.ToString()
                                                                + ".json" + "?auth=" + GlobalValues.dbSecret, httpMethod.GET);
            emailRequest.makeRequest();
            String res = emailRequest.executeGetRequest();
            Dictionary<string, User> jsonTemp = JsonConvert.DeserializeObject<Dictionary<string, User>>(res);
            return jsonTemp;
        }


        private void getUserContacts()
        {
            FirebaseRequest contactsRequest = new FirebaseRequest(GlobalValues.FBRTDBURI + "/" + Table.users.ToString() 
                                                    + "/"+GlobalValues.userID+"/contacts.json" + "?auth=" + GlobalValues.dbSecret, httpMethod.GET);
            contactsRequest.makeRequest();
            String res = contactsRequest.executeGetRequest();
            Console.WriteLine(res);
            if (res != null && res != "")
            {
                Dictionary<String, Contact> jsonTemp = JsonConvert.DeserializeObject<Dictionary<String, Contact>>(res);
                GlobalValues.contacts = jsonTemp;
            }
        }
        private void LoginFrm_Load(object sender, EventArgs e)
        {

        }
    }
}
