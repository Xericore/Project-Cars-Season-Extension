using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Serialization;

namespace ProjectCarsSeasonExtensionTests
{
    [TestClass]
    public class ChampionshipTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ISeasonReader seasonReader = new TestSeasonReader();
            var dataView = new DataView(seasonReader);
        }
    }
}
