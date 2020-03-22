namespace CommandHandler.Infrastructure.Repository
{
    /**
     * The event store only knows the Append operation.
     * 
     * This is not a classic CRUD.
     **/
    public interface IRepository<T> where T : class
    {
        bool Append(T entity);
    }
}
