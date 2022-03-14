using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemo
{
    class CharConsole : IDevice
    {
        int startYPlayerNames = 2;
        int startYMap = 4;
        int startYInput = 20;
        int startYCongrats = 22;
        char[,] map = new char[3,3];
        char spaceSymbol = ' ';

        public CharConsole(char pSpaceSymbol = '.')
        {
            spaceSymbol = pSpaceSymbol;
            Clear();
            Console.WriteLine("Welcome!");
        }


        public void Clear()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    map[x, y] = spaceSymbol;

        }

        private void ClearLine(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.WriteLine("                                               ");
        }

        public void AddSymbol(Movement mov, char symbol)
        {
            map[mov.x, mov.y] = symbol;
        }

        public string ReadPlayerName(char symbol)
        {
            ClearLine(startYPlayerNames);
            ClearLine(startYPlayerNames+1);
            Console.SetCursorPosition(0, startYPlayerNames);
            Console.WriteLine("Insert player name for ["+symbol+"]:");
            Console.SetCursorPosition(0, startYPlayerNames + 1);
            return Console.ReadLine();
        }

        public Movement ReadInputCoordinates(string playerName)
        {
            int x, y;
            Console.ForegroundColor = ConsoleColor.Blue;
            ClearLine(startYInput);
            ClearLine(startYInput+1);
            Console.SetCursorPosition(0, startYInput);
            Console.WriteLine("["+playerName+"] Insert Coordinates [1..3,1..3] (Example: 1,1):");
            Console.WriteLine("                                    ");
            Console.SetCursorPosition(0, startYInput + 1);
            string text = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            if ((Int32.TryParse(text.Split(',')[0],out x)) && (Int32.TryParse(text.Split(',')[1], out y)))
            {
                return new Movement(--x, --y);
            }
            else
                return null;
        }

        public char[,] GetMap()
        {
            return map;
        }
        public void ShowPlayerNames(string playerA,string playerB,char symbolA,char symbolB)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            ClearLine(startYPlayerNames);
            ClearLine(startYPlayerNames+1);
            Console.SetCursorPosition(0, startYPlayerNames);
            Console.WriteLine(">!!"+playerA +"["+symbolA+"]  VS " + playerB+"["+symbolB+"]!!<");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowBoard()
        {
            Console.SetCursorPosition(0,startYMap);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("   |   |   ");
            Console.WriteLine("   |   |   ");
            Console.WriteLine("   |   |   ");
            Console.WriteLine("---+---+---");
            Console.WriteLine("   |   |  ");
            Console.WriteLine("   |   |   ");
            Console.WriteLine("   |   |   ");
            Console.WriteLine("---+---+---");
            Console.WriteLine("   |   |  ");
            Console.WriteLine("   |   |   ");
            Console.WriteLine("   |   |   ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int y=0;y<3;y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.SetCursorPosition(x*4 +1, y*4 + startYMap+1);
                    Console.Write(map[x,y]);
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        public void CongratsWinner(string winnerName)
        {
            ClearLine(startYCongrats);
            Console.SetCursorPosition(0, startYCongrats);
            Console.WriteLine("Player "+winnerName+" won! Congratulations "+ winnerName+"!" );
            Console.ReadLine();
        }

        public void CongratsWithNobody()
        {
            ClearLine(startYCongrats);
            Console.SetCursorPosition(0, startYCongrats);
            Console.WriteLine("I'm so sorry, nobody won!");
            Console.ReadLine();
        }

        public void ShowEndGame()
        {
            Console.WriteLine("Game Over! Press any key");
            Console.ReadLine();
        }
    }
}
