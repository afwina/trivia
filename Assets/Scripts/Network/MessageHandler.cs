using System;

namespace Game.Network
{
    public class MessageHandler
    {
        public Action<string> OnSuccess;
        public Action<string> OnFailure;
    }
}