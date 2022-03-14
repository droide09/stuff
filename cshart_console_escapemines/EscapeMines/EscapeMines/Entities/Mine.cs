using System;
namespace EscapeMines
{
    public class Mine : Entity
    {
        public Mine(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public override void Move(char movement)
        {
            // mines can't move
        }
    }
}
