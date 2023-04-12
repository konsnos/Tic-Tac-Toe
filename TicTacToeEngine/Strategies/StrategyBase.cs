using System.Collections.Generic;
using TicTacToeEngine.Players;

namespace TicTacToeEngine.Strategies
{
    public abstract class StrategyBase
    {
        protected PlayerType _playerType;
        protected BoardStateType _playerBoardStateType;
        protected BoardStateType _enemyPlayerBoardStateType;

        public StrategyBase(PlayerType playerType)
        {
            _playerType = playerType;
            _playerBoardStateType = TicTacToeManager.GetBoardStateType(_playerType);

            var enemyPlayerType = _playerType == PlayerType.X ? PlayerType.O : PlayerType.X;
            _enemyPlayerBoardStateType = TicTacToeManager.GetBoardStateType(enemyPlayerType);
        }

        public abstract TileCoordinates GetNextMove(BoardStateType[,] boardState, int moveCount);

        protected List<TileCoordinates> GetAvailableMoves(BoardStateType[,] boardState)
        {
            var availableMoves = new List<TileCoordinates>(boardState.Length);

            for (int y = 0; y < boardState.GetLength(1); y++)
            {
                for (int x = 0; x < boardState.GetLength(0); x++)
                {
                    if (boardState[x, y] == BoardStateType.Empty)
                    {
                        availableMoves.Add(new TileCoordinates(x, y));
                    }
                }
            }

            return availableMoves;
        }

        protected bool GetWinMove(BoardStateType boardStateTypeToSearch, BoardStateType[,] boardState,
            out TileCoordinates tileCoordinates)
        {
            tileCoordinates = new TileCoordinates();

            int count;

            // Horizontal
            for (int y = 0; y < TicTacToeManager.BoardSize; y++)
            {
                count = 0;
                for (int x = 0; x < TicTacToeManager.BoardSize; x++)
                {
                    if (boardState[x, y] == boardStateTypeToSearch)
                    {
                        count++;
                    }
                    else if(boardState[x, y] == BoardStateType.Empty)
                    {
                        tileCoordinates = new TileCoordinates(x, y);
                    }
                    else
                    {
                        // already blocked
                        count = 0;
                        break;
                    }
                }

                if (count == 2) return true;
            }

            // Vertical
            for (int x = 0; x < TicTacToeManager.BoardSize; x++)
            {
                count = 0;
                for (int y = 0; y < TicTacToeManager.BoardSize; y++)
                {
                    if (boardState[x, y] == boardStateTypeToSearch)
                    {
                        count++;
                    }
                    else if(boardState[x, y] == BoardStateType.Empty)
                    {
                        tileCoordinates = new TileCoordinates(x, y);
                    }
                    else
                    {
                        // already blocked
                        count = 0;
                        break;
                    }
                }

                if (count == 2) return true;
            }

            // Diagonal top left to bottom right
            count = 0;
            for (int i = 0; i < TicTacToeManager.BoardSize; i++)
            {
                if (boardState[i, i] == boardStateTypeToSearch)
                {
                    count++;
                }
                else if(boardState[i, i] == BoardStateType.Empty)
                {
                    tileCoordinates = new TileCoordinates(i, i);
                }
                else
                {
                    // already blocked
                    count = 0;
                    break;
                }
            }

            if (count == 2) return true;

            // Diagonal top right to bottom left
            count = 0;
            for (int i = 0; i < TicTacToeManager.BoardSize; i++)
            {
                int y = (TicTacToeManager.BoardSize - 1) - i;
                if (boardState[i, y] == boardStateTypeToSearch)
                {
                    count++;
                }
                else if(boardState[i, y] == BoardStateType.Empty)
                {
                    tileCoordinates = new TileCoordinates(i, y);
                }
                else
                {
                    // already blocked
                    count = 0;
                    break;
                }
            }

            if (count == 2) return true;

            return false;
        }
    }
}