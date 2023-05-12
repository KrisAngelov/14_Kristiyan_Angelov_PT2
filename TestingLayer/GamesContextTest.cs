using BusinessLayer;
using DataLayer;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TestingLayer
{
    [TestFixture]
    public class GamesContextTest
    {
        private GamesContext context = new(SetupFixture.dbContext);
        private Game game;
        private User u;
        private Genre g;

        [SetUp]
        public void CreateGame()
        {
            game = new("Minecraft");
            g = new("Survival");
            u = new("Todor", "Demirov", 15, "GrosMaistora1234", "1234", "todordemirov.schoolmath.eu");

            game.Genres.Add(g);
            game.Users.Add(u);

            context.Create(game);
        }

        [TearDown]
        public void DropGame()
        {
            foreach (Game item in SetupFixture.dbContext.Games.ToList())
            {
                SetupFixture.dbContext.Games.Remove(item);
            }

            SetupFixture.dbContext.SaveChanges();
        }

        [Test]
        public void Create()
        {
            Game testGame = new("Tetris");
            int gamesBefore = SetupFixture.dbContext.Games.Count();
            context.Create(testGame);
            int gamesAfter = SetupFixture.dbContext.Games.Count();
            Assert.That(gamesBefore + 1 == gamesAfter, "Create() does not work!");
        }

        [Test]
        public void Read()
        {
            Game readGame = context.Read(game.Id);
            Assert.AreEqual(game, readGame, "Read() does not return the same object!");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            Game readGame = context.Read(game.Id, true);
            Assert.That(readGame.Users.Contains(u), "U is not in the Users list!");
            Assert.That(readGame.Genres.Contains(g), "G is not in the Genres list!");
        }

        [Test]
        public void ReadAll()
        {
            List<Game> games = (List<Game>)context.ReadAll();
            Assert.That(games.Count != 0, "ReadAll() does not return games!");
        }

        [Test]
        public void ReadAllWithNavigationalProperties()
        {
            Game readGame = new("Minecraft");
            Genre g1 = new("Action");
            User u1 = new("Kristiyan", "Angelov", 17, "KrisAngelov", "4321", "kristiyanangelov_zh19.schoolmath.eu");
            SetupFixture.dbContext.Users.Add(u);
            SetupFixture.dbContext.Genres.Add(g1);
            SetupFixture.dbContext.Games.Add(readGame);

            List<Game> games = (List<Game>)context.ReadAll(true);
            Assert.That(games.Count != 0 && context.Read(readGame.Id, true).Genres.Count == 1 
                && context.Read(readGame.Id, true).Users.Count == 1, "ReadAll() does not return games!");
        }

        [Test]
        public void Update()
        {
            Game changedGame = context.Read(game.Id);

            changedGame.Name = "Updated " + game.Name;

            context.Update(changedGame);
            game = context.Read(game.Id);

            Assert.AreEqual(changedGame, game, "Update() does not work!");
        }
       
        [Test]
        public void Delete()
        {
            int gamesBefore = SetupFixture.dbContext.Games.Count();
            context.Delete(game.Id);
            int gamesAfter = SetupFixture.dbContext.Games.Count();
            Assert.IsTrue(gamesBefore - 1 == gamesAfter, "Delete() does not work!");
        }
    }
}
