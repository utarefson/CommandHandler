namespace CommandHandler.Domain.Entity
{
    public interface IEntity
    {
        bool Create();
        bool Update();
        bool Delete();
    }
}
