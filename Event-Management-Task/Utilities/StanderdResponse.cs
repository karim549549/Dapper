namespace Event_Management_Task.Utilities
{
    public class StanderdResponse
    {
        public string? Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; } = new object();

    }
}
