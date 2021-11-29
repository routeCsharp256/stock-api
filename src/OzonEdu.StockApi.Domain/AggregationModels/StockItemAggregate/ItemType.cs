using OzonEdu.StockApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.AggregationModels.StockItemAggregate
{
    public class ItemType : Enumeration
    {
        public static ItemType TShirtStarter = new(1, nameof(TShirtStarter));
        public static ItemType NotepadStarter = new(2, nameof(NotepadStarter));
        public static ItemType PenStarter = new(3, nameof(PenStarter));
        public static ItemType SocksStarter = new(4, nameof(SocksStarter));
        public static ItemType TShirtAfterProbation = new(5, nameof(TShirtAfterProbation));
        public static ItemType SweatshirtAfterProbation = new(6, nameof(SweatshirtAfterProbation));
        public static ItemType SweatshirtConferenceSpeaker = new(7, nameof(SweatshirtConferenceSpeaker));
        public static ItemType NotepadConferenceSpeaker = new(8, nameof(NotepadConferenceSpeaker));
        public static ItemType PenConferenceSpeaker = new(9, nameof(PenConferenceSpeaker));
        public static ItemType TShirtСonferenceListener = new(10, nameof(TShirtСonferenceListener));
        public static ItemType NotepadConferenceListener = new(11, nameof(NotepadConferenceListener));
        public static ItemType PenConferenceListener = new(12, nameof(PenConferenceListener));
        public static ItemType TShirtVeteran = new(13, nameof(TShirtVeteran));
        public static ItemType SweatshirtVeteran = new(14, nameof(SweatshirtVeteran));
        public static ItemType NotepadVeteran = new(15, nameof(NotepadVeteran));
        public static ItemType PenVeteran = new(16, nameof(PenVeteran));
        public static ItemType CardHolderVeteran = new(17, nameof(CardHolderVeteran));
        
        public ItemType(int id, string name) : base(id, name)
        {
        }
    }
}