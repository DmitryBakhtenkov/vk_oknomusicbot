using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using vkontakteoknomusic_2.Models;

namespace vkontakteoknomusic_2
{
    public class Repository
    {
        private readonly CommandContext _context;

        public Repository() { /*For tests */ }

        public Repository(string connectionString, string database, string collection)
        {
            _context = new CommandContext(connectionString, database, collection);
        }

        public virtual async Task<IEnumerable<Command>> GetAllAsync()
        {
            return await _context.Notes.Find(_ => true).ToListAsync();
        }

        public virtual async Task<Command> GetByTriggerAsync(string trigger)
        {
            return await _context.Notes.Find(c => c.Trigger == trigger).FirstOrDefaultAsync();
        }

        public virtual async Task<bool> CreateCommandAsync(Command command)
        {
            if(command.ButtonNames != null)
                command.ButtonNames = command.ButtonNames.TakeWhile(c => c != null);
            
            if (await _context.Notes.Find(c => c.Trigger == command.Trigger).FirstOrDefaultAsync() != null || command.Trigger == null)
            {
                return false;
            }

            await _context.Notes.InsertOneAsync(command);
            return true;
        }

        public virtual async Task<bool> UpdateCommandAsync(string oldParam, Command newCommand)
        {
            if (_context.Notes.Find(_ => true).ToList().Contains(newCommand))
                return false;

            var filter = Builders<Command>.Filter.Eq(c => c.Trigger, oldParam);
            var oldCommand = await GetByTriggerAsync(oldParam);
            newCommand.Id = oldCommand.Id;
           //var update = Builders<Command>.Update.Set(c => c.Trigger, newCommand.Trigger);
            var updateResult = await _context.Notes.ReplaceOneAsync(filter, newCommand);

            if (updateResult.IsAcknowledged)
                return true;
            else
                return false;
        }

        public virtual async Task<bool> DeleteCommandAsync(Command command)
        {
            if (await GetByTriggerAsync(command.Trigger) != null)
            {
                var filter = Builders<Command>.Filter.Eq(c => c.Trigger, command.Trigger);
                await _context.Notes.DeleteOneAsync(filter);
                return true;
            }
            return false;
        }
    }
}
