namespace Event_Management_Task.Utilities
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double expireInMinutes { get; set; }

    }
}
