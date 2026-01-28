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
    private readonly ConcurrentDictionary<string, Action<string>> m_MessageHandlers = new ();

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

            ClientEvent e = JsonConvert.DeserializeObject<ClientEvent>(thing);
            if (m_MessageHandlers.TryGetValue(e.Event, out Action<string> action))
            {
                action.Invoke(e.Data);
            }
            
        };

        m_WebSocket.Connect();
        await hasOpened.Task;
        Process();
    }

    public void AddMessageListener(string eventName, Action<string> action)
    {
        m_MessageHandlers.TryAdd(eventName, action);
    }

    public async Task SendMessage(string eventName, string data)
    {
        ClientEvent e = new ClientEvent {Event = eventName, ClientId = CLIENT_ID, Data = data};
        m_WebSocket.SendText(JsonConvert.SerializeObject(e));
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
