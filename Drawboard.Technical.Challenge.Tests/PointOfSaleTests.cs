using Drawboard.Technical.Challenge.Exceptions;
using Drawboard.Technical.Challenge.Models;
using Drawboard.Technical.Challenge.Services;
using Drawboard.Technical.Challenge.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace Drawboard.Technical.Challenge.Tests
{
    public class PointOfSaleTests
    {
        [Theory]
        [InlineData("ABCDABA", 13.25)]
        [InlineData("CCCCCCC", 6)]
        [InlineData("ABCD", 7.25)]
        public void GetTotal_MultipleValidProducts_GetsTotals(string products, decimal expectedTotal)
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();
            posTerminal.SetPricing(BaselineProvider.GetProducts());

            // Act
            posTerminal.Scan(products);

            //Asset
            Assert.Equal(expectedTotal, posTerminal.GetTotal());
        }

        [Theory]
        [InlineData("ABBCADA",'A', 3)]
        [InlineData("BBBDDBB", 'B', 5)]
        [InlineData("BBBDDBB", 'D', 2)]
        public void Scan_MultipleValidProducts_AddsItems(string products, char code, int expectedCount)
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();
            posTerminal.SetPricing(BaselineProvider.GetProducts());

            // Act
            posTerminal.Scan(products);

            //Asset
            Assert.True(posTerminal.GetScannedProducts().TryGetValue(code, out var count));
            Assert.Equal(expectedCount, count);
        }

        [Theory]
        [InlineData('A')]
        [InlineData('B')]
        [InlineData('C')]
        [InlineData('D')]
        public void Scan_SingleValidProduct_AddsItem(char code)
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();
            posTerminal.SetPricing(BaselineProvider.GetProducts());

            // Act
            posTerminal.Scan(code);

            //Asset
            Assert.True(posTerminal.GetScannedProducts().TryGetValue(code, out var count));
            Assert.Equal(1, count);
        }

        [Theory]
        [InlineData("ABCCDF")]
        [InlineData("aabcd")]
        [InlineData(" ")]
        [InlineData("AAA BB")]
        public void Scan_MultipleInvalidProduct_ThrowsProductNotFound(string products)
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();
            posTerminal.SetPricing(BaselineProvider.GetProducts());

            // Act & Assert
            Assert.Throws<ProductNotFoundException>(() => posTerminal.Scan(products));
        }

        [Theory]
        [InlineData(' ')]
        [InlineData('E')]
        [InlineData('F')]
        [InlineData(null)]
        public void Scan_SingleInvalidProduct_ThrowsProductNotFound(char code)
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();
            posTerminal.SetPricing(BaselineProvider.GetProducts());

            // Act & Assert
            Assert.Throws<ProductNotFoundException>(() => posTerminal.Scan(code));
        }

        [Fact]
        public void SetPricing_AddsProducts()
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();
            var baseProducts = BaselineProvider.GetProducts();

            // Act
            posTerminal.SetPricing(baseProducts);

            // Assert
            Assert.Equal(posTerminal.GetDefinedProducts(), baseProducts);
        }

        [Fact]
        public void SetPricing_EmptyProducts_ThrowsNotConfigured()
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();

            // Act & Assert
            Assert.Throws<ProductsNotConfiguredException>(() => posTerminal.SetPricing(new List<Product>()));
        }

        [Fact]
        public void SetPricing_ProductsNull_ThrowsNotConfigured()
        {
            // Arrange
            var posTerminal = new PointOfSaleTerminal();

            // Act & Assert
            Assert.Throws<ProductsNotConfiguredException>(() => posTerminal.SetPricing(null));
        }
    }
}
