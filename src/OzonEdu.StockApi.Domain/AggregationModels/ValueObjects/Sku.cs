using System.Collections.Generic;
using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.AggregationModels.ValueObjects
{
    public class Sku : ValueObject
    {
        public long Value { get; }
        
        public Sku(long sku)
        {
            Value = sku;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}