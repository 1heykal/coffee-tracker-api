using Microsoft.AspNetCore.Identity;

namespace CoffeeTrackerApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Gender { get; set; }
        public List<Record> Records { get; set; }

    }
}
