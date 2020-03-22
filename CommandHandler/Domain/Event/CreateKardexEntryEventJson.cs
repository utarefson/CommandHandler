namespace CommandHandler.Domain.Event
{
    public class CreateKardexEntryEventJson
    {
        public string Event { get; set; }
        public int Id { get; set; }
        public long KardexEntryDate { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int IsASale { get; set; }
    }
}
