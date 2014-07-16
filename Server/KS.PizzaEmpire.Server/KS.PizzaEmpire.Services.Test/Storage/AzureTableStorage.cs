using KS.PizzaEmpire.Business;
using KS.PizzaEmpire.Services.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KS.PizzaEmpire.Services.Test.Storage
{
    [TestClass]
    public class TestAzureTableStorage
    {
        [TestInitialize]
        public void TestInitializeMethod()
        {
            Task.WaitAll(TestInitializeMethodAsync());
        }

        private async Task TestInitializeMethodAsync()
        {
            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("players");
            await storage.DeleteTable();
        }

        [TestMethod]
        public async Task TestAzureTableStorageGet()
        {
            // Arrange
            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("players");
            await storage.Insert<GamePlayer>(new GamePlayer
            { 
                RowKey = "Kevin", 
                Name = "Kevin"
            });

            // Act
            GamePlayer player = await storage.Get<GamePlayer>("Ke", "Kevin");

            // Assert
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Kevin", player.Name);
        }

        [TestMethod]
        public async Task TestAzureTableStorageGetAll()
        {
            // Arrange
            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable("players");
            await storage.Insert<GamePlayer>(new GamePlayer
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            await storage.Insert<GamePlayer>(new GamePlayer
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await storage.Insert<GamePlayer>(new GamePlayer
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });

            // Act
            List<GamePlayer> players = (await storage.GetAll<GamePlayer>("Ka")).ToList();

            // Assert
            Assert.AreEqual(players.Count, 2);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (GamePlayer pl in players)
            {
                ps[pl.Name] = true;
            }

            Assert.IsTrue(ps.ContainsKey("Karen"));
            Assert.IsTrue(ps.ContainsKey("Kathy"));
        }
    }
}
