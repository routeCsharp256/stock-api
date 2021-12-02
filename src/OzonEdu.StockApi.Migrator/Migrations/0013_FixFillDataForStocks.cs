using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(13)]
    public class FixFillDataForStocks : ForwardOnlyMigration {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO stocks (sku_id, quantity, minimal_quantity)
                VALUES
                    (38, 500, 50),
                    (39, 500, 50),
                    (40, 500, 50),
                    (41, 1, 1),
                    (42, 1, 1),
                    (43, 500, 50),
                    (44, 500, 50),
                    (45, 500, 50),
                    (46, 500, 50),
                    (47, 500, 50),
                    (48, 500, 50),
                    (49, 500, 50),
                    (50, 500, 50)
                ON CONFLICT DO NOTHING");
        }
    }
}