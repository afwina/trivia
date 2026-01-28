using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Network
{
    public class HTTPHandler
    {
        private const string URL = "http://localhost:65535";

        public async Task<RequestMatchResponse> POSTRequestMatch()
        {
            WWWForm form = new WWWForm();
            UnityWebRequest req = UnityWebRequest.Post(URL + "/request", form);
            await req.SendWebRequest();

            Debug.Log(req.downloadHandler.text);
            
            HTTPResponse<RequestMatchResponse> obj = JsonConvert.DeserializeObject<HTTPResponse<RequestMatchResponse>>(req.downloadHandler.text);
            
            return obj.body;
        }
    }

    public class HTTPResponse<T>
    {
        public string success;
        public int status;
        public T body;
    }
        
    public class RequestMatchResponse 
    {
        public string JoinCode;
    }
}