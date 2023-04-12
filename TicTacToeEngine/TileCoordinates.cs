namespace TicTacToeEngine
{
    public readonly struct TileCoordinates
    {
        public int X { get; }
        public int Y { get; }

        public TileCoordinates(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public override string ToString()
        {
            return $"Tile {X},{Y}";
        }
    }
}