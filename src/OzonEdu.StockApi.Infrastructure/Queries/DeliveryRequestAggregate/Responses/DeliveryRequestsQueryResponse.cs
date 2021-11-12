using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Infrastructure.Queries.Responses
{
    public class DeliveryRequestsQueryResponse : IItemsModel<DeliveryRequestItem>
    {
        public IReadOnlyList<DeliveryRequestItem> Items { get; set; }
    }
}