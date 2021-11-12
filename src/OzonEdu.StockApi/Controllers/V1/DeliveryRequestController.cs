using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.StockApi.Infrastructure.Commands.CreateDeliveryRequest;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.DeliveryRequestAggregate;
using OzonEdu.StockApi.Models.InputModels;
using OzonEdu.StockApi.Models.ViewModels;

namespace OzonEdu.StockApi.Controllers.V1
{
    [ApiController()]
    [Route("/api/[controller]")]
    public class DeliveryRequestController : Controller
    {
        private readonly IMediator _mediator;

        public DeliveryRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<List<DeliveryRequestViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDeliveryRequestsQuery()
            {
                Status = DeliveryRequestStatus.All
            }, cancellationToken);
            return result.Items.Select(it => new DeliveryRequestViewModel(it)).ToList();
        }

        [HttpGet("inwork")]
        public async Task<List<DeliveryRequestViewModel>> GetAllActive(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDeliveryRequestsQuery()
            {
                Status = DeliveryRequestStatus.InWork
            }, cancellationToken);
            return result.Items.Select(it => new DeliveryRequestViewModel(it)).ToList();
        }

        [HttpPost]
        public async Task<int> Create([FromBody] CreateDeliveryRequestInputModel value,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(new CreateDeliveryRequestCommand()
            {
                Items = value.Items.Select(it => new DeliveryRequestDto()
                {
                    Quantity = it.Quantity,
                    Sku = it.Sku
                }).ToList()
            }, cancellationToken);
        }
    }
}