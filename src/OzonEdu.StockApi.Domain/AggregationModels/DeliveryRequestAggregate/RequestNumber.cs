namespace OzonEdu.StockApi.Domain.AggregationModels.DeliveryRequestAggregate
{
    public class RequestNumber
    {
        public RequestNumber(long value)
        {
            Value = value;
        }

        public long Value { get; }
    }
}