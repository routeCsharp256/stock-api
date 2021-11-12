using System.Collections.Generic;

namespace OzonEdu.StockApi.Models.InputModels
{
    public class CreateDeliveryRequestInputModel
    {
        public IReadOnlyList<DeliveryRequestModel> Items { get; set; }
    }
}