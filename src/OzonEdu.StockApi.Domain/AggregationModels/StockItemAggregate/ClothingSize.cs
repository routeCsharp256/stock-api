using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    public class ClothingSize : Enumeration
    {
        public static ClothingSize XS = new(1, nameof(XS));
        public static ClothingSize S = new(2, nameof(S));
        public static ClothingSize M = new(3, nameof(M));
        public static ClothingSize L = new(4, nameof(L));
        public static ClothingSize XL = new(5, nameof(XL));
        public static ClothingSize XXL = new(6, nameof(XXL));

        public ClothingSize(int id, string name) : base(id, name)
        {
        }
    }
}