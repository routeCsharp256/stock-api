using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.StockApi.HttpModels;
using OzonEdu.StockApi.Models;
using OzonEdu.StockApi.Services.Interfaces;

namespace OzonEdu.StockApi.Controllers.V1
{
    [ApiController]
    [Route("v1/api/stocks")]
    [Produces("application/json")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
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

        /// <summary>
        /// Добавляет stock item.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<StockItem>> Add(StockItemPostViewModel postViewModel, CancellationToken token)
        {
            var createdStockItem = await _stockService.Add(new StockItemCreationModel
            {
                ItemName = postViewModel.ItemName,
                Quantity = postViewModel.Quantity
            }, token);
            return Ok(createdStockItem);
        }
    }

    public class CustomException : Exception
    {
        public CustomException() : base("some custom exception")
        {
        }
    }
}