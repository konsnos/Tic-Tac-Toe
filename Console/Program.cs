// See https://aka.ms/new-console-template for more information

using TicTacToeEngine;
using TicTacToeEngine.Players;

var gameSettings = new GameSettings(
    new PlayerSettings(PlayerType.X, true, AIDifficulty.Easy),
    new PlayerSettings(PlayerType.O, true, AIDifficulty.Hard)
    );

var ticTacToeManager = new TicTacToeManager(gameSettings.PlayerX, gameSettings.PlayerO);

ticTacToeManager.Start(OnPlayedAction, OnActivePlayerChange, OnGameEnded);

void OnPlayedAction(TileCoordinates selectedTileCoordinates)
{
    // not used for now
}

void OnActivePlayerChange(PlayerType playerType)
{
    // not used for now
}

void OnGameEnded(GameEndType gameEndType)
{
    if (gameEndType == GameEndType.Draw)
    {
        Console.WriteLine("Game ended in a draw");
    }
    else
    {
        Console.WriteLine($"Player {ticTacToeManager.ActivePlayerType} won!");
    }
            
    ticTacToeManager.Stop();
}

Console.WriteLine("Program ended...");