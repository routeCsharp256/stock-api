using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries
{
    public class DeliveryRequestsQueryResponse : IItemsModel<DeliveryRequestItem>
    {
        public IReadOnlyList<DeliveryRequestItem> Items { get; set; }
    }
}