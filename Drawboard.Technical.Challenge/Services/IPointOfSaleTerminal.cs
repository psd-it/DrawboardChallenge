using Drawboard.Technical.Challenge.Models;
using System.Collections.Generic;

namespace Drawboard.Technical.Challenge.Services
{
    public interface IPointOfSaleTerminal
    {
        /// <summary>
        /// Set the price for each product
        /// </summary>
        /// <param name="products">Product definitions</param>
        /// <exception cref="ProductsNotConfiguredException">Occurs when an empty/null enumerable is passed</exception>
        void SetPricing(IEnumerable<Product> products);

        /// <summary>
        /// Scans a product for purchasing
        /// </summary>
        /// <param name="productCode">Code of the product being scanned</param>
        /// <exception cref="ProductNotFoundException">Thrown when product code doesn't exist</exception>
        void Scan(char productCode);

        /// <summary>
        /// Scans a series of products for purchasing
        /// </summary>
        /// <param name="productCodes">Codes of the product being scanned</param>
        /// <exception cref="ProductNotFoundException">Thrown when product code doesn't exist</exception>
        void Scan(string productCodes);

        /// <summary>
        /// Gets the total price of scanned items in this transaction
        /// </summary>
        /// <returns>Total cost of products after discounts are applied</returns>
        decimal GetTotal();

        void ClearTransaction();
    }
}
