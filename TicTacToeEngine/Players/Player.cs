using System;

namespace TicTacToeEngine.Players
{
    public abstract class Player
    {
        public abstract bool IsAI { get; }

        public PlayerType PlayerType { get; }

        private readonly Action<TileCoordinates> _playedAction;

        protected Player(PlayerType playerType, Action<TileCoordinates> onPlayedAction)
        {
            PlayerType = playerType;
            _playedAction = onPlayedAction;
        }

        protected void InvokeAction(TileCoordinates selectedTile)
        {
            _playedAction.Invoke(selectedTile);
        }
    }

    public enum PlayerType
    {
        X,
        O
    }
}