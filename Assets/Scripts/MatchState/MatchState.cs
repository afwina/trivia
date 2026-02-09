using Game.Input;
using Unity.VisualScripting;

namespace Game.MatchState
{
    public abstract class MatchState
    {
        public static string Name { get; }
        protected MatchStateContext Context { get; }

        public MatchState(MatchStateContext context)
        {
            Context = context;
        }
        
        public abstract void OnEnter();
        public abstract void Update();
        public abstract void OnExit();
    }
}