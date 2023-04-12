using TicTacToeEngine.Players;

namespace TicTacToeEngine
{
    public struct PlayerSettings
    {
        public readonly PlayerType PlayerType { get; }
        public readonly bool IsAI { get; }
        public readonly AIDifficulty AIDifficulty { get; }

        public PlayerSettings(PlayerType playerType, bool isAI, AIDifficulty aiDifficulty)
        {
            PlayerType = playerType;
            IsAI = isAI;
            AIDifficulty = aiDifficulty;
        }
    }
}