using System.Collections.Generic;
using OzonEdu.StockApi.Enums;

namespace OzonEdu.StockApi.HttpModels
{
    public record DeliveryRequestViewModel
    {
        public int Id { get; init; }

        public long DeliveryRequestId { get; init; }

        public DeliveryRequestStatus RequestStatus { get; init; }
        
        public IReadOnlyList<long> SkusCollection { get; init; }
    }
}