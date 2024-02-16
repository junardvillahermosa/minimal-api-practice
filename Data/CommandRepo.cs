using Microsoft.EntityFrameworkCore;
using SixMinApi.Models;

//Creating different methods

namespace SixMinApi.Data
{
    public class CommandRepo : ICommandRepo //Implement Interface
    {
        private AppDbContext _context; //Private read-only field

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
            await _context.AddAsync(cmd);
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
            return await _context.Commands.ToListAsync();
        }

        public async Task<Command?> GetCommandById(int id)
        {
            //throw new NotImplementedException();
            return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChanges()
        {
            //throw new NotImplementedException();
            await _context.SaveChangesAsync();
        }
    }
}