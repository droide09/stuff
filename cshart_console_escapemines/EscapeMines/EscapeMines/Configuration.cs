using System;
using System.Collections.Generic;
using System.IO;

namespace EscapeMines
{
    public sealed class Configuration
    {
        private static Configuration _instance;

        // configuration parameters
        public int Width, Height;

        public List<Coordinates> MinesCoordinates;

        public Coordinates ExitPoint;

        public Position TurtleStartingPosition;

        public List<string> TurtleMovementsList;

        private Configuration()
        {
            MinesCoordinates = new List<Coordinates>();
            TurtleMovementsList = new List<string>();
        }

        public void Load(string fileName)
        {
            string[] parsedData;

            var lines = File.ReadAllLines(fileName);

            // get width and height of the board
            var boardSize = Utils.ParseCoordinate("board size",lines[Constants.LineBoardSize], Constants.BoardSizeSeparator);
            Width = boardSize.X;
            Height = boardSize.Y;

            MinesCoordinates.Clear();
            // get mines position
            if (lines[Constants.LineMines].Trim() != "")
            {
                parsedData = lines[1].Split(' ');
                for (var index = 0; index < parsedData.Length; index++)
                {
                    var coord = Utils.ParseCoordinate($"mine {index}",parsedData[index], Constants.MineSeparator);
                    Utils.CheckCoordinateInsideBoard($"mine {index}", coord, Width, Height);
                    MinesCoordinates.Add(coord);
                }
            }

            // get exit point
            ExitPoint = Utils.ParseCoordinate("exit point",lines[Constants.LineExitPoint],Constants.EndPointSeparator);
            Utils.CheckCoordinateInsideBoard("exit point", ExitPoint, Width, Height);
            if (MinesCoordinates.Find(mine => mine.Equals(ExitPoint))!=null)
                throw new InvalidOperationException("mines over exit point in configuration file");

            // get starting position
            TurtleStartingPosition = Utils.ParsePosition("turtle starting point",lines[Constants.LineStartingPoint],Constants.StartingPointSeparator);
            Utils.CheckCoordinateInsideBoard("turtle", ExitPoint, Width, Height);

            // get turtle moves
            TurtleMovementsList.Clear();
            for (var currentLine = Constants.LineMovements; currentLine < lines.Length; currentLine++)
            {
                var sequence = lines[currentLine].Replace(Constants.MovementsSeparator,"");
                TurtleMovementsList.Add(sequence);
            }
        }

        public static Configuration GetInstance()
        {
            if (_instance == null)
                _instance = new Configuration();
            return _instance;
        }
     }
}
