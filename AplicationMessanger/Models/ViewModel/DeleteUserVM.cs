using AplicationMessanger.Areas.Identity.Data;

namespace AplicationMessanger.Models.ViewModel
{
    public class DeleteUserVM
    {
        public string ChatId { get; set; }
        public string UserId { get; set; }
        public List<AplicationMessangerUser> Users { get; set; }
    }
}
