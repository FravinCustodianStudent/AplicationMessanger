using System.ComponentModel.DataAnnotations;
using AplicationMessanger.Areas.Identity.Data;
namespace AplicationMessanger.Models.Entity
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public string Avatar { get; set; }
        public List<AplicationMessangerUser> Users { get; set; }
    }
}
