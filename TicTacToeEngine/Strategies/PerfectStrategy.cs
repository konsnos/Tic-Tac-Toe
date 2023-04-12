using System;
using System.Collections.Generic;
using TicTacToeEngine.Players;

namespace TicTacToeEngine.Strategies
{
    public class PerfectStrategy : StrategyBase
    {
        private readonly Random _random;
        
        public PerfectStrategy(PlayerType playerType) : base(playerType)
        {
            _random = new Random();
        }

        public override TileCoordinates GetNextMove(BoardStateType[,] boardState, int moveCount)
        {
            MinMax(boardState, true, moveCount, out var tileCoordinates, 0);

            return tileCoordinates;
        }

        private int MinMax(BoardStateType[,] boardState, bool isSelf, int moveCount, out TileCoordinates tileCoordinates, int depth)
        {
            var availableMoves = GetAvailableMoves(boardState);
            tileCoordinates = new TileCoordinates();
            if (availableMoves.Count == 0) return 0;
            
            int[] scores = new int[availableMoves.Count];

            for (int i = 0; i < availableMoves.Count; i++)
            {
                var newMove = availableMoves[i];
                // var newBoard = boardState.Clone() as BoardStateType[,];
                var newBoardStateType = isSelf ? _playerBoardStateType : _enemyPlayerBoardStateType;
                boardState[newMove.X, newMove.Y] = newBoardStateType;
                int newMoveCount = moveCount + 1;
                
                scores[i] = GetScore(boardState, newMoveCount, newMove, newBoardStateType, isSelf, depth);
                if (scores[i] == 0)
                {
                    scores[i] = MinMax(boardState, !isSelf, newMoveCount, out _, depth + 1);
                }

                boardState[newMove.X, newMove.Y] = BoardStateType.Empty;
            }

            var sameList = new List<int>();
            int index = 0;
            int score;
            if (isSelf)
            {
                score = -20;
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i] > score)
                    {
                        index = i;
                        score = scores[i];
                        sameList.Clear();
                        sameList.Add(i);
                    }
                    else if (scores[i] == score)
                    {
                        sameList.Add(i);
                    }
                }
            }
            else
            {
                score = 20;
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i] < score)
                    {
                        index = i;
                        score = scores[i];
                        sameList.Clear();
                        sameList.Add(i);
                    }
                    else if (scores[i] == score)
                    {
                        sameList.Add(i);
                    }
                }
            }

            if (sameList.Count > 1)
            {
                index = sameList[_random.Next(sameList.Count)];
            }

            // Uncomment for debug
            // if (depth == 0)
            //     new TileCoordinates();

            tileCoordinates = availableMoves[index];
            
            return score;
        }

        private static int GetScore(BoardStateType[,] boardState, int moveCount, TileCoordinates lastTileSelected,
            BoardStateType boardStateType, bool isSelf, int depth)
        {
            var endType = TicTacToeManager.GetEnded(boardState, moveCount, lastTileSelected, boardStateType);
            if (endType == GameEndType.Won)
            {
                if (isSelf) return 10 - depth;
                return depth-10;
            }

            return 0;
        }
    }
}