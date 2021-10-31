using System;
using OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate;
using Xunit;

namespace OzonEdu.StockApi.Domain.Tests
{
    public class QuantityValueObjectTests
    {
        [Fact]
        public void CreateQuantityInstanceWithoutMinimalSuccess()
        {
            //Arrange
            var quantity = 10;

            //Act
            var result = new Quantity(quantity);

            //Assert
            Assert.Equal(quantity, result.Value);
        }
    }
}