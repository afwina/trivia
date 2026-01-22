namespace Game.Input
{
    public class PlayerChoiceAction : AInputAction
    {
        public int Choice;
        
        public PlayerChoiceAction(string playerId, int choice)
        {
            PlayerId = playerId;
            Choice = choice;
        }
    }
}