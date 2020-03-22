using Newtonsoft.Json;
using System;
using System.IO;

namespace CommandHandler.Infrastructure.Repository
{
    public class FileRepository<T> : IRepository<T> where T : class
    {
        private const string FILE_PATH = @"C:\Temp\EventStore.txt";
        
        public bool Append(T entity)
        {
            bool stored = true;

            try
            {
                string json = JsonConvert.SerializeObject(
                    entity,
                    Formatting.None,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                );

                string[] lines = new string[] { json };
                File.AppendAllLines(FILE_PATH, lines);
            }
            catch (Exception)
            {
                stored = false;
            }

            return stored;
        }
    }
}
