using System.Collections.Generic;

namespace EscapeMines
{
    // world of the game
    public interface IWorld
    {
        // status of the game
        int Status { get; }

        // number of sequences of movememnts
        IEnumerable<string> Sequences();

        // exeution of movements in a sequence of movements
        void Go(string sequence);

        // return information of the status of the gam
        string GetInfo();
    }
}