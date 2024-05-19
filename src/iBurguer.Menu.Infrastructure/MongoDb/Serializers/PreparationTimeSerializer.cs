using iBurguer.Menu.Core.Domain;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace iBurguer.Menu.Infrastructure.MongoDB.Serializers;

[ExcludeFromCodeCoverage]
public class PreparationTimeSerializer : IBsonSerializer<PreparationTime>
{
    public Type ValueType => typeof(PreparationTime);

    public PreparationTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return new PreparationTime((ushort)value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PreparationTime value)
    {
        context.Writer.WriteInt32(value.Minutes);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is PreparationTime time)
        {
            Serialize(context, args, (PreparationTime)value);
        }
        else
        {
            throw new NotSupportedException("This is invalid preparation time");
        }
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }
}