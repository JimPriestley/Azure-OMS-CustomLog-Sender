using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OMSCustomLog
{
    public static class LogSender
    {
        //Build API Signature 
        private static string BuildSignature(string message, string secret)
        {
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = Convert.FromBase64String(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hash = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// This method posts a json object to the OMS Workspace, into a table called "CustomLogName"_CL
        /// </summary>
        /// <param name="WorkSpaceId">OMS WorkSpace ID</param>
        /// <param name="WorkSpaceKey">OMS WorkSpace Key</param>
        /// <param name="CustomLogName">Name of the CustomLog Table, automatically appended with "_CL" in OMS</param>
        /// <param name="EventJson">JSON object containing one or more log events. Each property will be dynamically created as a column in OMS</param>
        /// <returns></returns>
        public static string Send(string WorkSpaceId,  string WorkSpaceKey, string CustomLogName, string EventJson)
        {
            var eventDate = DateTime.UtcNow.ToString("r");

            var signature = BuildSignature(EventJson, WorkSpaceKey);

            try
            {
                string url = "https://" + WorkSpaceId + ".ods.opinsights.azure.com/api/logs?api-version=2016-04-01";

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Log-Type", CustomLogName);
                client.DefaultRequestHeaders.Add("Authorization", signature);
                client.DefaultRequestHeaders.Add("x-ms-date", eventDate);
                client.DefaultRequestHeaders.Add("time-generated-field", eventDate);

                System.Net.Http.HttpContent httpContent = new StringContent(EventJson, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                Task<System.Net.Http.HttpResponseMessage> response = client.PostAsync(new Uri(url), httpContent);

                System.Net.Http.HttpContent responseContent = response.Result.Content;
                return (responseContent.ReadAsStringAsync().Result);

            }
            catch (Exception excep)
            {
                throw new Exception("API Post Exception: " + excep.Message);
            }
        }



    }
}
