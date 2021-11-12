using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(2)]
    public class StockTable: Migration
    {
        public override void Up()
        {
            Create
                .Table("stocks")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("sku_id").AsInt64().NotNullable()
                .WithColumn("quantity").AsInt32().NotNullable()
                .WithColumn("minimal_quantity").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("stocks");
        }
    }
}