namespace OzonEdu.StockApi.Infrastructure.Models
{
    /// <summary>
    /// Транспортная модель для элемента стока
    /// </summary>
    public class StockItemDto
    {
        /// <summary>
        /// Код товарной позиции
        /// </summary>
        public long Sku { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор размера
        /// </summary>
        public int? ClothingSizeId { get; set; }

        /// <summary>
        /// Идентификатор типа товара
        /// </summary>
        public int ItemTypeId { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Минимальное допустимое количество
        /// </summary>
        public int MinimalQuantity { get; set; }
    }
}