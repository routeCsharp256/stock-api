using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(10)]
    public class FillData: ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO skus (id, name, item_type_id,clothing_size)
                VALUES 
                    (1,  'TShirtStarter XS',  1, 1),
                    (2,  'TShirtStarter S',   1, 2),
                    (3,  'TShirtStarter M',   1, 3),
                    (4,  'TShirtStarter L',   1, 4),
                    (5,  'TShirtStarter XL',  1, 5),
                    (6,  'TShirtStarter XXL', 1, 6),
                    (7,  'TShirtAfterProbation XS',  5, 1),
                    (8,  'TShirtAfterProbation S',   5, 2),
                    (9,  'TShirtAfterProbation M',   5, 3),
                    (10, 'TShirtAfterProbation L',   5, 4),
                    (11, 'TShirtAfterProbation XL',  5, 5),
                    (12, 'TShirtAfterProbation XXL', 5, 6),
                    (13, 'SweatshirtAfterProbation XS',  6, 1),
                    (14, 'SweatshirtAfterProbation S',   6, 2),
                    (15, 'SweatshirtAfterProbation M',   6, 3),
                    (16, 'SweatshirtAfterProbation L',   6, 4),
                    (17, 'SweatshirtAfterProbation XL',  6, 5),
                    (18, 'SweatshirtAfterProbation XXL', 6, 6),
                    (19, 'SweatshirtСonferenceSpeaker XS',  7, 1),
                    (20, 'SweatshirtСonferenceSpeaker S',   7, 2),
                    (21, 'SweatshirtСonferenceSpeaker M',   7, 3),
                    (22, 'SweatshirtСonferenceSpeaker L',   7, 4),
                    (23, 'SweatshirtСonferenceSpeaker XL',  7, 5),
                    (24, 'SweatshirtСonferenceSpeaker XXL', 7, 6),
                    (25, 'TShirtСonferenceListener XS',  10, 1),
                    (26, 'TShirtСonferenceListener S',   10, 2),
                    (27, 'TShirtСonferenceListener M',   10, 3),
                    (28, 'TShirtСonferenceListener L',   10, 4),
                    (29, 'TShirtСonferenceListener XL',  10, 5),
                    (30, 'TShirtСonferenceListener XXL', 10, 6),
                    (31, 'TShirtVeteran XS',  13, 1),
                    (31, 'TShirtVeteran S',   13, 2),
                    (32, 'TShirtVeteran M',   13, 3),
                    (33, 'TShirtVeteran L',   13, 4),
                    (34, 'TShirtVeteran XL',  13, 5),
                    (35, 'TShirtVeteran XXL', 13, 6),
                    (36, 'SweatshirtVeteran XS',  14, 1),
                    (37, 'SweatshirtVeteran S',   14, 2),
                    (38, 'SweatshirtVeteran M',   14, 3),
                    (38, 'SweatshirtVeteran L',   14, 4),
                    (39, 'SweatshirtVeteran XL',  14, 5),
                    (40, 'SweatshirtVeteran XXL', 14, 6),
                    (41, 'NotepadStarter', 2, null),
                    (42, 'PenStarter', 3, null),
                    (43, 'SocksStarter', 4, null),
                    (44, 'NotepadСonferenceSpeaker', 8, null),
                    (45, 'PenСonferenceSpeaker', 9, null),
                    (46, 'NotepadСonferenceListener', 11, null),
                    (47, 'PenСonferenceListener', 12, null),
                    (48, 'NotepadVeteran', 15, null),
                    (49, 'PenVeteran', 16, null),
                    (50, 'CardHolderVeteran', 17, null)
                ON CONFLICT DO NOTHING");
            
            Execute.Sql(@"
                INSERT INTO stocks (sku_id, quantity, minimal_quantity)
                VALUES
                    (1,  500, 50),
                    (2,  500, 50),
                    (3,  500, 50),
                    (4,  500, 50),
                    (5,  500, 50),
                    (6,  500, 50),
                    (7,  500, 50),
                    (8,  500, 50),
                    (9,  500, 50),
                    (10, 500, 50),
                    (11, 500, 50),
                    (12, 500, 50),
                    (13, 500, 50),
                    (14, 500, 50),
                    (15, 500, 50),
                    (16, 500, 50),
                    (17, 500, 50),
                    (18, 500, 50),
                    (19, 500, 50),
                    (20, 500, 50),
                    (21, 500, 50),
                    (22, 500, 50),
                    (23, 500, 50),
                    (24, 500, 50),
                    (25, 500, 50),
                    (26, 500, 50),
                    (27, 500, 50),
                    (28, 500, 50),
                    (29, 500, 50),
                    (30, 500, 50),
                    (31, 500, 50),
                    (32, 500, 50),
                    (33, 500, 50),
                    (34, 500, 50),
                    (35, 500, 50),
                    (36, 500, 50),
                    (37, 500, 50)
                ON CONFLICT DO NOTHING");
        }
    }
}