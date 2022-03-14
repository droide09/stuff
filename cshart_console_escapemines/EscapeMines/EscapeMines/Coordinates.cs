namespace EscapeMines
{
    public class Coordinates
    {
        public int X;
        public int Y;

        public Coordinates()
        {
        }

        public Coordinates(int x,int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object y)
        {
            var other = (Coordinates)y;
            if ((this.X == other.X) && (this.Y == other.Y))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + this.X;
            hash = hash * 31 + this.Y;

            return hash;
        }

        public bool IsInside(int boardWidth,int boardHeight)  
        {
            if ((X >= 0) && (X < boardWidth) && (Y >= 0) && (Y < boardHeight))
                return true;
            else
                return false;
        }

    }

    public struct Position
    {
        public Coordinates Coordinates;
        public string Direction;
    }
}
