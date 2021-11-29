using System.Collections.Generic;
using FluentMigrator;

namespace OzonEdu.StockApi.Migrator.Migrations
{
    [Migration(11)]
    public class FixFillData : Migration 
    {
        public override void Up()
        {
            var updateCommands = new List<string>();
            updateCommands.Add(@"update skus set name='SweatshirtConferenceSpeaker XS' where skus.id=19");
            updateCommands.Add(@"update skus set name='SweatshirtConferenceSpeaker S' where skus.id=20");
            updateCommands.Add(@"update skus set name='SweatshirtConferenceSpeaker M' where skus.id=21");
            updateCommands.Add(@"update skus set name='SweatshirtConferenceSpeaker L' where skus.id=22");
            updateCommands.Add(@"update skus set name='SweatshirtConferenceSpeaker XL' where skus.id=23");
            updateCommands.Add(@"update skus set name='SweatshirtConferenceSpeaker XXL' where skus.id=24");
            updateCommands.Add(@"update skus set name='TShirtConferenceListener XS' where skus.id=25");
            updateCommands.Add(@"update skus set name='TShirtConferenceListener S' where skus.id=26");
            updateCommands.Add(@"update skus set name='TShirtConferenceListener M' where skus.id=27");
            updateCommands.Add(@"update skus set name='TShirtConferenceListener L' where skus.id=28");
            updateCommands.Add(@"update skus set name='TShirtConferenceListener XL' where skus.id=29");
            updateCommands.Add(@"update skus set name='TShirtConferenceListener XXL' where skus.id=30");
            updateCommands.Add(@"update skus set name='NotepadConferenceSpeaker' where skus.id=44");
            updateCommands.Add(@"update skus set name='PenConferenceSpeaker' where skus.id=45");
            updateCommands.Add(@"update skus set name='NotepadConferenceListener' where skus.id=46");
            updateCommands.Add(@"update skus set name='PenConferenceListener' where skus.id=47");

            foreach (var updateCommand in updateCommands)
            {
                Execute.Sql(updateCommand);
            }
        }

        public override void Down()
        {
            var updateCommands = new List<string>();
            updateCommands.Add(@"update skus set name='SweatshirtСonferenceSpeaker XS' where skus.id=19");
            updateCommands.Add(@"update skus set name='SweatshirtСonferenceSpeaker S' where skus.id=20");
            updateCommands.Add(@"update skus set name='SweatshirtСonferenceSpeaker M' where skus.id=21");
            updateCommands.Add(@"update skus set name='SweatshirtСonferenceSpeaker L' where skus.id=22");
            updateCommands.Add(@"update skus set name='SweatshirtСonferenceSpeaker XL' where skus.id=23");
            updateCommands.Add(@"update skus set name='SweatshirtСonferenceSpeaker XXL' where skus.id=24");
            updateCommands.Add(@"update skus set name='TShirtСonferenceListener XS' where skus.id=25");
            updateCommands.Add(@"update skus set name='TShirtСonferenceListener S' where skus.id=26");
            updateCommands.Add(@"update skus set name='TShirtСonferenceListener M' where skus.id=27");
            updateCommands.Add(@"update skus set name='TShirtСonferenceListener L' where skus.id=28");
            updateCommands.Add(@"update skus set name='TShirtСonferenceListener XL' where skus.id=29");
            updateCommands.Add(@"update skus set name='TShirtСonferenceListener XXL' where skus.id=30");
            updateCommands.Add(@"update skus set name='NotepadСonferenceSpeaker' where skus.id=44");
            updateCommands.Add(@"update skus set name='PenСonferenceSpeaker' where skus.id=45");
            updateCommands.Add(@"update skus set name='NotepadСonferenceListener' where skus.id=46");
            updateCommands.Add(@"update skus set name='PenСonferenceListener' where skus.id=47");

            foreach (var updateCommand in updateCommands)
            {
                Execute.Sql(updateCommand);
            }
        }
    }
}