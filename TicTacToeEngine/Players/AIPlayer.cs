using System;
using TicTacToeEngine.Strategies;

namespace TicTacToeEngine.Players
{
    public class AIPlayer : Player
    {
        public override bool IsAI => true;

        private readonly StrategyBase _strategy;

        public AIPlayer(PlayerType playerType, StrategyBase strategy, Action<TileCoordinates> onPlayedAction) : base(playerType, onPlayedAction)
        {
            _strategy = strategy;
        }

        public void PlayTurn(BoardStateType[,] boardState, int moveCount)
        {
            var selectedTile = _strategy.GetNextMove(boardState, moveCount);
            
            InvokeAction(selectedTile);
        }
    }
}