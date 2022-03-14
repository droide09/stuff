using System;

namespace EscapeMines
{
    public static class Utils
    {
        public static void ShowInfo(ConsoleColor color, string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

        public static void ShowMessage(string message)
        {
            ShowInfo(ConsoleColor.Gray, message);
        }

        public static void ShowError(string message)
        {
            ShowInfo(ConsoleColor.Red, message);
        }

        public static Coordinates ParseCoordinate(string entityName, string coord, char separator)
        {
            int x, y;

            var parsedData = coord.Split(separator);
            if (parsedData.Length != 2)
                throw new InvalidOperationException($"{entityName} coordinate wrong format in configuration file");
            if (!Int32.TryParse(parsedData[0], out x))
                throw new InvalidOperationException($"{entityName} coordinate x is invalid in configuration file");
            if (!Int32.TryParse(parsedData[1], out y))
                throw new InvalidOperationException($"{entityName} coordinate y is invalid in configuration file");
            if (x < 0)
                throw new InvalidOperationException($"{entityName} coordinate x is negative in configuration file");
            if (y < 0)
                throw new InvalidOperationException($"{entityName} coordinate y is negative in configuration file");

            return new Coordinates(x, y);
        }

        public static Position ParsePosition(string entityName, string pos, char separator)
        {
            var parsedData = pos.Split(separator);
            if (parsedData.Length != 3)
                throw new InvalidOperationException("turtle initial position format in configuration file");
            if (!Constants.Directions.Contains(parsedData[2]))
                throw new InvalidOperationException("turtle initial position direction is invalid in configuration file");
            return new Position()
            {
                Coordinates = ParseCoordinate(entityName, $"{parsedData[0]} {parsedData[1]}", separator),
                Direction = parsedData[2]
            };
        }

        public static void CheckCoordinateInsideBoard(string entityName, Coordinates coord, int width, int height)
        {
            if (!coord.IsInside(width,height))
                throw new InvalidOperationException($"{entityName} is outside the board");
        }
    }
}
