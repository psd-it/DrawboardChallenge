using Drawboard.Technical.Challenge.Constants;
using System;

namespace Drawboard.Technical.Challenge.Exceptions
{
    public class ProductsNotConfiguredException : Exception
    {
        public ProductsNotConfiguredException() : base(ErrorMessages.ProductsNotConfigured)
        {
        }
    }
}
