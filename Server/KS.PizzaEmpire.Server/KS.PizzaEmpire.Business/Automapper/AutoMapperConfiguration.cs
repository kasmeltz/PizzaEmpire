namespace KS.PizzaEmpire.Business.Automapper
{
    using AutoMapper;
    using Business.ProtoSerializable;
    using Business.TableStorage;
    using Common.BusinessObjects;
    using KS.PizzaEmpire.Business.Cache;
    using ProtoBuf;
    using System.Collections.Generic;
    using System.IO;

    public class ProtoSerializerConverter<T,K> : ITypeConverter<T, byte[]>
    {
        public byte[] Convert(ResolutionContext context)
        {
            T businessObject = (T)context.SourceValue;
            K serializedItem = Mapper
                .Map<T,K>(businessObject);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, serializedItem);
                return memoryStream.ToArray();
            }
        }
    }

    public class ProtoDeserializerConverter<T,K> : ITypeConverter<byte[], K>
    {
        public K Convert(ResolutionContext context)
        {
            byte[] bytes = (byte[])context.SourceValue;
            T serializedItem;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                serializedItem = Serializer
                    .Deserialize<T>(memoryStream);
            }

            return Mapper
                .Map<T, K>(serializedItem);
        }
    }
    
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            // *************************** TO / FROM BYTE ARRAY ***************************
            Mapper.CreateMap<List<BuildableItemStat>, byte[]>()
                .ConvertUsing(new 
                    ProtoSerializerConverter<List<BuildableItemStat>, 
                        List<BuildableItemStatProtoSerializable>>());

            Mapper.CreateMap<byte[], List<BuildableItemStat>>()
                .ConvertUsing(new
                    ProtoDeserializerConverter<List<BuildableItemStatProtoSerializable>,
                        List<BuildableItemStat>>());

            Mapper.CreateMap<List<ProductionItemStat>, byte[]>()
                .ConvertUsing(new
                    ProtoSerializerConverter<List<ProductionItemStat>,
                        List<ProductionItemStatProtoSerializable>>());

            Mapper.CreateMap<byte[], List<ProductionItemStat>>()
                .ConvertUsing(new
                    ProtoDeserializerConverter<List<ProductionItemStatProtoSerializable>,
                        List<ProductionItemStat>>());

            Mapper.CreateMap<List<ConsumableItemStat>, byte[]>()
                .ConvertUsing(new
                    ProtoSerializerConverter<List<ConsumableItemStat>,
                        List<ConsumableItemStatProtoSerializable>>());

            Mapper.CreateMap<byte[], List<ConsumableItemStat>>()
                .ConvertUsing(new
                    ProtoDeserializerConverter<List<ConsumableItemStatProtoSerializable>,
                        List<ConsumableItemStat>>());

            Mapper.CreateMap<List<WorkItemStat>, byte[]>()
                .ConvertUsing(new
                    ProtoSerializerConverter<List<WorkItemStat>,
                        List<WorkItemStatProtoSerializable>>());

            Mapper.CreateMap<byte[], List<WorkItemStat>>()
                .ConvertUsing(new
                    ProtoDeserializerConverter<List<WorkItemStatProtoSerializable>,
                        List<WorkItemStat>>());

            Mapper.CreateMap<List<StorageItemStat>, byte[]>()
                .ConvertUsing(new
                    ProtoSerializerConverter<List<StorageItemStat>,
                        List<StorageItemStatProtoSerializable>>());

            Mapper.CreateMap<byte[], List<StorageItemStat>>()
                .ConvertUsing(new
                    ProtoDeserializerConverter<List<StorageItemStatProtoSerializable>,
                        List<StorageItemStat>>());

            Mapper.CreateMap<List<BusinessLocation>, byte[]>()
                .ConvertUsing(new
                    ProtoSerializerConverter<List<BusinessLocation>,
                        List<BusinessLocationProtoSerializable>>());

            Mapper.CreateMap<byte[], List<BusinessLocation>>()
                .ConvertUsing(new
                    ProtoDeserializerConverter<List<BusinessLocationProtoSerializable>,
                        List<BusinessLocation>>());

            Mapper.CreateMap<List<WorkInProgress>, byte[]>()
                            .ConvertUsing(new
                                ProtoSerializerConverter<List<WorkInProgress>,
                                    List<WorkInProgressProtoSerializable>>());

            Mapper.CreateMap<byte[], List<WorkInProgress>>()
                .ConvertUsing(new
                    ProtoDeserializerConverter<List<WorkInProgressProtoSerializable>,
                        List<WorkInProgress>>());
            // *************************** TO / FROM BYTE ARRAY ***************************

            // *************************** PROTO SERIALIZABLE ***************************
            Mapper.CreateMap<ItemQuantity, ItemQuantityProtoSerializable>();
            Mapper.CreateMap<BuildableItemStat, BuildableItemStatProtoSerializable>();
            Mapper.CreateMap<ProductionItemStat, ProductionItemStatProtoSerializable>();
            Mapper.CreateMap<ConsumableItemStat, ConsumableItemStatProtoSerializable>();
            Mapper.CreateMap<WorkItemStat, WorkItemStatProtoSerializable>();
            Mapper.CreateMap<StorageItemStat, StorageItemStatProtoSerializable>();
            Mapper.CreateMap<BusinessLocation, BusinessLocationProtoSerializable>();
            Mapper.CreateMap<LocationStorage, LocationStorageProtoSerializable>();
            Mapper.CreateMap<WorkInProgress, WorkInProgressProtoSerializable>();

            Mapper.CreateMap<ItemQuantityProtoSerializable, ItemQuantity>();
            Mapper.CreateMap<BuildableItemStatProtoSerializable, BuildableItemStat>();
            Mapper.CreateMap<ProductionItemStatProtoSerializable, ProductionItemStat>();
            Mapper.CreateMap<ConsumableItemStatProtoSerializable, ConsumableItemStat>();
            Mapper.CreateMap<WorkItemStatProtoSerializable, WorkItemStat>();
            Mapper.CreateMap<StorageItemStatProtoSerializable, StorageItemStat>();
            Mapper.CreateMap<BusinessLocationProtoSerializable, BusinessLocation>();
            Mapper.CreateMap<LocationStorageProtoSerializable, LocationStorage>();
            Mapper.CreateMap<WorkInProgressProtoSerializable, WorkInProgress>();
            // *************************** PROTO SERIALIZABLE ***************************

            // *************************** TABLE STORAGE ***************************
            // Buildable Item Types
            Mapper.CreateMap<ProductionItem, BuildableItemTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore())
                .ForMember(bts => bts.StorageStats, opt => opt.Ignore())
                .ForMember(bts => bts.WorkStats, opt => opt.Ignore())
                .ForMember(bts => bts.ConsumableStats, opt => opt.Ignore())
                .ForMember(bts => bts.ProducedWith, opt => opt.Ignore())
                .ForMember(bts => bts.StoredIn, opt => opt.Ignore())
                .ForMember(bts => bts.Category, opt => opt.UseValue<int>((int)BuildableItemCategory.Production));
            Mapper.CreateMap<BuildableItemTableStorage, ProductionItem>();

            Mapper.CreateMap<StorageItem, BuildableItemTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore())
                .ForMember(bts => bts.ProductionStats, opt => opt.Ignore())
                .ForMember(bts => bts.WorkStats, opt => opt.Ignore())
                .ForMember(bts => bts.ConsumableStats, opt => opt.Ignore())
                .ForMember(bts => bts.ProducedWith, opt => opt.Ignore())
                .ForMember(bts => bts.StoredIn, opt => opt.Ignore())
                .ForMember(bts => bts.Category, opt => opt.UseValue<int>((int)BuildableItemCategory.Storage));
            Mapper.CreateMap<BuildableItemTableStorage, StorageItem>();

            Mapper.CreateMap<WorkItem, BuildableItemTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore())
                .ForMember(bts => bts.ProductionStats, opt => opt.Ignore())
                .ForMember(bts => bts.ConsumableStats, opt => opt.Ignore())
                .ForMember(bts => bts.StorageStats, opt => opt.Ignore())
                .ForMember(bts => bts.ProducedWith, opt => opt.Ignore())
                .ForMember(bts => bts.StoredIn, opt => opt.Ignore())
                .ForMember(bts => bts.Category, opt => opt.UseValue<int>((int)BuildableItemCategory.Work));
            Mapper.CreateMap<BuildableItemTableStorage, WorkItem>();

            Mapper.CreateMap<ConsumableItem, BuildableItemTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore())
                .ForMember(bts => bts.ProductionStats, opt => opt.Ignore())
                .ForMember(bts => bts.StorageStats, opt => opt.Ignore())
                .ForMember(bts => bts.WorkStats, opt => opt.Ignore())
                .ForMember(bts => bts.Category, opt => opt.UseValue<int>((int)BuildableItemCategory.Consumable));
            Mapper.CreateMap<BuildableItemTableStorage, ConsumableItem>();

            // Experience Levels
            Mapper.CreateMap<ExperienceLevel, ExperienceLevelTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore());

            Mapper.CreateMap<ExperienceLevelTableStorage, ExperienceLevel>();

            // Game Player
            Mapper.CreateMap<GamePlayer, GamePlayerTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore());

            Mapper.CreateMap<GamePlayerTableStorage, GamePlayer>()
                .ForMember(bts => bts.StateChanged, opt => opt.Ignore());
            // *************************** TABLE STORAGE ***************************

            // *************************** CACHEABLE ***************************
            Mapper.CreateMap<GamePlayer, GamePlayerCacheable>();
            Mapper.CreateMap<GamePlayerCacheable, GamePlayer>();
            // *************************** CACHEABLE ***************************
        }
    }
}
