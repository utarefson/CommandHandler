namespace CommandHandler.Domain.Command.KardexEntry
{
    /**
     * RoutingKey: CreateKardexEntryCommand
     **/
    public class CreateKardexEntryCommandJson
    {
        public long KardexEntryDate { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int IsASale { get; set; }
    }
}
