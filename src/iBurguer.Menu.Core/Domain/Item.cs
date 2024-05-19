using iBurguer.Menu.Core.Abstractions;

namespace iBurguer.Menu.Core.Domain;

public class Item : Entity<Id>, IAggregateRoot
{
    #region Attributes

    private IList<Url> _images;
    private Price _price;

    #endregion Attributes


    #region Properties

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Price Price
    {
        get => _price;
        private set
        {
            if (_price != null && _price != value)
            {
                RaiseEvent(new MenuItemPriceUpdated(Id, value, _price));
            }

            _price = value;
        }
    }

    public IReadOnlyCollection<Url> Images
    {
        get => _images.AsReadOnly();
        private set
        {
            _images = value.ToArray();
        }
    }

    public Category Category { get; private set; }

    public PreparationTime PreparationTime { get; private set; }

    public bool Enabled { get; private set; } = true;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    #endregion Properties


    #region Constructors

    public Item(string name, string description, Price price, Category category,
        ushort preparationTime, IEnumerable<Url> images)
    {
        Id = Id.New();
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        PreparationTime = preparationTime;
        CreatedAt = DateTime.Now;
        _images = images.ToList();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    #endregion Constructors


    #region Methods

    public void Update(string name, string description, Price price, Category category,
        ushort preparationTime, IEnumerable<Url> images)
    {
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        PreparationTime = preparationTime;
        UpdatedAt = DateTime.Now;
        _images = images.ToList();
        UpdatedAt = DateTime.Now;
    }

    public void Enable()
    {
        Enabled = true;
        UpdatedAt = DateTime.Now;
    }

    public void Disable()
    {
        Enabled = false;
        UpdatedAt = DateTime.Now;
    }

    #endregion Methods
}