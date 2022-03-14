using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemo
{
    interface IDevice
    {
        void Clear();

        void AddSymbol(Movement mov, char symbol);

        char[,] GetMap();

        void ShowBoard();

        string ReadPlayerName(char symbol);

        void ShowPlayerNames(string playerA, string playerB,char symbolA,char symbolB);

        Movement ReadInputCoordinates(string playerName);

        void CongratsWinner(string winner);

        void CongratsWithNobody();

        void ShowEndGame();
    }
}
