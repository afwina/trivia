using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Network
{
    public class HTTPHandler
    {
        private const string URL = "http://localhost:66535";

        public async Task<RequestMatchResponse> POSTRequestMatch()
        {
            WWWForm form = new WWWForm();
            UnityWebRequest req = UnityWebRequest.Post(URL + "/request", form);
            //await req.SendWebRequest();
            return new RequestMatchResponse();
        }
    }

    public struct RequestMatchResponse
    {
        
    }
}