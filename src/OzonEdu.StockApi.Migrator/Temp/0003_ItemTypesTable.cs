using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(3)]
    public class ItemTypes:Migration
    {
        public override void Up()
        {
            Create
                .Table("item_types")
                .WithColumn("id").AsInt32().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("item_types");
        }
    }
}