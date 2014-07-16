using KS.PizzaEmpire.Business;
using KS.PizzaEmpire.Services.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;

namespace KS.PizzaEmpire.Services.Test.Storage
{
    public class TestEntity : TableEntity
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PartitionKey = _name.Substring(0, 2);
            }
        }

        public int? Number { get; set; }
    }

    [TestClass]
    public class TestAzureTableStorage
    {
        protected AzureTableStorage Storage;

        [TestInitialize]
        public void TestInitializeMethod()
        {
            Task.WaitAll(TestInitializeMethodAsync());
        }

        private async Task TestInitializeMethodAsync()
        {
            Storage = new AzureTableStorage(
                "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://127.0.0.1;");
            await Storage.SetTable("players");
            await Storage.DeleteTable();
        }

        [TestMethod]
        public async Task TestAzureTableStorageGet()
        {
            // Arrange
            await Storage.SetTable("players");
            await Storage.Insert<TestEntity>(new TestEntity
            { 
                RowKey = "Kevin", 
                Name = "Kevin"
            });

            // Act
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");

            // Assert
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Kevin", player.Name);
        }

        [TestMethod]
        public async Task TestAzureTableStorageGetAll()
        {
            // Arrange
            await Storage.SetTable("players");
            await Storage.Insert<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            await Storage.Insert<TestEntity>(new TestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.Insert<TestEntity>(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });

            // Act
            List<TestEntity> players = (await Storage.GetAll<TestEntity>("Ka")).ToList();

            // Assert
            Assert.AreEqual(players.Count, 2);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (TestEntity pl in players)
            {
                ps[pl.Name] = true;
            }

            Assert.IsTrue(ps.ContainsKey("Karen"));
            Assert.IsTrue(ps.ContainsKey("Kathy"));
        }

        [TestMethod]
        public async Task TestAzureTableStorageInsertMultiple()
        {
            // Arrange
            await Storage.SetTable("players");
            List<TestEntity> players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });

            // Act
            await Storage.Insert<TestEntity>(players);

            // Assert
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Kevin", player.Name);

            players = (await Storage.GetAll<TestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (TestEntity pl in players)
            {
                ps[pl.Name] = true;
            }

            Assert.IsTrue(ps.ContainsKey("Karen"));
            Assert.IsTrue(ps.ContainsKey("Kathy"));
        }

        [TestMethod]
        public async Task TestAzureTableStorageReplace()
        {
            // Arrange
            await Storage.SetTable("players");
            await Storage.Insert<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1                
            });

            // Act
            await Storage.Replace<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 2
            });
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");

            // Assert
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);
            Assert.AreEqual(2, player.Number);
        }

        [TestMethod]
        public async Task TestAzureTableStorageReplaceMultiple()
        {
            // Arrange
            await Storage.SetTable("players");
            List<TestEntity> players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.Insert<TestEntity>(players);

            players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith"
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy"
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.Replace<TestEntity>(players);

            // Assert
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);

            players = (await Storage.GetAll<TestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (TestEntity pl in players)
            {
                ps[pl.Name] = true;
            }

            Assert.IsTrue(ps.ContainsKey("Kandy"));
            Assert.IsTrue(ps.ContainsKey("Kamala"));
        }

        [TestMethod]
        public async Task TestAzureTableStorageMerge()
        {
            // Arrange
            await Storage.SetTable("players");
            await Storage.Insert<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.Merge<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith"
            });
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");

            // Assert
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);
            Assert.AreEqual(1, player.Number);
        }

        [TestMethod]
        public async Task TestAzureTableStorageMergeMultiple()
        {
            // Arrange
            await Storage.SetTable("players");
            List<TestEntity> players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy",
                Number = 2
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Karen",
                Number = 3
            });
            await Storage.Insert<TestEntity>(players);

            players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 4
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy",
                Number = 5
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.Merge<TestEntity>(players);

            // Assert
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);
            Assert.AreEqual(4, player.Number);

            players = (await Storage.GetAll<TestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, TestEntity> ps = new Dictionary<string, TestEntity>();
            foreach (TestEntity pl in players)
            {
                ps[pl.Name] = pl;
            }

            Assert.IsTrue(ps.ContainsKey("Kandy"));
            Assert.IsTrue(ps.ContainsKey("Kamala"));

            Assert.AreEqual(5, ps["Kandy"].Number);
            Assert.AreEqual(3, ps["Kamala"].Number);
        }

        [TestMethod]
        public async Task TestAzureTableStorageInsertOrReplace()
        {
            // Arrange
            await Storage.SetTable("players");
            await Storage.InsertOrReplace<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.InsertOrReplace<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 2
            });
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");

            // Assert
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);
            Assert.AreEqual(2, player.Number);
        }

        [TestMethod]
        public async Task TestAzureTableStorageInsertOrReplaceMultiple()
        {
            // Arrange
            await Storage.SetTable("players");
            List<TestEntity> players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.InsertOrReplace<TestEntity>(players);

            players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith"
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy"
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.InsertOrReplace<TestEntity>(players);

            // Assert
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);

            players = (await Storage.GetAll<TestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (TestEntity pl in players)
            {
                ps[pl.Name] = true;
            }

            Assert.IsTrue(ps.ContainsKey("Kandy"));
            Assert.IsTrue(ps.ContainsKey("Kamala"));
        }

        [TestMethod]
        public async Task TestAzureTableStorageInsertOrMerge()
        {
            // Arrange
            await Storage.SetTable("players");
            await Storage.InsertOrMerge<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.InsertOrMerge<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith"
            });
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");

            // Assert
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);
            Assert.AreEqual(1, player.Number);
        }

        [TestMethod]
        public async Task TestAzureTableStorageInsertOrMergeMultiple()
        {
            // Arrange
            await Storage.SetTable("players");
            List<TestEntity> players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy",
                Number = 2
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Karen",
                Number = 3
            });
            await Storage.InsertOrMerge<TestEntity>(players);

            players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 4
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy",
                Number = 5
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.InsertOrMerge<TestEntity>(players);

            // Assert
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Keith", player.Name);
            Assert.AreEqual(4, player.Number);

            players = (await Storage.GetAll<TestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, TestEntity> ps = new Dictionary<string, TestEntity>();
            foreach (TestEntity pl in players)
            {
                ps[pl.Name] = pl;
            }

            Assert.IsTrue(ps.ContainsKey("Kandy"));
            Assert.IsTrue(ps.ContainsKey("Kamala"));

            Assert.AreEqual(5, ps["Kandy"].Number);
            Assert.AreEqual(3, ps["Kamala"].Number);
        }

        [TestMethod]
        public async Task TestAzureTableStorageDelete()
        {
            // Arrange
            await Storage.SetTable("players");
            await Storage.Insert<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.Delete<TestEntity>(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 2
            });
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");

            // Assert
            Assert.IsNull(player);
        }

        [TestMethod]
        public async Task TestAzureTableStorageDeleteMultiple()
        {
            // Arrange
            await Storage.SetTable("players");
            List<TestEntity> players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.InsertOrReplace<TestEntity>(players);

            players = new List<TestEntity>();
            players.Add(new TestEntity
            {
                RowKey = "Kevin",
                Name = "Keith"
            });
            players.Add(new TestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy"
            });
            players.Add(new TestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.Delete<TestEntity>(players);

            // Assert
            TestEntity player = await Storage.Get<TestEntity>("Ke", "Kevin");
            Assert.IsNull(player);

            players = (await Storage.GetAll<TestEntity>("Ka")).ToList();
            Assert.AreEqual(0, players.Count);
        }
    }
}
