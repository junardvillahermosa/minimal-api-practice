using Microsoft.EntityFrameworkCore;
using SixMinApi.Models;

//Creating different methods

namespace SixMinApi.Data
{
    public class CommandRepo : ICommandRepo //Implement Interface
    {
        private readonly AppDbContext _context; //Private read-only field

        //Create constructors 
        public CommandRepo(AppDbContext context) //parameters
        {
            _context = context;
            
        }
        public async Task CreateCommand(Command cmd)
        {
            //throw new NotImplementedException();
            if(cmd == null){
                throw new ArgumentNullException(nameof(cmd));
            }
            await _context.Commands!.AddAsync(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            //throw new NotImplementedException();
            if(cmd == null){
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Remove(cmd);
        }

        public async Task<IEnumerable<Command>> GetAllCommands()
        {
            //throw new NotImplementedException();
            return await _context.Commands!.ToListAsync();
        }

        public async Task<Command?> GetCommandById(int id)
        {
            //throw new NotImplementedException();
            return await _context.Commands!.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCommand(Command cmd)
        {
            var existingCommand = await _context.Commands!.FirstOrDefaultAsync(c => c.Id == cmd.Id);

            if (existingCommand != null)
            {
                // You can update the properties of the existingCommand based on the cmd parameter
                existingCommand.HowTo = cmd.HowTo;
                existingCommand.Platform = cmd.Platform;
                existingCommand.CommandLine = cmd.CommandLine;
                existingCommand.PlatformId = cmd.PlatformId;


                // Mark the entity as modified to ensure it gets updated in the database
                _context.Entry(existingCommand).State = EntityState.Modified;
            }
            else
            {
                throw new InvalidOperationException("Command not found.");
            }
        }

        public async Task SaveChanges()
        {
            //throw new NotImplementedException();
            await _context.SaveChangesAsync();
        }
    }
}