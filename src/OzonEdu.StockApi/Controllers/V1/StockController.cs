using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.StockApi.HttpModels;
using OzonEdu.StockApi.Infrastructure.Commands.CreateStockItem;

namespace OzonEdu.StockApi.Controllers.V1
{
    [ApiController]
    [Route("v1/api/stocks")]
    [Produces("application/json")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            // use mediator
            throw new NotSupportedException();
        }
        
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken token)
        {
            // use mediator
            throw new NotSupportedException();
        }

        [HttpGet("quantity/{sku:long}")]
        public async Task<ActionResult<int>> GetAvailableQuantity(long sku, CancellationToken token)
        {
            // use mediator
            throw new NotSupportedException();
        }

        /// <summary>
        /// Добавляет stock item.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Add(StockItemPostViewModel postViewModel, CancellationToken token)
        {
            var createStockItemCommand = new CreateStockItemCommand
            {
                Name = postViewModel.Name,
                Quantity = postViewModel.Quantity,
                Sku = postViewModel.Sku,
                Tags = postViewModel.Tags,
                ClothingSize = postViewModel.ClothingSize,
                MinimalQuantity = postViewModel.MinimalQuantity,
                StockItemType = postViewModel.StockItemType
            };
            var result = await _mediator.Send(createStockItemCommand, token);
            return Ok(result);
        }
    }
}