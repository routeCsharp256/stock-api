using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(12, TransactionBehavior.None)]
    public class AddDeliveryRequestSkuMapCustomType : Migration
    {
        public override void Up()
        {
            const string query = @"
                DO $$BEGIN
                    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'delivery_request_sku_maps_v1') THEN
                        CREATE TYPE delivery_request_sku_maps_v1 as
                        (
                            delivery_requests_id BIGINT,
                            sku_id BIGINT
                        );
                    END IF;
                END$$;";
            Execute.Sql(query);
        }

        public override void Down()
        {
            const string query = "DROP TYPE IF EXISTS delivery_request_sku_maps_v1;";
            Execute.Sql(query);
        }
    }
}