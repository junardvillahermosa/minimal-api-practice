using Microsoft.EntityFrameworkCore;
using SixMinApi.Models;

//Creating different methods

namespace SixMinApi.Data
{
    public class PlatformRepo : IPlatformRepo //Implement Interface
    {
        private readonly AppDbContext _context; //Private read-only field

        //Create constructors 
        public PlatformRepo(AppDbContext context) //parameters
        {
            _context = context;
            
        }
        public async Task CreatePlatform(Platform pm)
        {
            //throw new NotImplementedException();
            if(pm == null){
                throw new ArgumentNullException(nameof(pm));
            }
            await _context.Platforms!.AddAsync(pm);
        }

        public void DeletePlatform(Platform pm)
        {
            //throw new NotImplementedException();
            if(pm == null){
                throw new ArgumentNullException(nameof(pm));
            }
            _context.Platforms.Remove(pm);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms()
        {
            //throw new NotImplementedException();
            return await _context.Platforms!.ToListAsync();
        }

        public async Task<Platform?> GetPlatformById(int id)
        {
            //throw new NotImplementedException();
            return await _context.Platforms!.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdatePlatform(Platform pm)
        {
            var existingPlatform = await _context.Platforms!.FirstOrDefaultAsync(p => p.Id == pm.Id);

            if (existingPlatform != null)
            {
                // You can update the properties of the existingCommand based on the cmd parameter
                existingPlatform.PlatformName = pm.PlatformName;

                // Mark the entity as modified to ensure it gets updated in the database
                _context.Entry(existingPlatform).State = EntityState.Modified;
            }
            else
            {
                throw new InvalidOperationException("Platform not found.");
            }
        }

        public async Task SaveChanges()
        {
            //throw new NotImplementedException();
            await _context.SaveChangesAsync();
        }
    }
}