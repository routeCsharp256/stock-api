using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.StockApi.HttpModels;
using OzonEdu.StockApi.Infrastructure.Commands.CreateDeliveryRequest;
using OzonEdu.StockApi.Infrastructure.Models;
using OzonEdu.StockApi.Infrastructure.Queries.DeliveryRequestAggregate;

namespace OzonEdu.StockApi.Controllers.V1
{
    [ApiController]
    [Route("v1/api/delivery-requests")]
    [Produces("application/json")]
    public class DeliveryRequestController : Controller
    {
        private readonly IMediator _mediator;

        public DeliveryRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<DeliveryRequestViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetDeliveryRequestsQuery
                {
                    Status = DeliveryRequestStatus.All
                },
                cancellationToken);
            return result.Items.Select(it => new DeliveryRequestViewModel
            {
                Id = it.Id,
                RequestStatus = (int)it.RequestStatus, // TODO: Исправить.
                DeliveryRequestId = it.DeliveryRequestId,
                SkusCollection = it.SkusCollection
            }).ToList();
        }

        [HttpGet("inwork")]
        public async Task<List<DeliveryRequestViewModel>> GetAllActive(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetDeliveryRequestsQuery
                {
                    Status = DeliveryRequestStatus.InWork
                },
                cancellationToken);
            return result.Items.Select(it => new DeliveryRequestViewModel
            {
                Id = it.Id,
                RequestStatus = (int)it.RequestStatus, // TODO: Исправить.
                DeliveryRequestId = it.DeliveryRequestId,
                SkusCollection = it.SkusCollection
            }).ToList();
        }

        [HttpPost]
        public async Task<int> Create(
            CreateDeliveryRequestInputModel value,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new CreateDeliveryRequestCommand
                {
                    Items = value.Items.Select(
                            it => new DeliveryRequestDto
                            {
                                Quantity = it.Quantity,
                                Sku = it.Sku
                            })
                        .ToList()
                },
                cancellationToken);
        }
    }
}