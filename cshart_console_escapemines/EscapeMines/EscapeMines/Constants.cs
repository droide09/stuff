namespace EscapeMines
{
    public static class Constants
    {
        //lines in the configuration file
        public const int LineBoardSize = 0;
        public const int LineMines = 1;
        public const int LineExitPoint = 2;
        public const int LineStartingPoint = 3;
        public const int LineMovements = 4;

        public const char BoardSizeSeparator = ' ';
        public const char MineSeparator = ',';
        public const char EndPointSeparator = ' ';
        public const char StartingPointSeparator = ' ';
        public const string MovementsSeparator = " ";

        public const string TurnRight = "R";
        public const string TurnLeft = "R";
        public const string Move = "M";
        public const string Directions = "NESW";
    }
}
