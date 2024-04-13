using iBurguer.Menu.Core.Domain;
using MongoDB.Bson.Serialization;

namespace iBurguer.Menu.Infrastructure.MongoDB.Serializers;

public class PriceSerializer : IBsonSerializer<Price>
{
    public Type ValueType => typeof(Price);
    
    public Price Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = Convert.ToDecimal(context.Reader.ReadDouble());
        return new Price(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Price value)
    {
        context.Writer.WriteDouble(Convert.ToDouble(value.Amount));
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is Price price)
        {
            Serialize(context, args, (Price)value);
        }
        else
        {
            throw new NotSupportedException("This is invalid price");
        }
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }
}