namespace CoffeeTrackerApi.Services
{
    public class EmailSettings
    {
        public string FromName { get; set; }

        public string FromAddress { get; set; }

        public string ToName { get; set; }

        public string ToAddress { get; set; }

        public string GmailUsername { get; set; }

        public string GmailPassword { get; set; }
    }
}
