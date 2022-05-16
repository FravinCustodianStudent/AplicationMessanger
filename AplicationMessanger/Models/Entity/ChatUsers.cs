using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AplicationMessanger.Areas.Identity.Data;

namespace AplicationMessanger.Models.Entity
{
    public class ChatUsers
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        public AplicationMessangerUser User { get; set; }
        public string ChatId { get; set; }

        public Chat Сhat { get; set; }
    }
}
