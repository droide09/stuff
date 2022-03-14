using System;
namespace EscapeMines
{
    public class Turtle : Entity
    {
        public string Direction;
        private Position _initialPosition;

        public Turtle(Position position) : base()
        {
            _initialPosition = position;
            Home();
        }

        public void Home()
        {
            Coordinates = new Coordinates(_initialPosition.Coordinates.X, _initialPosition.Coordinates.Y);
            Direction = _initialPosition.Direction;
        }

        // determine the direction (North,South,East,West) from the orientation (+90 degress, -90 degree)
        // orientation = 1 is turn right
        // orientation = -1 is turn left
        protected string Turn(int orientation)
        {
            // we move forward or backward in the string "NESW"
            var currentDirectionPosition = Constants.Directions.IndexOf(Direction[0]);
            var nextDirectionPosition = currentDirectionPosition + orientation;

            if (nextDirectionPosition >= Constants.Directions.Length)
                nextDirectionPosition = 0;
            if (nextDirectionPosition < 0)
                nextDirectionPosition = Constants.Directions.Length - 1;

            return Constants.Directions[nextDirectionPosition].ToString();
        }

        public override void Move(char movement)
        {
            switch (movement)
            {
                case 'R':
                    Direction = Turn(1); // turn 90 degree
                    break;
                case 'L':
                    Direction = Turn(-1); // turn -90 degree
                    break;
                case 'M':
                    switch (Direction)
                    {
                        case "N":
                            Coordinates.Y--;
                            break;
                        case "S":
                            Coordinates.Y++;
                            break;
                        case "E":
                            Coordinates.X++;
                            break;
                        case "W":
                            Coordinates.X--;
                            break;
                    }
                    break;
            }
        }
    }
}
