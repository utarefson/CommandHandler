namespace CommandHandler.Infrastructure.Repository
{
    public static class RepositoryFactory<T> where T : class
    {
        public static IRepository<T> Create()
        {
            // return new MySQLRepository<T>();
            return new FileRepository<T>();
        }
    }
}
