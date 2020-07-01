using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChoreCore.DB
{
    public class UtilityHelper
    {
        private const string USER_AGENT = "firebase-net/0.2";

        public static bool ValidateURI(string url)
        {
            Uri locurl;

            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out locurl))
            {
                if (!(locurl.IsAbsoluteUri &&
                      (locurl.Scheme == "http" || locurl.Scheme == "https")) ||
                    !locurl.IsAbsoluteUri)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool TryParseJSON(string inJSON, out string output)
        {
            try
            {
                JToken parsedJSON = JToken.Parse(inJSON);
                output = parsedJSON.ToString();
                return true;
            }
            catch (Exception ex)
            {
                output = ex.Message;
                return false;
            }
        }

        public static Task<HttpResponseMessage> RequestHelper(HttpMethod method, Uri uri, string json = null)
        {
            if (!string.IsNullOrEmpty(AuthHelper.ACCESS_TOKEN))
                uri = new Uri($"{uri}?access_token={AuthHelper.ACCESS_TOKEN}");

            var client = new HttpClient();
            var msg = new HttpRequestMessage(method, uri);
            msg.Headers.Add("user-agent", USER_AGENT);
            if (json != null)
            {
                msg.Content = new StringContent(
                    json,
                    UnicodeEncoding.UTF8,
                    "application/json");
            }

            return client.SendAsync(msg);
        }
    }
}
