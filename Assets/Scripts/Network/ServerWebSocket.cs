using System.Threading.Tasks;
using NativeWebSocket;
using UnityEngine;

public class ServerWebSocket 
{
    private WebSocket m_WebSocket;
    private const string URL = "ws://localhost:66534";

    public async Task Connect()
    {
        m_WebSocket = new WebSocket(URL);

        m_WebSocket.OnOpen += () =>
        {
            Debug.Log("OPEN");
        };
        
        
        m_WebSocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        m_WebSocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        m_WebSocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");
            Debug.Log(bytes);

            // getting the message as a string
            // var message = System.Text.Encoding.UTF8.GetString(bytes);
            // Debug.Log("OnMessage! " + message);
        };

        await m_WebSocket.Connect();
    }

    public async Task SendMessage()
    {
        
    }
}
