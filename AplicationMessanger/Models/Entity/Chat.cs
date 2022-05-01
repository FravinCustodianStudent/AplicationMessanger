using System.ComponentModel.DataAnnotations;
using AplicationMessanger.Areas.Identity.Data;
namespace AplicationMessanger.Models.Entity
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AplicationMessangerUser> Users { get; set; }
    }
}
