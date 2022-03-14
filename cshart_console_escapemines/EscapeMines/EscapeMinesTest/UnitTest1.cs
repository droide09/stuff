using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace EscapeMines
{
    public class Tests
    {
        Configuration _configuration;
        string _configFileName = Path.Combine(Path.GetTempPath(),"config.txt");

        [SetUp]
        public void Setup()
        {
            _configuration = Configuration.GetInstance();
        }

        [TearDown]
        public void Cleanup()
        {
            if (_configuration != null)
                _configuration = null;
        }

        // run the sequences of moves and return the result status for each sequence
        private List<int> RunGame()
        {
            IWorld world = new TurtleMineWorld(_configuration);
            var results = new List<int>();
            foreach (var sequence in world.Sequences())
            {
                try
                {
                    world.Go(sequence);
                }
                catch (InvalidOperationException)
                {
                    Utils.ShowError("Turtle is out of the board");
                    throw;
                }
                results.Add(world.Status);
            }
            return results;
        }

        [Test]
        public void TestSuccess()
        {
            var configFileString = "10 10\n2,2 7,7 6,6 8,8 8,2\n9 4\n0 1 N\nR R M M M L M M M M M M M M M";

            File.WriteAllText(_configFileName, configFileString);
            _configuration.Load(_configFileName);
            Assert.That(RunGame()[0] == ResultStates.Success);
        }

        [Test]
        public void TestStillInDanger()
        {
            var configFileString = "10 10\n2,2 7,7 6,6 8,8 8,2\n9 4\n0 1 N\nR R M M M L M M M M M M M\n";

            File.WriteAllText(_configFileName, configFileString);
            _configuration.Load(_configFileName);
            Assert.That(RunGame()[0] == ResultStates.StillInDanger);
        }

        [Test]
        public void TestMineHit()
        {
            var configFileString = "10 10\n2,2 4,4 6,6 8,4 8,2\n9 4\n0 1 N\nR R M M M L M M M M M M M M M\n";

            File.WriteAllText(_configFileName, configFileString);
            _configuration.Load(_configFileName);
            Assert.That(RunGame()[0] == ResultStates.MineHit);
        }

        [Test]
        public void TestMultipleSequences()
        {
            var configFileString = "10 10\n2,2 7,7 6,6 8,8 8,2\n9 4\n0 1 N\nR R M M M L M M M M M M M M M\nR R M M M L M M M M M M M\nR M M R M M M";

            File.WriteAllText(_configFileName, configFileString);
            _configuration.Load(_configFileName);
            var results = RunGame();
            Assert.That(results[0] == ResultStates.Success);
            Assert.That(results[1] == ResultStates.StillInDanger);
            Assert.That(results[2] == ResultStates.MineHit);
        }


        [Test]
        public void TestTurtleOutOfBoard()
        {
            var configFileString = "10 10\n2,2\n9 3\n0 1 N\nR R M M M L M M M M M M M M M M M M M M M M M M M M M M M M M M M\n";

            File.WriteAllText(_configFileName, configFileString);
            _configuration.Load(_configFileName);
            Assert.That(() =>
            {
                RunGame();
            },
            Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void TestMineOutOfBoard()
        {
            var configFileString = "10 10\n2,2 4,4 4,14\n9 3\n0 1 N\nR R M M M L M M M M M M M M M M M M M\n";

            File.WriteAllText(_configFileName, configFileString);
            Assert.That(() =>
            {
                _configuration.Load(_configFileName);
            },
            Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void TestMissingFile()
        {
            Assert.That(() =>
            {
                _configuration.Load("missing.cfg");
            },
            Throws.Exception.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void TestInvalidBoardDimensions()
        {
            var configFileString = "10 A\n2,2 4,4 6,6 8,8 8,2\n9 4\n0 1 N\nR R M M M \nL M M M M M M M M M\n";

            File.WriteAllText(_configFileName, configFileString);

            Assert.That(() =>
            {
                _configuration.Load(_configFileName);
            },
            Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void TestMissingLine()
        {
            var configFileString = "10 10\n2,2 4,4 6,6 8,8 8,2\n0 1 N\nR R M M M \nL M M M M M M M M M\n";

            File.WriteAllText(_configFileName, configFileString);

            Assert.That(() =>
            {
                _configuration.Load(_configFileName);
            },
            Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void TestMissingMines()
        {
            var configFileString = "10 10\n\n9 4\n0 1 N\nR R M M M L M M M M M M M M M\n";

            File.WriteAllText(_configFileName, configFileString);
            _configuration.Load(_configFileName);
            Assert.That(RunGame()[0] == ResultStates.Success);
        }

    }
}