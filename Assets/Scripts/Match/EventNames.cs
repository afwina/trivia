namespace Game.Match
{
    public static class EventNames
    {
        // Client events
        public const string GAME_CLIENT_JOIN = "gameClientJoin";
        public const string START_MATCH = "startMatch";
        public const string END_MATCH = "endMatch";
        public const string START_QUESTION = "startQuestion";
        public const string CLOSE_QUESTION = "closeQuestion";

        // Server events
        public const string PLAYER_JOINED = "playerJoined";
        public const string PLAYER_ANSWER = "answer";

    }
}