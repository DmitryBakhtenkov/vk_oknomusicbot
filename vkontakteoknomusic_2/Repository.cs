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
        private CommandContext _context = new CommandContext();

        public async Task<IEnumerable<Command>> GetAllAsync()
        {
            return await _context.Notes.Find(_ => true).ToListAsync();
        }

        public async Task<Command> GetCommandByTriggerAsync(string trigger)
        {
            return await _context.Notes.Find(c => c.Trigger == trigger).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCommandAsync(Command command)
        {
            command.ButtonNames = command.ButtonNames.TakeWhile(c => c != null);
            
            if (await _context.Notes.Find(c => c.Trigger == command.Trigger).FirstOrDefaultAsync() != null || command.Trigger == null)
            {
                return false;
            }

            await _context.Notes.InsertOneAsync(command);
            return true;
        }

        public async Task<bool> UpdateCommandAsync(string oldTrigger, Command newCommand)
        {
            if (_context.Notes.Find(_ => true).ToList().Contains(newCommand))
                return false;

            var filter = Builders<Command>.Filter.Eq(c => c.Trigger, oldTrigger);
            var update = Builders<Command>.Update.Set(c => c, newCommand);
            var updateResult = await _context.Notes.UpdateOneAsync(filter, update);

            if (updateResult.IsAcknowledged)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteCommandAsync(Command command)
        {
            if (await GetCommandByTriggerAsync(command.Trigger) != null)
            {
                var filter = Builders<Command>.Filter.Eq(c => c.Trigger, command.Trigger);
                await _context.Notes.DeleteOneAsync(filter);
                return true;
            }
            return false;
        }
    }
}
