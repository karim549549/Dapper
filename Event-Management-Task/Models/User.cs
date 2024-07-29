namespace Event_Management_Task.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password_hash { get; set; }

        public DateTime  Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public string Role { get; set; }
    }
}
