using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTrackerApi.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ConsumingnDate { get; set; }


    }
}
