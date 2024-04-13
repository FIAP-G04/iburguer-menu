using iBurguer.Menu.Core.Domain;
using MongoDB.Bson.Serialization;

namespace iBurguer.Menu.Infrastructure.MongoDB.Serializers;

public class CategorySerializer : IBsonSerializer<Category>
{
    public Type ValueType => typeof(Category);
    
    public Category Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadInt32();
        return Category.FromId(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Category value)
    {
        context.Writer.WriteInt32(value.Id());
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is Category category)
        {
            Serialize(context, args, (Category)value);
        }
        else
        {
            throw new NotSupportedException("This is invalid category");
        }
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }
}