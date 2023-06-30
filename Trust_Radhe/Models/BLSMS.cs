using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace BusinessLayer
{
    static public class BLSMS
    {
        public static string SendSMS2(string User, string password, string senderid, string Mobile_Number, string Message)
        {
            // use the API URL here  
            string strUrl = "http://wiztechsms.com/http-api.php?username=" + User + "&password=" + password + "&senderid=" + senderid + "&route=2&number=" + Mobile_Number + "&message=" + Message;        // Create a request object  
            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }
        public static string SendSMSNew(string Mobile_Number, string Message)
        {
            // use the API URL here  
            //string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=dharaworld&password=123456&sender=DHARAW&to=8052949381&message=" + 
            //    User + "&password=" + password + "&senderid=" + senderid + "&route=2&number=" + Mobile_Number + "&message=" + Message;        // Create a request object  

            string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=dreamuser&password=jUED8rm9rTJMBPE&sender=DRMCRS&to=" + Mobile_Number + "&message=" + Message + "& reqid = 1 & format ={ json}&route_id = 39 & callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";

            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }
        static public string ForgetPassword(string MemberName, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["ForgetPassword"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }
        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }
        static public string OTP(string MemberName, string OTP)
        {
            string Message = ConfigurationSettings.AppSettings["OTP"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[OTP]", OTP);

            return Message;
        }


    }
}
