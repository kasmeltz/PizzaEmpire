namespace KS.PizzaEmpire.Business.StorageInformation
{
    using Common;
    using KS.PizzaEmpire.Business.Cache;
    using KS.PizzaEmpire.Business.ProtoSerializable;
    using KS.PizzaEmpire.Business.TableStorage;
    using KS.PizzaEmpire.Common.BusinessObjects;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents in item that contains information about storing a
    /// BuildableItem entity in various types of storage.
    /// </summary>
    public class BuildableItemStorageInformation : BaseStorageInformation
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public BuildableItemStorageInformation(string uniqueKey)
            : base(uniqueKey)
        {
            TableName = "BuildableItem";
            PartitionKey = "Version" + Constants.APPLICATION_VERSION;
            RowKey = uniqueKey;
            CacheKey = "BI_" + uniqueKey;
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ICacheEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ICacheEntity ToCache(IBusinessObjectEntity item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Translates an ICacheEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromCache(ICacheEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the info from a BuildableItemTableStorage to a BuildableItem table storage
        /// </summary>
        /// <param name="clone"></param>
        /// <param name="other"></param>
        public void Copy(BuildableItem clone, BuildableItemTableStorage other)
        {
            clone.ItemCode = (BuildableItemEnum)other.ItemCode;
            List<BuildableItemStatProtoSerializable> stats = null;
            using (MemoryStream memoryStream = new MemoryStream(other.Stats))
            {
                stats = Serializer.Deserialize<List<BuildableItemStatProtoSerializable>>(memoryStream);
            }
            clone.Stats = BuildableItemStatProtoSerializable.ToBusiness(stats);
        }

        /// <summary>
        /// Copies the info from a BuildableItem to a BuildableItemTableStorage table storage
        /// </summary>
        /// <param name="clone"></param>
        /// <param name="other"></param>
        public void Copy(BuildableItemTableStorage clone, BuildableItem other)
        {
            clone.ItemCode = (int)other.ItemCode;
            List<BuildableItemStatProtoSerializable> stats = 
                BuildableItemStatProtoSerializable.FromBusiness(other.Stats);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, stats);
                clone.Stats = memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Translates an ITableStorageEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromTableStorage(ITableStorageEntity entity)
        {
            BuildableItemTableStorage other = entity as BuildableItemTableStorage;
            BuildableItem clone = new BuildableItem();

            Copy(clone, other);

            return clone;
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            BuildableItem other = entity as BuildableItem;
            BuildableItemTableStorage clone = new BuildableItemTableStorage();

            clone.PartitionKey = PartitionKey;
            clone.RowKey = RowKey;

            Copy(clone, other);
            
            return clone;
        }
    }
}
