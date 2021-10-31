using MediatR;

namespace OzonEdu.StockApi.Infrastructure.Commands
{
    public class CreateStockItemCommand : IRequest<int>
    {
        /// <summary>
        /// Идентификатор нового товара
        /// </summary>
        public long Sku { get; init; }
        
        /// <summary>
        /// Название позиции
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Тип позиции
        /// </summary>
        public int StockItemType { get; init; }

        /// <summary>
        /// Размер позиции, если это одежда
        /// </summary>
        public int ClothingSize { get; init; }

        /// <summary>
        /// Количество элементов в наличии
        /// </summary>
        public int Quantity { get; init; }

        /// <summary>
        /// Минимальное количество позиций
        /// </summary>
        public int? MinimalQuantity { get; init; }

        /// <summary>
        /// Дополнительные теги
        /// </summary>
        public string Tags { get; init; }
    }
}