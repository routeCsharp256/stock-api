using System;

namespace OzonEdu.StockApi.Infrastructure.Exceptions
{
    public class MessageBrokerException : Exception
    {
        public MessageBrokerException(string message) : base(message)
        {
            
        }

        public MessageBrokerException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}