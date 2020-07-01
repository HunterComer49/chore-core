using System.Net.Http;

namespace ChoreCore.DB
{
    public class FirebaseResponse
    {
        public FirebaseResponse() { }

        public FirebaseResponse(bool success, string errorMessage, HttpResponseMessage httpResponse = null, 
            string jsonContent = null)
        {
            Success = success;
            JSONContent = jsonContent;
            ErrorMessage = errorMessage;
            HttpResponse = httpResponse;
        }

        public bool Success { get; set; }

        public string JSONContent { get; set; }

        public string ErrorMessage { get; set; }

        public HttpResponseMessage HttpResponse { get; set; }
    }
}
