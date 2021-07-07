using Drawboard.Technical.Challenge.Constants;
using Drawboard.Technical.Challenge.Exceptions;
using Drawboard.Technical.Challenge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Drawboard.Technical.Challenge.Services
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private IEnumerable<Product> _products;
        private IDictionary<char, int> _scannedProductCounts;
        private decimal _runningTotal;
        private bool _discountApplied;

        public PointOfSaleTerminal()
        {
            _scannedProductCounts = new Dictionary<char, int>();
            _discountApplied = false;
        }

        public decimal GetTotal()
        {
            return _runningTotal;
        }

        public void Scan(char productCode)
        {
            var product = _products.FirstOrDefault(x => x.ProductCode == productCode)
                ?? throw new ProductNotFoundException(string.Format(ErrorMessages.ProductWithIdNotFound, productCode));
            if (_scannedProductCounts.TryGetValue(productCode, out var productCount))
            {
                _scannedProductCounts[productCode] = ++productCount;
                AddToTotal(product, product.Pack?.PackSize == productCount);
                return;
            }

            _scannedProductCounts.Add(product.ProductCode, ++productCount);
            AddToTotal(product);
        }

        public void Scan(string productCodes)
        {
            foreach (var code in productCodes.ToCharArray())
            {
                Scan(code);
            }
        }

        public void SetPricing(IEnumerable<Product> products)
        {
            if (!products?.Any() ?? true)
            {
                throw new ProductsNotConfiguredException();
            }
            _products = products;
        }

        private void AddToTotal(Product product, bool adjustDiscount = false)
        {
            _runningTotal += product.UnitPrice;

            if (!_discountApplied && adjustDiscount)
            {
                var discount = (product.Pack.PackSize * product.UnitPrice) - product.Pack.PackPrice;
                _runningTotal -= discount;
                _discountApplied = true;
            }

        }

        // Getter methods helps validate unit tests, if we had a persistence interface
        // these would not be needed as we could Mock it out and validate
        public IDictionary<char, int> GetScannedProducts()
        {
            return _scannedProductCounts;
        }

        public IEnumerable<Product> GetDefinedProducts()
        {
            return _products;
        }

        public void ClearTransaction()
        {
            _runningTotal = 0m;
            _discountApplied = false;
            _scannedProductCounts.Clear();
        }
    }
}
