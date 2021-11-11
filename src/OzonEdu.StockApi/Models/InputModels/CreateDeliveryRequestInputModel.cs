using System.Collections.Generic;
using MediatR;

namespace OzonEdu.StockApi.Models.InputModels
{
    public class CreateDeliveryRequestInputModel
    {
        public IReadOnlyList<DeliveryRequestModel> Items { get; set; }
    }
}