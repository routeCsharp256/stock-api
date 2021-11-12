using System.Collections.Generic;
using OzonEdu.StockApi.Infrastructure.Models;

namespace OzonEdu.StockApi.Models.ViewModels
{
    public class DeliveryRequestViewModel
    {
        public DeliveryRequestViewModel()
        {
            
        }

        public DeliveryRequestViewModel(DeliveryRequestItem queryResponse)
        {
            Id = queryResponse.Id;
            DeliveryRequestId = queryResponse.DeliveryRequestId;
            RequestStatus = queryResponse.RequestStatus;
            SkusCollection = queryResponse.SkusCollection;
        }
        
        public int Id { get; set; }
        public int DeliveryRequestId { get; set; }
        public DeliveryRequestStatus RequestStatus { get; set; }
        public IReadOnlyList<long> SkusCollection { get; set; }
    }
}