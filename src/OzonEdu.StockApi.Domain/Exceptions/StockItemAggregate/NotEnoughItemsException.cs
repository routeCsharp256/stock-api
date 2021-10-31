using System;

namespace OzonEdu.StockApi.Domain.Exceptions.StockItemAggregate
{
    public class NotEnoughItemsException : Exception
    {
        public NotEnoughItemsException(string message) : base(message)
        {
            
        }
        
        public NotEnoughItemsException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}