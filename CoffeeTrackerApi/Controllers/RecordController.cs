using CoffeeTrackerApi.Models;
using CoffeeTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CoffeeTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly IRepository<Record> db;
        private readonly string recordsCacheKey = "records";
        private IMemoryCache _memoryCache;
        private ILogger<RecordController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public RecordController(IRepository<Record> _db,
            IMemoryCache memoryCache,
            ILogger<RecordController> logger)
        {
            db = _db;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var record = await db.Get(id);

            if (record is null)
                return NotFound();

            return record;
        }



        [HttpGet]
        public async Task<ActionResult<List<Record>>> GetAll()
        {
            _logger.Log(LogLevel.Information, "trying to fetch the list of records from cache.");
            if (_memoryCache.TryGetValue(recordsCacheKey, out IEnumerable<Record> records))
            {
                _logger.Log(LogLevel.Information, "Record List found in cache.");
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (_memoryCache.TryGetValue(recordsCacheKey, out records))
                    {
                        _logger.Log(LogLevel.Information, "Record List found in cache.");

                    }
                    else
                    {
                        _logger.Log(LogLevel.Information, "Record List not found in cache. Fetching from database.");

                        records = await db.GetAll();


                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal);

                        _memoryCache.Set(recordsCacheKey, records, cacheEntryOptions);
                    }
                }
                finally
                {
                    semaphore.Release();
                }

            }
            return Ok(records.ToList());
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Record>>> GetByName([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term))
                return BadRequest("name parameter is required.");

            var records = (await db.GetAll()).Where(r => new[] { r.Name, r.Description, r.Type, r.Cost.ToString(), r.ConsumingDate.ToString() }.Any(f => f.Contains(term))).ToList();

            if (records.Count == 0)
                return NotFound($"No records found with the name {term}");

            return records;
        }


        [HttpPost]
        public async Task<IActionResult> Create(Record record)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await db.Add(record);

            _memoryCache.Remove(recordsCacheKey);

            return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Record record)
        {
            if (record is null || record.Id != id)
                return BadRequest();

            var existingRecord = db.Get(id);
            if (existingRecord is null)
                return NotFound();

            await db.Update(record);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var record = await db.Get(id);
            if (record is null) return NotFound();

            await db.Delete(id);
            return NoContent();
        }

    }
}
