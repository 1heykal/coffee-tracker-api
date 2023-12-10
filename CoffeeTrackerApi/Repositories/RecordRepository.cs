using CoffeeTrackerApi.Models;

namespace CoffeeTrackerApi.Repositories
{
    public class RecordRepository : IRepository<Record>
    {
        ApplicationDbContext dbContext;
        public RecordRepository() 
        {
            dbContext = new();
        }

        public void Add(Record entity)
        {
            dbContext.Records.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var record = Get(id);
            if (record is null)
                return;

            dbContext.Records.Remove(record);
            dbContext.SaveChanges();
        }

        public Record? Get(int id) => dbContext.Records.FirstOrDefault<Record>(r => r.Id == id);

        public IEnumerable<Record> GetAll() => dbContext.Records.ToList();
       

        public void Update(Record entity)
        {
            var record = dbContext.Records.FirstOrDefault(r => r.Id == entity.Id);
            if (record is null)
                return;

            ApplicationDbContext db = new();
            db.Records.Update(entity);
            db.SaveChanges();

        }
    }
}
