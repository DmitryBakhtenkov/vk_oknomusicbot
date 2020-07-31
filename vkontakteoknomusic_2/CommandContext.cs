using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using vkontakteoknomusic_2.Models;

namespace vkontakteoknomusic_2
{
    /// <summary>
    /// Контекст всех команд
    /// </summary>
    public class CommandContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collection;

        //Connect to db
        public CommandContext(string username, string password, string hostPort, string database, string collection)
        {
            var client = new MongoClient($"mongodb://{username}:{password}@{hostPort}/{database}");
            if (client != null)
            {
                _database = client.GetDatabase(database);
                _collection = collection;
            }
              
        }
        public CommandContext(string connectionString, string database, string collection)
        {
            var client = new MongoClient(connectionString);
            if (client != null)
            {
                _database = client.GetDatabase(database);
                _collection = collection;
            }

        }
        //Get collection
        public IMongoCollection<Command> Notes => _database.GetCollection<Command>(_collection);
    }
    
}