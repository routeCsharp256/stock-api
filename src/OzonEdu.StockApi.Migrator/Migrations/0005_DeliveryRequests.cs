using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Temp
{
    [Migration(5)]
    public class DeliveryRequests: Migration {
        public override void Up()
        {
            Create
                .Table("delivery_requests")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("request_id").AsInt64().NotNullable()
                .WithColumn("request_status").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("delivery_requests");
        }
    }
}