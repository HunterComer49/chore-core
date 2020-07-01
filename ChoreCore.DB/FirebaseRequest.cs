using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChoreCore.DB
{
    public class FirebaseRequest
    {
        private const string JSON_SUFFIX = ".json";

        public FirebaseRequest(HttpMethod method, string uri, string jsonString = null)
        {
            Method = method;
            JSON = jsonString;
            if (uri.Replace("/", string.Empty).EndsWith("firebaseio.com"))
            {
                Uri = uri + '/' + JSON_SUFFIX;
            }
            else
            {
                Uri = uri + JSON_SUFFIX;
            }
        }

        private HttpMethod Method { get; set; }

        private string JSON { get; set; }

        private string Uri { get; set; }

        public async Task<FirebaseResponse> ExecuteAsync()
        {
            Uri requestURI;

            if (UtilityHelper.ValidateURI(Uri))
            {
                requestURI = new Uri(Uri);
            }
            else
            {
                return new FirebaseResponse(false, "Provided Firebase path is not a valid HTTP/S URL");
            }

            string json = null;

            if (JSON != null && !UtilityHelper.TryParseJSON(JSON, out json))
            {
                return new FirebaseResponse(false, string.Format("Invalid JSON : {0}", json));
            }

            Task<HttpResponseMessage> response = UtilityHelper.RequestHelper(Method, requestURI, json);
            HttpResponseMessage result = await response;

            FirebaseResponse firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = (await response).IsSuccessStatusCode
            };

            if (Method.Equals(HttpMethod.Get))
            {
                firebaseResponse.JSONContent = await result.Content.ReadAsStringAsync();
            }

            return firebaseResponse;
        }
    }
}
