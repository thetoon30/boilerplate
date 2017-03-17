using angular2inter.Core.Model.ParseObject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.Core.Helper
{
    //NOTE : refer to http://stackoverflow.com/questions/17079917/how-do-i-send-api-push-message-with-net-parse-com-c
    public static class PushParse
    {
        const string ApplicationId = "Ymm7qtOplpXL1QyQ5cU6okwn0y5BVYV56j7Ue3ES";
        const string RestAPIKey = "DRTOXhHnJ0LI57XOzGLZK8Gm2v8AJxdX0nUUv9gT";
        const string MasterKey = "MQh3hoRJ79yyOdIjlh30SUrp7bTvXH0bJTCQ3Pig";

        public static InstallationCollection GetInstallation()
        {
            string urlpath = "https://api.parse.com/1/installations";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);

            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-Master-Key", MasterKey);
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<InstallationCollection>(responseText);
            }
        }

        public static InstallationObject GetInstallation(string id)
        {
            string urlpath = "https://api.parse.com/1/installations/" + id;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);

            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<InstallationObject>(responseText);
            }
        }

        public static bool CreateInstallation(string deviceType, string deviceToken, string gcmSenderId, string channel)
        {
            bool isPushMessageSend = false;

            string postString = "";
            string urlpath = "https://api.parse.com/1/installations";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);
            if (deviceType == "ios")
                postString = "{\"deviceType\": \"" + deviceType + "\", \"deviceToken\" : \"" + deviceToken + "\", \"channel\" : [\"" + channel + "\"]";
            else if (deviceType == "android")
                postString = "{\"deviceType\": \"" + deviceType + "\",\"pushType\" : \"gcm\",\"deviceToken\" : \"" + deviceToken + "\", \"GCMSenderId\" : \"" + gcmSenderId + "\"channel\" : [\"" + channel + "\"]";

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = postString.Length;
            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "POST";
            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject jObjRes = JObject.Parse(responseText);
                if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                {
                    isPushMessageSend = true;
                }
            }

            return isPushMessageSend;
        }

        public static bool UpdateInstallation(string id, string deviceType, string deviceToken, string gcmSenderid, string channel)
        {
            bool isPushMessageSend = false;

            string postString = "";
            string urlpath = "https://api.parse.com/1/installations/" + id;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);
            if (deviceType == "ios")
                postString = "{\"deviceType\": \"" + deviceType + "\", \"deviceToken\" : \"" + deviceToken + "\", \"channel\" : [\"\"]";
            else if (deviceType == "android")
                postString = "{\"deviceType\": \"" + deviceType + "\",\"pushType\" : \"gcm\",\"deviceToken\" : \"" + deviceToken + "\", \"GCMSenderId\" : \"" + gcmSenderid + "\"channel\" : [\"\"]";

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = postString.Length;
            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "POST";
            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject jObjRes = JObject.Parse(responseText);
                if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                {
                    isPushMessageSend = true;
                }
            }

            return isPushMessageSend;
        }

        public static bool DeleteInstallation(string id)
        {
            string urlpath = "https://api.parse.com/1/installations/" + id;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);

            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-Master-Key", MasterKey);
            httpWebRequest.Method = "DELETE";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return true;
            }
        }

        public static UserObjectResponse GetUser()
        {
            string urlpath = "https://api.parse.com/1/users";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);

            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserObjectResponse>(responseText);
            }
        }

        public static UserObject GetUser(string id)
        {
            string urlpath = "https://api.parse.com/1/users/" + id;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);

            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserObject>(responseText);
            }
        }

        public static bool PushNotification(string pushMessage)
        {
            bool isPushMessageSend = false;

            string postString = "";
            string urlpath = "https://api.parse.com/1/push";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);
            postString = "{ \"channels\": [ \"\"  ], " +
                             "\"data\" : {\"alert\":\"" + pushMessage + "\"}" +
                             "}";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = postString.Length;
            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "POST";
            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject jObjRes = JObject.Parse(responseText);
                if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                {
                    isPushMessageSend = true;
                }
            }

            return isPushMessageSend;
        }

        public static bool PushNotification(string pushMessage, string objectName, string objectValue)
        {
            bool isPushMessageSend = false;

            string postString = "";
            string urlpath = "https://api.parse.com/1/push";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);
            postString = "{\"where\": {\"" + objectName + "\": \"" + objectValue + "\"},\"data\": {\"alert\": \"" + pushMessage + "\"}}";

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = postString.Length;
            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "POST";
            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject jObjRes = JObject.Parse(responseText);
                if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                {
                    isPushMessageSend = true;
                }
            }

            return isPushMessageSend;
        }

        public static bool PushNotification(string pushMessage, string channel)
        {
            bool isPushMessageSend = false;

            string postString = "";
            string urlpath = "https://api.parse.com/1/push";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);
            postString = "{ \"channels\": [ \"" + channel + "\"  ], " +
                             "\"data\" : {\"alert\":\"" + pushMessage + "\"}" +
                             "}";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = postString.Length;
            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "POST";
            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject jObjRes = JObject.Parse(responseText);
                if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                {
                    isPushMessageSend = true;
                }
            }

            return isPushMessageSend;
        }

        public static bool PushNotification(string pushMessage, string deviceType, string channel, DateTime pushScheduleDate)
        {
            bool isPushMessageSend = false;

            string postString = "";
            string urlpath = "https://api.parse.com/1/push";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);
            postString = "{\"where\": {\"deviceType\": \"" + deviceType + "\"},\"data\": {\"alert\": \"" + pushMessage + "\"}}";

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = postString.Length;
            httpWebRequest.Headers.Add("X-Parse-Application-Id", ApplicationId);
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", RestAPIKey);
            httpWebRequest.Method = "POST";
            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject jObjRes = JObject.Parse(responseText);
                if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                {
                    isPushMessageSend = true;
                }
            }

            return isPushMessageSend;
        }
    }
}
