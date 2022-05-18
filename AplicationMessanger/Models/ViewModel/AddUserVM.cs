using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Models.Entity;

namespace AplicationMessanger.Models.ViewModel
{
    public class AddUserVM
    {
        public Chat? CurrentChat { get; set; }
        public List<AplicationMessangerUser>? Users { get; set; }
        public string? UserId { get; set; }
        public string ChatId { get; set; }
        public string? SearchLine { get; set; }
    }
}
