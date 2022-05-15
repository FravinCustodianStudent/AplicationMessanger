using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AplicationMessanger.Areas.Identity.Data;

namespace AplicationMessanger.Models.Entity
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public int ChatId { get; set; }
        public AplicationMessangerUser User { get; set; }
    }
}
