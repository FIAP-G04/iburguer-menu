using iBurguer.Menu.Infrastructure.MongoDB.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace iBurguer.Menu.Infrastructure.MongoDb.Serializers;

public static class MongoDbSerializers
{
    public static void Register()
    {
        BsonSerializer.TryRegisterSerializer(new ObjectSerializer(ObjectSerializer.AllAllowedTypes));
        BsonSerializer.TryRegisterSerializer(GuidSerializer.StandardInstance);
        BsonSerializer.TryRegisterSerializer(new PriceSerializer());
        BsonSerializer.TryRegisterSerializer(new CategorySerializer());
        BsonSerializer.TryRegisterSerializer(new PreparationTimeSerializer());
        BsonSerializer.TryRegisterSerializer(new UrlSerializer());
    }
}