using System;

namespace TicTacToeEngine.Players
{
    public class HumanPlayer : Player
    {
        public override bool IsAI => false;

        public void PlayTurn(TileCoordinates selectedTile)
        {
            InvokeAction(selectedTile);
        }

        public HumanPlayer(PlayerType playerType, Action<TileCoordinates> onPlayedAction) : base(playerType, onPlayedAction)
        {
        }
    }
}