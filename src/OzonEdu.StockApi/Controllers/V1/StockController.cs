using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.StockApi.HttpModels;
using OzonEdu.StockApi.Infrastructure.Commands;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Services.Interfaces;
using StockItem = OzonEdu.StockApi.Models.StockItem;

namespace OzonEdu.StockApi.Controllers.V1
{
    [ApiController]
    [Route("v1/api/stocks")]
    [Produces("application/json")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IMediator _mediator;

        public StockController(IStockService stockService, IMediator mediator)
        {
            _stockService = stockService;
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var stockItems = await _stockService.GetAll(token);
            return Ok(stockItems);
        }
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<StockItem>> GetById(long id, CancellationToken token)
        {
            var stockItem = await _stockService.GetById(id, token);
            if (stockItem is null)
            {
                return NotFound();
            }

            return stockItem;
        }

        [HttpGet("quantity/{sku:long}")]
        public async Task<ActionResult<int>> GetAvailableQuantity(long sku, CancellationToken cancellationToken)
        {
            var query = new GetAvailableQuantityQuery()
            {
                Sku = sku
            };
            var result = await _mediator.Send(query, cancellationToken);
            
            return result;
        }

        /// <summary>
        /// Добавляет stock item.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Add(StockItemPostViewModel postViewModel, CancellationToken token)
        {
            var createStockItemCommand = new CreateStockItemCommand()
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