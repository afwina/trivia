using System;

namespace Game.Input
{
    public interface IInputController
    {
        public Action<AInputAction>  OnInputAction { get; set; }
    }
}