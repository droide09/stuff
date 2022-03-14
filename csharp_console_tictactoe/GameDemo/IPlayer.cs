using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemo
{
    interface IPlayer
    {
        char GetSymbol();
        string ReadName();
        Movement Move();
    }
}
