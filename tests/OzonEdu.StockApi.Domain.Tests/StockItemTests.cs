using System;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using OzonEdu.StockApi.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace OzonEdu.StockApi.Domain.Tests
{
    public class StockItemTests
    {
        [Fact]
        public void IncreaseStockItemQuantity()
        {
            //Arrange 
            var stockItem = new StockItem(
                new Sku(149568),
                new Name("Super puper TShirt"),
                new Item(ItemType.TShirt),
                ClothingSize.S,
                new Quantity(10),
                new QuantityValue(5));

            var valueToIncrease = 10;
        
            //Act
            stockItem.IncreaseQuantity(valueToIncrease);
        
            //Assert
            Assert.Equal(20, stockItem.Quantity.Value);
        }

        [Fact]
        public void IncreaseQuantityNegativeValueSuccess()
        {
            //Arrange 
            //Arrange 
            var stockItem = new StockItem(
                new Sku(149568),
                new Name("Super puper TShirt"),
                new Item(ItemType.TShirt),
                ClothingSize.S,
                new Quantity(10),
                new QuantityValue(5));
            var valueToIncrease = -10;
            //Act
            
            //Assert
            Assert.Throws<Exception>(() => stockItem.IncreaseQuantity(valueToIncrease));
        }
    }
}