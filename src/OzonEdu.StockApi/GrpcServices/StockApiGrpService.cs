using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using OzonEdu.StockApi.Grpc;

namespace OzonEdu.StockApi.GrpcServices
{
    public class StockApiGrpService : StockApiGrpc.StockApiGrpcBase
    {
        private readonly IMediator _mediator;

        public StockApiGrpService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetAllStockItemsResponse> GetAllStockItems(
            Empty _,
            ServerCallContext context)
        {
            // Использовать mediatr
            throw new NotImplementedException();
        }

        public override Task<Empty> AddStockItem(AddStockItemRequest request, ServerCallContext context)
        {
            // Использовать mediatr
            throw new NotImplementedException();
        }
    }
}