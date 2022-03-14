using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMines
{
    public class TurtleMineWorld : IWorld
    {

        private int _endStatus; // status of the game

        private Configuration _configuration;

        public int Status
        {
            get 
            { 
                return _endStatus; 
            }
        }

        private Turtle _turtle;
        private List<Mine> _mines;

        public TurtleMineWorld(Configuration config)
        {
            _configuration = config;

            // create turtle and mine objects
            _turtle = new Turtle(_configuration.TurtleStartingPosition);
            _mines = new List<Mine>();
            _configuration.MinesCoordinates.ForEach(coordinate => _mines.Add(new Mine(coordinate)));
        }

        // list of steps in the game
        // for each iteratin returns a sequence of movements
        public IEnumerable<string> Sequences()
        {
            foreach (var sequence in _configuration.TurtleMovementsList)
            {
                yield return sequence;
            }
        }

        // execute a sequence of movements
        public void Go(string sequence)
        {
            _turtle.Home();
            _endStatus = ResultStates.StillInDanger; //initial status
            foreach (var move in sequence)
            {
                _turtle.Move(move);
                Update();
                if ((_endStatus == ResultStates.Success) || (_endStatus == ResultStates.MineHit))
                    break;
            }
        }

        // update the world based on the last move, we check for collision with mine
        // and if the turtle is in the exit point
        private void Update()
        {
            // check if turtle is inside the board
            if (!_turtle.Coordinates.IsInside(_configuration.Width,_configuration.Height))
                    throw new InvalidOperationException();

            // if turtle go over a mine            
            if (_mines.Find(x => x.Coordinates.Equals(_turtle.Coordinates))!=null)
                _endStatus = ResultStates.MineHit;

            // if turtle go over reaches the exit point
            if (_turtle.Coordinates.Equals(_configuration.ExitPoint))
                _endStatus = ResultStates.Success;
        }

        // return information on the current status of the game
        public string GetInfo()
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine($"Board size is {_configuration.Width}x{_configuration.Height}");
            info.AppendLine($"Turtle is in ({_turtle.Coordinates.X},{_turtle.Coordinates.Y})");
            info.Append("Mines are in");
            foreach (var mine in _mines)
                info.Append($" ({mine.Coordinates.X},{mine.Coordinates.Y})");
            info.AppendLine("");
            info.AppendLine($"Exit point is in ({_configuration.ExitPoint.X},{_configuration.ExitPoint.Y})");
            return info.ToString();
        }
    }
}
