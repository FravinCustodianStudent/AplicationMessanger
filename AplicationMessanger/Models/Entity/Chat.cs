using System.ComponentModel.DataAnnotations;
using AplicationMessanger.Areas.Identity.Data;
namespace AplicationMessanger.Models.Entity
{
    public class Chat
    {

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public string Avatar { get; set; }
        public List<AplicationMessangerUser> Users { get; set; } = new List<AplicationMessangerUser>();
    }
}
