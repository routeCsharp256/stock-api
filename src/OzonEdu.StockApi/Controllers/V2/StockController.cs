using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.StockApi.HttpModels;
using OzonEdu.StockApi.Models;
using OzonEdu.StockApi.Services.Interfaces;

namespace OzonEdu.StockApi.Controllers.V2
{
    [ApiController]
    [Route("v2/api/stocks")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public async Task<ActionResult<StockItem>> Add(StockItemPostViewModelV2 postViewModel, CancellationToken token)
        {
            var createdStockItem = await _stockService.Add(new StockItemCreationModel
            {
                ItemName = postViewModel.ItemName,
                Quantity = postViewModel.Quantity.Value
            }, token);
            return Ok(createdStockItem);
        }
    }
}