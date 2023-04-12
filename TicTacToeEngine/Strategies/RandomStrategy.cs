using System;
using TicTacToeEngine.Players;

namespace TicTacToeEngine.Strategies
{
    public class RandomStrategy : StrategyBase
    {
        private readonly Random _random;

        public RandomStrategy(PlayerType playerType) : base(playerType)
        {
            _random = new Random();
        }

        public override TileCoordinates GetNextMove(BoardStateType[,] boardState, int moveCount)
        {
            var availableMoves = GetAvailableMoves(boardState);

            int index = _random.Next(availableMoves.Count);

            return availableMoves[index];
        }
    }
}