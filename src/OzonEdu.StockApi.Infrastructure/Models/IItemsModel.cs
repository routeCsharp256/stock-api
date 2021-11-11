using System.Collections.Generic;

namespace OzonEdu.StockApi.Infrastructure.Models
{
    public interface IItemsModel<TItemsModel>
        where TItemsModel : class
    {
        IReadOnlyList<TItemsModel> Items { get; set; }
    }
}