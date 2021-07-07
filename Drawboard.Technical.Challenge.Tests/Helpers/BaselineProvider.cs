using Drawboard.Technical.Challenge.Models;
using System.Collections.Generic;

namespace Drawboard.Technical.Challenge.Tests.Helpers
{
    public static class BaselineProvider
    {
        public static IEnumerable<Product> GetProducts()
        {
            return new[]
            {
                new Product
                {
                    ProductCode = 'A',
                    UnitPrice = 1.25m,
                    Pack = new ProductPack
                    {
                        PackPrice = 3m,
                        PackSize = 3
                    }
                },
                new Product
                {
                    ProductCode = 'B',
                    UnitPrice = 4.25m
                },
                new Product
                {
                    ProductCode = 'C',
                    UnitPrice = 1m,
                    Pack = new ProductPack
                    {
                        PackPrice = 5m,
                        PackSize = 6
                    }
                },
                new Product
                {
                    ProductCode = 'D',
                    UnitPrice = .75m
                }
            };
        }
    }
}
