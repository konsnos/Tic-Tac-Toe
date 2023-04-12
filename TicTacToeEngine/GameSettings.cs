namespace TicTacToeEngine
{
    public class GameSettings
    {
        public PlayerSettings PlayerX { get; }
        public PlayerSettings PlayerO { get; }
        
        public GameSettings(PlayerSettings playerX, PlayerSettings playerO)
        {
            PlayerX = playerX;
            PlayerO = playerO;
        }
    }
}