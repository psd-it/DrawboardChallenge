using Drawboard.Technical.Challenge.Constants;
using System;

namespace Drawboard.Technical.Challenge.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base(ErrorMessages.ProductNotFound)
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}
