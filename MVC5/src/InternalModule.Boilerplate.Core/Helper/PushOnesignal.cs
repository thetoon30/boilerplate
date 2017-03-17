using InternalModule.Boilerplate.Core.Model.Onesignal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public static class PushOnesignal
    {
        const string ApplicationId = "305537d5-b1cc-493b-8335-46428b6a44d9";
        const string RestAPIKey = "ZjRmZjBiODQtMDgwZC00MTA4LWE0OGQtYmRhOWJmNDQ3N2Qz";

        public static PlayerObjectResponse GetPlayer()
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/players?app_id=" + ApplicationId) as HttpWebRequest;

            request.Method = "GET";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + RestAPIKey);

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<PlayerObjectResponse>(responseText);
            }
        }

        public static PlayerObject GetPlayer(string id)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/players/" + id) as HttpWebRequest;

            request.Method = "GET";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + RestAPIKey);

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<PlayerObject>(responseText);
            }
        }

        public static bool PushNotification(string pushMessage)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + RestAPIKey);

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"" + ApplicationId + "\","
                                                    + "\"contents\": {\"en\": \"" + pushMessage + "\"},"
                                                    + "\"included_segments\": [\"All\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return false;
            }

            System.Diagnostics.Debug.WriteLine(responseContent);

            return true;
        }

        public static bool PushNotification(string pushMessage, string title)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + RestAPIKey);

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"" + ApplicationId + "\","
                                                    + "\"contents\": {\"en\": \"" + pushMessage + "\"},"
                                                    + "\"headings\": {\"en\": \"" + title + "\"},"
                                                    + "\"included_segments\": [\"All\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return false;
            }

            System.Diagnostics.Debug.WriteLine(responseContent);

            return true;
        }

        public static bool PushNotification(string pushMessage, string objectName, string objectValue)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + RestAPIKey);

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"" + ApplicationId + "\","
                                                    + "\"contents\": {\"en\": \"" + pushMessage + "\"},"
                                                    + "\"" + objectName + "\": [\"" + objectValue + "\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return false;
            }

            System.Diagnostics.Debug.WriteLine(responseContent);

            return true;
        }

        public static bool PushNotification(string pushMessage, string title, string objectName, string objectValue)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + RestAPIKey);

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"" + ApplicationId + "\","
                                                    + "\"contents\": {\"en\": \"" + pushMessage + "\"},"
                                                    + "\"" + objectName + "\": [\"" + objectValue + "\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return false;
            }

            System.Diagnostics.Debug.WriteLine(responseContent);

            return true;
        }
    }
}
