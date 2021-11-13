using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using OzonEdu.StockApi.HttpModels;
using OzonEdu.StockApi.Infrastructure.Commands.CreateStockItem;
using OzonEdu.StockApi.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.StockApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.StockApi.Controllers.V1
{
    [ApiController]
    [Route("v1/api/stocks")]
    [Produces("application/json")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDbConnectionFactory<NpgsqlConnection> _factory;

        public StockController(IMediator mediator, IDbConnectionFactory<NpgsqlConnection> factory)
        {
            _mediator = mediator;
            _factory = factory;
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

        [HttpGet("quantity")]
        public async Task<StockItemQuantityModel[]> GetAvailableQuantity(long[] sku, CancellationToken token)
        {
            var result = await _mediator.Send(new GetStockItemsAvailableQuantityQuery
            {
                Skus = sku
            }, token);

            return result.Items.Select(it => new StockItemQuantityModel
            {
                Sku = it.Sku,
                Quantity = it.Quantity
            }).ToArray();
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

        [HttpGet("item-type-id-by-name/{name}")]
        public async Task<long> GetItemTypeIdByName([FromRoute] string name, CancellationToken cancellationToken)
        {
            var connection = await _factory.CreateConnection(cancellationToken);
            return await connection.QuerySingleAsync<long>($"SELECT id from item_types WHERE name = {name}");
        }
    }
}