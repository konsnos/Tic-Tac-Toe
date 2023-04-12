using System;
using System.Diagnostics.Contracts;
using TicTacToeEngine.Players;

namespace TicTacToeEngine
{
    public class TicTacToeManager
    {
        public const int BoardSize = 3;

        /// <summary>
        /// Set as [columns,rows]
        /// </summary>
        public BoardStateType[,] BoardState { get; }

        private Action<TileCoordinates>? _playerAction;
        private Action<PlayerType>? _activePlayerChange;
        private Action<GameEndType>? _gameEnded;

        private readonly Player[] _players;
        public int ActivePlayerIndex { get; set; }
        private int _moveCount;
        private Player ActivePlayer => _players[ActivePlayerIndex];
        public PlayerType ActivePlayerType => ActivePlayer.PlayerType;
        public bool IsActivePlayerAI => ActivePlayer.IsAI;

        public TicTacToeManager(PlayerSettings playerSettingsX, PlayerSettings playerSettingsO)
        {
            BoardState = new BoardStateType[BoardSize, BoardSize];
            
            _players = new Player[2];
            _players[0] = PlayersFactory.GetPlayer(playerSettingsX, OnPlayedAction);
            _players[1] = PlayersFactory.GetPlayer(playerSettingsO, OnPlayedAction);
        }

        private void OnPlayedAction(TileCoordinates selectedTile)
        {
            if (BoardState[selectedTile.X, selectedTile.Y] != BoardStateType.Empty)
            {
                Console.WriteLine("Action is illegal");
                return;
            }

            BoardState[selectedTile.X, selectedTile.Y] = GetBoardStateType(ActivePlayer.PlayerType);

            Console.WriteLine($"Player {ActivePlayer.PlayerType} played at {selectedTile} for move {_moveCount}");
            _playerAction?.Invoke(selectedTile);

            var gameEndType = GetEnded(BoardState, _moveCount, selectedTile, ActivePlayer.PlayerType);

            switch (gameEndType)
            {
                case GameEndType.Draw:
                    Console.WriteLine("Game ended in draw");
                    _gameEnded?.Invoke(GameEndType.Draw);
                    break;
                case GameEndType.Won:
                    Console.WriteLine($"Game ended in player {ActivePlayer.PlayerType} win");
                    _gameEnded?.Invoke(GameEndType.Won);
                    break;
                case GameEndType.None:
                    AdvancePlayer();
                    break;
                default:
                    throw new Exception($"Unhandled game end type {gameEndType}");
            }
        }

        public static BoardStateType GetBoardStateType(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.X:
                    return BoardStateType.X;
                case PlayerType.O:
                    return BoardStateType.O;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }

        [Pure]
        public static GameEndType GetEnded(BoardStateType[,] boardState, int moveCount, TileCoordinates lastTileSelected, PlayerType activePlayerType)
        {
            var boardStateTypeToSearch = GetBoardStateType(activePlayerType);

            return GetEnded(boardState, moveCount, lastTileSelected, boardStateTypeToSearch);
        }

        [Pure]
        public static GameEndType GetEnded(BoardStateType[,] boardState, int moveCount, TileCoordinates lastTileSelected, BoardStateType boardStateTypeToSearch)
        {
            // Horizontal
            for (int x = 0; x < BoardSize; x++)
            {
                if (boardState[x, lastTileSelected.Y] != boardStateTypeToSearch)
                {
                    break;
                }

                if (x == BoardSize - 1) return GameEndType.Won;
            }

            // Vertical
            for (int y = 0; y < BoardSize; y++)
            {
                if (boardState[lastTileSelected.X, y] != boardStateTypeToSearch)
                {
                    break;
                }

                if (y == BoardSize - 1) return GameEndType.Won;
            }

            // Diagonal top left to bottom right
            if (lastTileSelected.X == lastTileSelected.Y)
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    if (boardState[i, i] != boardStateTypeToSearch)
                    {
                        break;
                    }

                    if (i == BoardSize - 1) return GameEndType.Won;
                }
            }

            // Diagonal top right to bottom left
            if (lastTileSelected.X + lastTileSelected.Y == BoardSize - 1)
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    if (boardState[i, (BoardSize - 1) - i] != boardStateTypeToSearch)
                    {
                        break;
                    }

                    if (i == BoardSize - 1) return GameEndType.Won;
                }
            }

            if (moveCount == Math.Pow(BoardSize, 2) - 1) return GameEndType.Draw;

            return GameEndType.None;
        }

        private void AdvancePlayer()
        {
            ActivePlayerIndex++;
            if (ActivePlayerIndex >= _players.Length)
            {
                ActivePlayerIndex = 0;
            }

            _moveCount++;

            _activePlayerChange?.Invoke(ActivePlayer.PlayerType);

            PlayTurn();
        }

        public void Start(Action<TileCoordinates> onPlayedAction, Action<PlayerType> onActivePlayerChange,
            Action<GameEndType> onGameEnded)
        {
            _playerAction = onPlayedAction;
            _activePlayerChange = onActivePlayerChange;
            _gameEnded = onGameEnded;

            ActivePlayerIndex = 0;
            _moveCount = 0;

            _activePlayerChange?.Invoke(ActivePlayer.PlayerType);

            PlayTurn();
        }

        public void Stop()
        {
            _playerAction = null;
            _activePlayerChange = null;
            _gameEnded = null;
        }

        private void PlayTurn()
        {
            if (!ActivePlayer.IsAI) return;

            ((AIPlayer)ActivePlayer).PlayTurn(BoardState, _moveCount);
        }

        public void PlayHumanTurn(TileCoordinates tileCoordinates)
        {
            if (ActivePlayer.IsAI) return;

            ((HumanPlayer)ActivePlayer).PlayTurn(tileCoordinates);
        }
    }

    public enum BoardStateType
    {
        Empty,
        X,
        O
    }

    public enum GameEndType
    {
        None,
        Won,
        Draw
    }

    public enum AIDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}