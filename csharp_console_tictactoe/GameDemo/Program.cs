using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            char symbolA = 'X';
            char symbolB = 'O';
            char spaceSymbol = ' ';
            string playerAName, playerBName; // players
            Movement mova = new Movement(); // coordinates for player A
            Movement movb = new Movement(); // coordinates for player B
            IDevice terminal = new CharConsole(spaceSymbol); // text console game
            GameMaster master = new GameMaster(terminal,symbolA,symbolB, spaceSymbol); // this manage the roles of the game
            IPlayer playerA = new HumanPlayer(terminal,symbolA);
            IPlayer playerB = new HumanPlayer(terminal,symbolB);
            IPlayer firstPlayer=playerA,secondPlayer=playerB; // firstPlayer is the first that move

            // read player names from input
            do
                playerAName = playerA.ReadName();
            while (!master.ValidatePlayerName(playerAName));
            do
                playerBName = playerB.ReadName();
            while (!master.ValidatePlayerName(playerBName) || (playerAName==playerBName));

            terminal.ShowPlayerNames(playerAName, playerBName,playerA.GetSymbol(),playerB.GetSymbol());
            terminal.ShowBoard();

            // random choose first player between A and B
            var r = new Random();
            if (r.Next(2) == 0)
            {
                firstPlayer = playerB;
                secondPlayer = playerA;
            }

            // while not endgame { A move, B move}
            while (!master.IsEndGame())
            {
                // A Move
                do               
                    mova = firstPlayer.Move();              
                while (!master.IsValidMovement(mova, firstPlayer.GetSymbol()));
                terminal.AddSymbol(mova, firstPlayer.GetSymbol());
                terminal.ShowBoard();
                if (!master.IsEndGame())
                {
                    // B Move
                    do
                        movb = secondPlayer.Move();
                    while (!master.IsValidMovement(movb, secondPlayer.GetSymbol()));
                    terminal.AddSymbol(movb, secondPlayer.GetSymbol());
                }

                // show the game board
                terminal.ShowBoard();
            }

            // at this point the game and and we have a winner or no winners if there are no more possible movements
            char? winner = master.GetWinner();

            if (winner == symbolA)
                terminal.CongratsWinner(playerAName);
            else
            {
                if (winner == symbolB)
                    terminal.CongratsWinner(playerBName);
                else
                    terminal.CongratsWithNobody();
            }
            // close the terminal
            terminal.ShowEndGame();
        }
    }
}
