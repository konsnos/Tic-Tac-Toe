using System;
using TicTacToeEngine.Strategies;

namespace TicTacToeEngine.Players
{
    public static class PlayersFactory
    {
        public static Player GetPlayer(PlayerSettings playerSettings, Action<TileCoordinates> OnPlayedAction)
        {
            if (!playerSettings.IsAI) return new HumanPlayer(playerSettings.PlayerType, OnPlayedAction);

            return new AIPlayer(playerSettings.PlayerType, GetStrategy(playerSettings.PlayerType, playerSettings.AIDifficulty), OnPlayedAction);
        }

        private static StrategyBase GetStrategy(PlayerType playerType, AIDifficulty aiDifficulty)
        {
            return aiDifficulty switch
            {
                AIDifficulty.Easy => new RandomStrategy(playerType),
                AIDifficulty.Medium => new DefensiveStrategy(playerType),
                AIDifficulty.Hard => new PerfectStrategy(playerType),
                _ => throw new ArgumentOutOfRangeException(nameof(aiDifficulty), aiDifficulty, null)
            };
        }
    }
}