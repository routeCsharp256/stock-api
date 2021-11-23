using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Temp
{
    [Migration(9)]
    public class FillDictionaries:ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO clothing_sizes (id, name)
                VALUES 
                    (1, 'XS'),
                    (2, 'S'),
                    (3, 'M'),
                    (4, 'L'),
                    (5, 'XL'),
                    (6, 'XXL')
                ON CONFLICT DO NOTHING
            ");

            Execute.Sql(@"
                INSERT INTO item_types (id, name)
                VALUES 
                    (1,  'TShirtStarter'),
                    (2,  'NotepadStarter'),
                    (3,  'PenStarter'),
                    (4,  'SocksStarter'),
                    (5,  'TShirtAfterProbation'),
                    (6,  'SweatshirtAfterProbation'),
                    (7,  'SweatshirtСonferenceSpeaker'),
                    (8,  'NotepadСonferenceSpeaker'),
                    (9,  'PenСonferenceSpeaker'),
                    (10, 'TShirtСonferenceListener'),
                    (11, 'NotepadСonferenceListener'),
                    (12, 'PenСonferenceListener'),
                    (13, 'TShirtVeteran'),
                    (14, 'SweatshirtVeteran'),
                    (15, 'NotepadVeteran'),
                    (16, 'PenVeteran'),
                    (17, 'CardHolderVeteran')
                ON CONFLICT DO NOTHING
            ");
        }
    }
}