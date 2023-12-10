using CoffeeTrackerApi.Models;
using CoffeeTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackerController : ControllerBase
    {
        IRepository<Record> db; 
        public TrackerController(IRepository<Record> _db) 
        {
            db = _db;
        }

        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            var record = db.Get(id);

            if (record is null)
                return NotFound();

            return record;
        }

        [HttpGet]
        public ActionResult<List<Record>> GetAll() => db.GetAll().ToList();

        [HttpPost]
        public IActionResult Create(Record record)
        {
            db.Add(record);
            return CreatedAtAction(nameof(Get), new {id = record.Id}, record);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Record record) 
        {
            if (record is null || record.Id != id)
                return BadRequest();

            var existingRecord = db.Get(id);
            if (existingRecord is null)
                return NotFound();

            db.Update(record);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var record = db.Get(id);
            if (record is null) return NotFound();

            db.Delete(id);
            return NoContent();
        }

    }
}
