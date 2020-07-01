using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChoreCore.DB
{
    public class FirebaseDB
    {
        public FirebaseDB(string baseURL)
        {
            RootNode = baseURL;
        }

        private string RootNode { get; set; }

        public FirebaseDB Node(string node)
        {
            if (node.Contains("/"))
            {
                throw new FormatException("Node must not contain '/', use NodePath instead.");
            }

            return new FirebaseDB(RootNode + '/' + node);
        }

        public FirebaseDB NodePath(string nodePath)
        {
            return new FirebaseDB(RootNode + '/' + nodePath);
        }

        public async Task<FirebaseResponse> Get()
        {
            return await new FirebaseRequest(HttpMethod.Get, RootNode).ExecuteAsync();
        }

        public async Task<FirebaseResponse> Put(string jsonData)
        {
            return await new FirebaseRequest(HttpMethod.Put, RootNode, jsonData).ExecuteAsync();
        }

        public async Task<FirebaseResponse> Post(string jsonData)
        {
            return await new FirebaseRequest(HttpMethod.Post, RootNode, jsonData).ExecuteAsync();
        }

        public async Task<FirebaseResponse> Patch(string jsonData)
        {
            return await  new FirebaseRequest(new HttpMethod("PATCH"), RootNode, jsonData).ExecuteAsync();
        }

        public async Task<FirebaseResponse> Delete()
        {
            return await new FirebaseRequest(HttpMethod.Delete, RootNode).ExecuteAsync();
        }

        public override string ToString()
        {
            return RootNode;
        }
    }
}