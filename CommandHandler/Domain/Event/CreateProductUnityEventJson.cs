namespace CommandHandler.Domain.Event
{
    public class CreateProductUnityEventJson
    {
        public string Event { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
