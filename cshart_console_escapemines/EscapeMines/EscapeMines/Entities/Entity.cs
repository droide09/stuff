using System;
namespace EscapeMines
{
    public abstract class Entity
    {
        public Coordinates Coordinates;

        public Entity()
        {
            Coordinates = new Coordinates();
        }

        public abstract void Move(char movement);
    }
}
