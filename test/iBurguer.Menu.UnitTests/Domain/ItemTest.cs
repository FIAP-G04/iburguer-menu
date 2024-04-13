using FluentAssertions;
using FluentAssertions.Execution;
using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.UnitTests.Domain
{
    public class ItemTest
    {
        [Fact]
        public void ShouldRaiseEventWhenPriceUpdated()
        {
            var oldPrice = new Price(1);

            var item = new Item(
                "name", 
                "description", 
                oldPrice, 
                Category.Drink, 
                10, 
                new List<Url>() 
                { 
                    new("https://img.url.com") 
                });

            item.Update(
                item.Name, item.Description, new(2), item.Category, 
                item.PreparationTime.Minutes, item.Images);

            item.Events.Should().NotBeNullOrEmpty();
            item.Events.First().Should().BeAssignableTo<MenuItemPriceUpdated>();

            var priceEvent = item.Events.First() as MenuItemPriceUpdated;

            priceEvent.Should().NotBeNull();
            priceEvent.NewPrice.Should().Be(item.Price);
            priceEvent.OldPrice.Should().Be(oldPrice);
            priceEvent.ProductId.Should().Be(item.Id);
        }

        [Fact]
        public void ShouldNotRaiseEventWhenPriceDidntChange()
        {
            var oldPrice = new Price(1);

            var item = new Item(
                "name",
                "description",
                oldPrice,
                Category.Drink,
                10,
                new List<Url>()
                {
                    new("https://img.url.com")
                });

            item.Update(
                item.Name, item.Description, oldPrice, item.Category,
                item.PreparationTime.Minutes, item.Images);

            item.Events.Should().BeEmpty();
        }

        [Fact]
        public void ShouldUpdateItem()
        {
            var item = new Item(
                "oldName", "oldDescription", new Price(2),
                Category.Dessert, 11, new List<Url>()
                {
                    new("https://img.old.url.com")
                });

            var previousUpdatedAt = item.UpdatedAt;

            var newName = "name";
            var newDescription = "description";
            var newPrice = new Price(1);
            var newCategory = Category.Drink;
            ushort newPreparationTime = 10;
            var newImages = new List<Url>()
                {
                    new("https://img.url.com")
                };

            item.Update(
                newName, newDescription, newPrice, 
                newCategory, newPreparationTime, newImages);

            item.Name.Should().Be(newName);
            item.Description.Should().Be(newDescription);
            item.Price.Should().Be(newPrice);
            item.Category.Should().Be(newCategory);
            item.PreparationTime.Minutes.Should().Be(newPreparationTime);
            item.Images.Should().BeEquivalentTo(newImages);
            item.UpdatedAt.Should().NotBeNull();
            item.UpdatedAt.Should().BeAfter(previousUpdatedAt!.Value);
        }

        [Fact]
        public void ShouldEnableProduct()
        {
            var item = new Item(
                "oldName", "oldDescription", new Price(2),
                Category.Dessert, 11, new List<Url>()
                {
                    new("https://img.old.url.com")
                });

            var previousUpdatedAt = item.UpdatedAt;

            item.Enable();

            item.Enabled.Should().BeTrue();
            item.UpdatedAt.Should().NotBeNull();
            item.UpdatedAt.Should().BeAfter(previousUpdatedAt!.Value);
        }

        [Fact]
        public void ShouldDisableProduct()
        {
            
            var item = new Item(
                "oldName", "oldDescription", new Price(2),
                Category.Dessert, 11, new List<Url>()
                {
                    new("https://img.old.url.com")
                });
            
            var previousUpdatedAt = item.UpdatedAt;

            item.Disable();

            item.Enabled.Should().BeFalse();
            item.UpdatedAt.Should().NotBeNull();
            item.UpdatedAt.Should().BeAfter(previousUpdatedAt!.Value);
        }
    }
}
