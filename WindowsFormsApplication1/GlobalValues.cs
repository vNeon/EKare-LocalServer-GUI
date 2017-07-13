using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{

    public enum Table
    {
        users,
        message_log
    }

    class GlobalValues
    {
        public static string userID;
        public const string applicationID = "AAAAxHOyOu8:APA91bFN66xR3tpsOQC1OywO6s1Wv_8aq5iXNZdDnp1aog9LVjoySszWuHtvjZaEch0rQr5o3T3HoXgsKjbbZijFIJqy8rQVunQEAopAcbfP4dAAEgUKbb7woALRahEU7398wLwembnk";
        public const string FBRTDBURI = "https://myfirstapplication-5ad99.firebaseio.com/";
        public const string dbSecret = "rngcgjOb25J68o1JW5XUEFigUbO86kNQmKxN4IB5";

    }
}
