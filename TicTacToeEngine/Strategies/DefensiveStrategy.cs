using System;
using TicTacToeEngine.Players;

namespace TicTacToeEngine.Strategies
{
    public class DefensiveStrategy : StrategyBase
    {
        private readonly Random _random;

        public DefensiveStrategy(PlayerType playerType) : base(playerType)
        {
            _random = new Random();
        }
        
        public override TileCoordinates GetNextMove(BoardStateType[,] boardState, int moveCount)
        {
            var isWinMove = GetWinMove(_playerBoardStateType, boardState, out var tileCoordinates);
            if (isWinMove)
            {
                return tileCoordinates;
            }

            var isDefensiveMove = GetWinMove(_enemyPlayerBoardStateType, boardState, out var enemyTileCoordinates);
            if (isDefensiveMove)
            {
                return enemyTileCoordinates;
            }
            
            var availableMoves = GetAvailableMoves(boardState);

            int index = _random.Next(availableMoves.Count);

            return availableMoves[index];
        }
    }
}