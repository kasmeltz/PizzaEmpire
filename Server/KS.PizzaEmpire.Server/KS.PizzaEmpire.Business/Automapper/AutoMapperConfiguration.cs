namespace KS.PizzaEmpire.Business.Automapper
{
    using AutoMapper;
    using Business.ProtoSerializable;
    using Business.TableStorage;
    using Common.BusinessObjects;
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
            // Serialized classes
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

            // Serializable classes
            Mapper.CreateMap<ItemQuantity, ItemQuantityProtoSerializable>();
            Mapper.CreateMap<BuildableItemStat, BuildableItemStatProtoSerializable>();
            Mapper.CreateMap<ProductionItemStat, ProductionItemStatProtoSerializable>();
            Mapper.CreateMap<ConsumableItemStat, ConsumableItemStatProtoSerializable>();

            Mapper.CreateMap<ItemQuantityProtoSerializable, ItemQuantity>();
            Mapper.CreateMap<BuildableItemStatProtoSerializable, BuildableItemStat>();
            Mapper.CreateMap<ProductionItemStatProtoSerializable, ProductionItemStat>();
            Mapper.CreateMap<ConsumableItemStatProtoSerializable, ConsumableItemStat>();

            // Buildable Items
            Mapper.CreateMap<BuildableItem, BuildableItemTableStorage>()
                .Include<ProductionItem, ProductionItemTableStorage>()
                .Include<ConsumableItem, ConsumableItemTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore());

            Mapper.CreateMap<BuildableItemTableStorage, BuildableItem>()
                .Include<ProductionItemTableStorage, ProductionItem>();

            // Production Items
            Mapper.CreateMap<ProductionItem, ProductionItemTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore());

            Mapper.CreateMap<ProductionItemTableStorage, ProductionItem>();

            // Consumable Items
            Mapper.CreateMap<ConsumableItem, ConsumableItemTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore());

            Mapper.CreateMap<ConsumableItemTableStorage, ConsumableItem>();

            // Experience Levels
            Mapper.CreateMap<ExperienceLevel, ExperienceLevelTableStorage>()
                .ForMember(bts => bts.PartitionKey, opt => opt.Ignore())
                .ForMember(bts => bts.RowKey, opt => opt.Ignore())
                .ForMember(bts => bts.Timestamp, opt => opt.Ignore())
                .ForMember(bts => bts.ETag, opt => opt.Ignore());

            Mapper.CreateMap<ExperienceLevelTableStorage, ExperienceLevel>();
        }
    }
}
