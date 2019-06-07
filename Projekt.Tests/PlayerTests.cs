using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt;
using System;
using System.Collections.Generic;
namespace Projekt.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TeamNameTrue()
        {
            Team p = new Team("druzyna");
            Assert.AreEqual("druzyna", p.getTeamName());
        }
        [TestMethod]
        public void TeamWinTrue()
        {
            Team p = new Team("druzyna");
            p.addWin();
            p.addWin();
            Assert.AreEqual(2, p.getWins());
        }
        [TestMethod]
        public void TeamSportTrue()
        {
            Team p = new Team("druzyna");
            p.setVolleyball();
            Assert.AreEqual("Volleyball", p.getSport());
        }
        [TestMethod]
        public void CheckNameTrue()
        {
            Team p = new Team("d6-d&6d6d&6");
            bool var = false;
            Assert.AreEqual(var, p.CheckName(p.getTeamName()));
        }
        [TestMethod]
        public void CheckTeamList()
        {
            Team p = new Team("druzyna");
            p.AddPlayer(new Player("daniel", "kaminski"));
            p.AddPlayer(new Player("adam", "dolina"));
            Assert.AreNotEqual(new List<Player>(), p.getPlayers());
        }
        public void RefereesList()
        {
            Referee r = new Referee("daniel", "kaminski");
            Referees referees = new Referees();
            referees.AddRef(r);
            Assert.AreEqual(new List<Referee>(),referees.getRefs());
        }
    }
}
