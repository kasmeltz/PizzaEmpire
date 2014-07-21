using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.Services.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services.Test.Storage
{
    /// <summary>
    /// Simple data class that will be used to test the
    /// AzureTableStorage class.
    /// </summary>
    public class LucifureTableStorageTestEntity : TableEntity, ITableStorageEntity
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

    /// <summary>
    /// Unit tests for the AzureTableStorage class
    /// </summary>
    [TestClass]
    public class LucifureTableStoarageTest
    {
        protected LucifureTableStorage Storage;

        [TestInitialize]
        public void TestInitializeMethod()
        {
            Task.WaitAll(TestInitializeMethodAsync());
        }

        private async Task TestInitializeMethodAsync()
        {
            Storage = new LucifureTableStorage();
        }

        [TestMethod]
        public async Task TestAzureTableStorageGet()
        {
            // Arrange
            await Storage.Insert<LucifureTableStorageTestEntity>(new LucifureTableStorageTestEntity
            { 
                RowKey = "Kevin", 
                Name = "Kevin"
            });

            // Act
            LucifureTableStorageTestEntity player = await Storage.Get<LucifureTableStorageTestEntity>("Ke", "Kevin");

            // Assert
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Kevin", player.Name);
        }

        /*

        [TestMethod]
        public async Task TestAzureTableStorageGetAll()
        {
            // Arrange
            await Storage.Insert<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            await Storage.Insert<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.Insert<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });

            // Act
            List<AzureTableStorageTestEntity> players = (await Storage.GetAll<AzureTableStorageTestEntity>("Ka")).ToList();

            // Assert
            Assert.AreEqual(players.Count, 2);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (AzureTableStorageTestEntity pl in players)
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
            List<AzureTableStorageTestEntity> players = new List<AzureTableStorageTestEntity>();          
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });

            // Act
            await Storage.Insert<AzureTableStorageTestEntity>(players);

            // Assert
            players = (await Storage.GetAll<AzureTableStorageTestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (AzureTableStorageTestEntity pl in players)
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
            await Storage.Insert<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1                
            });

            // Act
            await Storage.Replace<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 2
            });
            AzureTableStorageTestEntity player = await Storage.Get<AzureTableStorageTestEntity>("Ke", "Kevin");

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
            List<AzureTableStorageTestEntity> players = new List<AzureTableStorageTestEntity>();
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.Insert<AzureTableStorageTestEntity>(players);

            players = new List<AzureTableStorageTestEntity>();
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy"
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.Replace<AzureTableStorageTestEntity>(players);

            // Assert
            players = (await Storage.GetAll<AzureTableStorageTestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (AzureTableStorageTestEntity pl in players)
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
            await Storage.Insert<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.Merge<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Keith"
            });
            AzureTableStorageTestEntity player = await Storage.Get<AzureTableStorageTestEntity>("Ke", "Kevin");

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
            List<AzureTableStorageTestEntity> players = new List<AzureTableStorageTestEntity>();
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy",
                Number = 2
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Karen",
                Number = 3
            });
            await Storage.Insert<AzureTableStorageTestEntity>(players);

            players = new List<AzureTableStorageTestEntity>();
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy",
                Number = 5
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.Merge<AzureTableStorageTestEntity>(players);

            // Assert
            players = (await Storage.GetAll<AzureTableStorageTestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, AzureTableStorageTestEntity> ps = new Dictionary<string, AzureTableStorageTestEntity>();
            foreach (AzureTableStorageTestEntity pl in players)
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
            await Storage.InsertOrReplace<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.InsertOrReplace<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 2
            });
            AzureTableStorageTestEntity player = await Storage.Get<AzureTableStorageTestEntity>("Ke", "Kevin");

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
            List<AzureTableStorageTestEntity> players = new List<AzureTableStorageTestEntity>();
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.InsertOrReplace<AzureTableStorageTestEntity>(players);

            players.Clear();

            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy"
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.InsertOrReplace<AzureTableStorageTestEntity>(players);

            // Assert
            players = (await Storage.GetAll<AzureTableStorageTestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, bool> ps = new Dictionary<string, bool>();
            foreach (AzureTableStorageTestEntity pl in players)
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
            await Storage.InsertOrMerge<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.InsertOrMerge<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Keith"
            });
            AzureTableStorageTestEntity player = await Storage.Get<AzureTableStorageTestEntity>("Ke", "Kevin");

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
            List<AzureTableStorageTestEntity> players = new List<AzureTableStorageTestEntity>();
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy",
                Number = 2
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Karen",
                Number = 3
            });
            await Storage.InsertOrMerge<AzureTableStorageTestEntity>(players);

            players = new List<AzureTableStorageTestEntity>();
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy",
                Number = 5
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.InsertOrMerge<AzureTableStorageTestEntity>(players);

            // Assert
            players = (await Storage.GetAll<AzureTableStorageTestEntity>("Ka")).ToList();
            Assert.AreEqual(2, players.Count);

            Dictionary<string, AzureTableStorageTestEntity> ps = new Dictionary<string, AzureTableStorageTestEntity>();
            foreach (AzureTableStorageTestEntity pl in players)
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
            await Storage.Insert<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin",
                Number = 1
            });

            // Act
            await Storage.Delete<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Keith",
                Number = 2
            });
            AzureTableStorageTestEntity player = await Storage.Get<AzureTableStorageTestEntity>("Ke", "Kevin");

            // Assert
            Assert.IsNull(player);
        }

        [TestMethod]
        public async Task TestAzureTableStorageDeleteMultiple()
        {
            // Arrange
            List<AzureTableStorageTestEntity> players = new List<AzureTableStorageTestEntity>();
            await Storage.InsertOrReplace<AzureTableStorageTestEntity>(new AzureTableStorageTestEntity
            {
                RowKey = "Kevin",
                Name = "Kevin"
            });
            
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kathy"
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Karen"
            });
            await Storage.InsertOrReplace<AzureTableStorageTestEntity>(players);

            players = new List<AzureTableStorageTestEntity>();           
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Kathy",
                Name = "Kandy"
            });
            players.Add(new AzureTableStorageTestEntity
            {
                RowKey = "Karen",
                Name = "Kamala"
            });

            // Act
            await Storage.Delete<AzureTableStorageTestEntity>(players);

            // Assert
            AzureTableStorageTestEntity player = await Storage.Get<AzureTableStorageTestEntity>("Ke", "Kevin");
            Assert.AreEqual("Ke", player.PartitionKey);
            Assert.AreEqual("Kevin", player.RowKey);
            Assert.AreEqual("Kevin", player.Name);

            players = (await Storage.GetAll<AzureTableStorageTestEntity>("Ka")).ToList();
            Assert.AreEqual(0, players.Count);
        }
         * */
    }
}
