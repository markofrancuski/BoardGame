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
    }

    namespace Tile
    {
        public enum TileState
        {
            NO_INPUT,
            DRAGGING_CARD,
            ACTION,
        }
    }
}
