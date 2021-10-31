using System;

namespace OzonEdu.StockApi.Domain.Exceptions
{
    public class NegativeValueException : Exception
    {
        public NegativeValueException(string message) : base(message)
        {
            
        }
        
        public NegativeValueException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}