using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemo
{
    class GameMaster
    {
        IDevice dev;
        char symbolA, symbolB, spaceSymbol;

        public GameMaster(IDevice pDev,char pSymbolA,char pSymbolB,char pSpaceSymbol)
        {
            dev = pDev;
            symbolA = pSymbolA;
            symbolB = pSymbolB;
            spaceSymbol = pSpaceSymbol;
        }

        public bool ValidatePlayerName(string name)
        {
            if ((name != null) && (name.Length > 0) && (name.Length < 50))
                return true;
            return false;
        }

        private bool checkkWinner(char[,] map,char symbol)
        {
            int countA = 0;
            // scan horizontal
            for (int y = 0; y < 3; y++)
            {
                countA = 0;
                for (int x = 0; x < 3; x++)
                    if (map[x, y] == symbol)
                        countA++;
                if (countA == 3)
                    return true;
            }
            // scan vertical
            for (int x = 0; x < 3; x++)
            {
                countA = 0;
                for (int y = 0; y < 3; y++)
                    if (map[x, y] == symbol)
                        countA++;
                if (countA == 3)
                    return true;
            }

            // scan diagonal
            countA = 0;
            for (int x = 0; x < 3; x++)
            {
                if (map[x, x] == symbol)
                    countA++;
            }
            if (countA == 3)
                return true;
            countA = 0;
            for (int x = 0; x < 3; x++)
            {
                if (map[2-x, x] == symbol)
                    countA++;
            }
            if (countA == 3)
                return true;

            return false;
        }

        private bool checkNobodyWin(char symbolA,char symbolB)
        {
            int count = 0;
            char[,] map = dev.GetMap();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                    if ((map[x, y] == symbolA) ||( map[x,y] == symbolB))
                        count++;
            }

            if (count==9)
                return true;
            return false;
        }

       
        public bool IsEndGame()
        {
            if (GetWinner() == null)
            {
                if (checkNobodyWin(symbolA, symbolB))
                    return true;
                else
                    return false;
            }
       
            return true;
        }

        public bool IsValidMovement(Movement mov, char symbol)
        {
            if ((mov.x < 0) || (mov.x > 2))
                return false;
            if ((mov.y < 0) || (mov.y > 2))
                return false;
            char[,] map = dev.GetMap();
            if (map[mov.x, mov.y] != spaceSymbol)
                return false;
            return true;
        }

        public char? GetWinner()
        {
            char[,] map = dev.GetMap();
            // check if A win
            if (checkkWinner(map, symbolA))
                return symbolA;
            if (checkkWinner(map, symbolB))
                return symbolB;
            return null;
        }
    }
}
