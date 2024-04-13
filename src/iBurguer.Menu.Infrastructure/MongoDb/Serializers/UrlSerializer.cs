using iBurguer.Menu.Core.Domain;
using MongoDB.Bson.Serialization;

namespace iBurguer.Menu.Infrastructure.MongoDB.Serializers;

public class UrlSerializer : IBsonSerializer<Url>
{
    public Type ValueType => typeof(Url);
    
    public Url Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new Url(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Url value)
    {
        context.Writer.WriteString(value.ToString());
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is Url url)
        {
            Serialize(context, args, (Url)value);
        }
        else
        {
            throw new NotSupportedException("This is invalid url");
        }
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }
}