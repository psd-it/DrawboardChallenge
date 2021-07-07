using Drawboard.Technical.Challenge.Models;
using Drawboard.Technical.Challenge.Services;
using System;
using System.Collections.Generic;

namespace Drawboard.Technical.Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var posTerminal = new PointOfSaleTerminal();
            posTerminal.SetPricing(GetProducts());

            var testStrings = new []
            {
                "ABCDABA", "CCCCCCC", "ABCD"
            };
            
            foreach(var productSequence in testStrings)
            {
                Console.WriteLine($"Scanning product sequence {productSequence}");
                posTerminal.Scan(productSequence);
                Console.WriteLine($"Total cost is ${posTerminal.GetTotal()}\n");
                posTerminal.ClearTransaction();
            }
        }

        private static IEnumerable<Product> GetProducts()
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
