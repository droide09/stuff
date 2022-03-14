using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemo
{
    class HumanPlayer : IPlayer
    {
        IDevice dev;
        char symbol;
        int maxPlayerNameLength = 50;
        string playerName;

        public HumanPlayer(IDevice pDev,char pSymbol)
        {
            symbol = pSymbol;
            dev = pDev;
        }

        public char GetSymbol()
        {
            return symbol;
        }

        public string ReadName()
        {
            do
            {
                playerName = dev.ReadPlayerName(symbol);
            } while ((playerName.Length <= 0) || (playerName.Length > maxPlayerNameLength));
            return playerName;
        }

        public Movement Move()
        {
            Movement coords;
            do
            {
                coords = dev.ReadInputCoordinates(playerName);
            } while (coords == null);

            return coords;
        }
    }
}
