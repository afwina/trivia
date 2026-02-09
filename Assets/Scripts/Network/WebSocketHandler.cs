using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Network;
using NativeWebSocket;
using Newtonsoft.Json;
using UnityEngine;

public class WebSocketHandler 
{
    private const string URL = "ws://localhost:65534";
    private const string CLIENT_ID = "gc_001";
    
    private WebSocket m_WebSocket;
    private readonly ConcurrentDictionary<string, MessageHandler> m_MessageHandlers = new ();

    public bool IsOpen { get; private set; }

    public async Task Connect()
    {
        TaskCompletionSource<bool> hasOpened = new TaskCompletionSource<bool>();
        m_WebSocket = new WebSocket(URL);

        m_WebSocket.OnOpen += () =>
        {
            Debug.Log("OPEN");
            IsOpen = true;
            hasOpened.SetResult(true);
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
            string thing = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received Message: "+thing);

            ServerEvent e = JsonConvert.DeserializeObject<ServerEvent>(thing);
            if (m_MessageHandlers.TryGetValue(e.Event, out MessageHandler handler))
            {
                if (e.Success)
                {
                    handler.OnSuccess.Invoke(e.Data);
                }
                else
                {
                    handler.OnFailure.Invoke(e.Data);
                }
            }
            
        };

        m_WebSocket.Connect();
        await hasOpened.Task;
        Process();
    }

    public void AddMessageListener(string eventName, Action<string> onSuccess, Action<string> onFail = null)
    {
        m_MessageHandlers.TryAdd(eventName, new MessageHandler{OnSuccess = onSuccess, OnFailure = onFail});
    }
    
    public void RemoveMessageListener(string eventName)
    {
        m_MessageHandlers.Remove(eventName, out _);
    }

    public async Task SendMessage(string eventName, string matchId, string data = "")
    {
        ClientEvent e = new ClientEvent {Event = eventName, ClientId = CLIENT_ID, MatchId = matchId, Data = data};
        m_WebSocket.SendText(JsonConvert.SerializeObject(e));
        Debug.Log("SEND "+ eventName);
    }

    private async void Process()
    {
        while (true)
        {
            m_WebSocket.DispatchMessageQueue();
            await Task.Delay(1000);
        }
    }
}
