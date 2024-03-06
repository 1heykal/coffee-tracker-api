using CoffeeTrackerApi.Data;
using CoffeeTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTrackerApi.Repositories
{
    public class RecordRepository : IRepository<Record>
    {
        ApplicationDbContext dbContext;
        public RecordRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task Add(Record entity)
        {
            await dbContext.Records.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var record = await Get(id);
            if (record is null)
                return;

            dbContext.Records.Remove(record);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Record?> Get(int id) => await dbContext.Records.FirstOrDefaultAsync<Record>(r => r.Id == id);

        public async Task<IEnumerable<Record>> GetAll() => await dbContext.Records.ToListAsync();


        public async Task Update(Record entity)
        {
            var record = Get(entity.Id);
            if (record is null)
                return;

            ApplicationDbContext db = new();
            db.Records.Update(entity);
            await db.SaveChangesAsync();

        }
    }
}
