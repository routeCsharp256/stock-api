using System.Collections.Generic;

namespace OzonEdu.StockApi.HttpModels
{
    public record DeliveryRequestViewModel
    {
        public int Id { get; init; }

        public int DeliveryRequestId { get; init; }

        public int RequestStatus { get; init; }
        
        public IReadOnlyList<long> SkusCollection { get; init; }
    }
}