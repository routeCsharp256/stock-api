using System;
using System.Collections.Generic;

namespace OzonEdu.StockApi.HttpModels
{
    public record CreateDeliveryRequestInputModel
    {
        public IReadOnlyList<DeliveryRequestModel> Items { get; init; } = Array.Empty<DeliveryRequestModel>();
    }
}