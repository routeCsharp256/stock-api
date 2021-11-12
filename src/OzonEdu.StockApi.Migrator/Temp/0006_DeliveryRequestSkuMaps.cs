using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(6)]
    public class DeliveryRequestSkuMaps: Migration
    {
        public override void Up()
        {
            Create
                .Table("delivery_request_sku_maps")
                .WithColumn("delivery_requests_id").AsInt64().PrimaryKey()
                .WithColumn("sku_id").AsInt64().PrimaryKey();
        }

        public override void Down()
        {
            Delete.Table("delivery_request_sku_maps");
        }
    }
}