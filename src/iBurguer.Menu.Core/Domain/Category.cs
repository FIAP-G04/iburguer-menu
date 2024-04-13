using System.Reflection;
using static iBurguer.Menu.Core.Exceptions;

namespace iBurguer.Menu.Core.Domain;

public class Category
{
    public static readonly Category MainDish = new(1, "MainDish");
    public static readonly Category SideDish = new(2, "SideDish");
    public static readonly Category Drink = new(3, "Drink");
    public static readonly Category Dessert = new(4, "Dessert");
    
    private readonly int _id;
    private readonly string _name;

    public Category(int id, string name)
    {
        _id = id;
        _name = name;
    }
    
    public override string ToString() => _name;
    
    public int Id() => _id;
    
    public static Category FromName(string name)
    {
        return FindCategory(category => category._name == name);
    }
    
    public static Category FromId(int id)
    {
        return FindCategory(category => category._id == id);
    }

    public static implicit operator Category(int value)
    {
        return FindCategory(category => category._id == value);
    }
    
    private static Category FindCategory(Func<Category, bool> predicate)
    {
        Type type = typeof(Category);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        var category = fields.FirstOrDefault(field =>
        {
            if (field.FieldType == typeof(Category))
            {
                Category category = (Category)field.GetValue(null);
                return predicate(category);
            }
            return false;
        })?.GetValue(null) as Category;

        InvalidCategory.ThrowIfNull(category);
        
        return category;
    }
    
    public static IEnumerable<string> ToList()
    {
        Type type = typeof(Category);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        return fields.Where(f => f.FieldType == typeof(Category)).Select(f => ((Category)f.GetValue(null)!)?._name).ToList();
    }
}