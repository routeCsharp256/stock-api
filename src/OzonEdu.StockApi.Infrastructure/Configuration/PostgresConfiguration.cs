using Npgsql;
using Npgsql.NameTranslation;
using OzonEdu.StockApi.Infrastructure.Repositories.Models;

namespace OzonEdu.StockApi.Infrastructure.Configuration
{
    public static class PostgresConfiguration
    {
        private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

        /// <summary>
        /// Map DAL models to composite types (enables UNNEST)
        /// </summary>
        public static void MapCompositeTypes()
        {
            var mapper = NpgsqlConnection.GlobalTypeMapper;
            mapper.MapComposite<DeliveryRequestSkuMap>("delivery_request_sku_maps_v1", Translator);
        }
    }
}