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

        //Connect to db
        public CommandContext()
        {
            var client = new MongoClient("mongodb://mongo-root:QazPlmOkWs12@188.227.84.24:20000/");
            if (client != null)
                _database = client.GetDatabase("vkontakteTestDb");
        }
        //Get collection
        public IMongoCollection<Command> Notes => _database.GetCollection<Command>("vkBot");
    }
    
}