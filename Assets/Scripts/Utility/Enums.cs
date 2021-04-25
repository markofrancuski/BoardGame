namespace Enums
{
    namespace Card
    {
        public enum CardType
        {
            NORMAL,
            CHAMPION,
            TERRAIN,
            SPELL
        }
    }

    namespace Game
    {
        public enum GamePhase
        {
            WAITING_FOR_BOTH_PLAYERS,
            DECK_SHUFFLE,
            INITIAL_SPAWNING,
        }


        public enum PawnAction
        {
            MOVE,
            ATTACK,
            SPELL,
        }
    }

}
