using System.Reflection.Metadata;

namespace SignalRMVC.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? ReceiverName { get; set; }
        public string? MessageText { get; set; }
        public string? Date { get; set; }
    }
}
