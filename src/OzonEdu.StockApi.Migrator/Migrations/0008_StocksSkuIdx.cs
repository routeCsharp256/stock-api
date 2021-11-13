using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(8, TransactionBehavior.None)]
    public class StocksSkuIdx: ForwardOnlyMigration 
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX CONCURRENTLY stocks_sku_id_idx ON stocks (sku_id)"
            );
        }
    }
}