using System;

namespace OzonEdu.StockApi.Domain.Exceptions.StockItemAggregate
{
    public class StockItemSizeException : Exception
    {
        public StockItemSizeException(string message) : base(message)
        {
            
        }
        
        public StockItemSizeException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}