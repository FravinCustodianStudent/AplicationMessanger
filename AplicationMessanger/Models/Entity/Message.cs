namespace AplicationMessanger.Models.Entity
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
    }
}
