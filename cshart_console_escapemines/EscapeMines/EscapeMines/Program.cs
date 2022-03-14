using System;

namespace EscapeMines
{
    public class Program
    {
        static void ShowStatus(int state)
        {
            switch (state)
            {
                case ResultStates.MineHit:
                    Utils.ShowInfo(ConsoleColor.DarkRed, "Mine Hit – The turtle hits a mine");
                    break;
                case ResultStates.StillInDanger:
                    Utils.ShowInfo(ConsoleColor.Yellow, "Still in Danger – The turtle has not yet found the exit or hit a mine");
                    break;
                case ResultStates.Success:
                    Utils.ShowInfo(ConsoleColor.Green, "Success – The turtle finds the exit point");
                    break;
            }
        }

        static bool CheckCommandLineArguments(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Wrong number of arguments!");
                Console.WriteLine("Usage:");
                Console.WriteLine("   EscapeMines <configfile>\n\n");
                return false;
            }

            return true;
        }

        static void ExitProgram()
        {
            Console.WriteLine("\nPress ENTER to exit.");
            Console.ReadLine();
        }

        /* main program
         * 
         * load the configuration from the txt file 
         * initialize the game envoronment
         * loop throw to movements sequences and execute each sequence of movements
         * at the end of the execution of a sequence print the result
         * 
         */
        public static void Main(string[] args)
        {
            Console.WriteLine("Escape Mines!\n");
            if (!CheckCommandLineArguments(args))
            {
                ExitProgram();
            }

            // load the configuration
            var configuration = Configuration.GetInstance();
            try 
            {
                configuration.Load(args[0]);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
                ExitProgram();
            }
               
            // create and initialize the environment
            IWorld world = new TurtleMineWorld(configuration);
            Utils.ShowMessage(world.GetInfo());

            // for each sequence of movemebts do the steps to complete the game
            foreach (var sequence in world.Sequences())
            {
                Utils.ShowMessage($"\nRun sequence {sequence}");
                try
                {
                    // run a sequence of movements
                    world.Go(sequence);
                }
                catch (InvalidOperationException)
                {
                    Utils.ShowError("Turtle is out of the board");
                    continue;
                }
                // display result status after the execution of the sequence
                ShowStatus(world.Status); 
            }

            ExitProgram();
        }
    }
}
