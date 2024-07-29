namespace Event_Management_Task.DTOs.User
{
    public class RegisterDto
    {
        public string FirstName {  get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
