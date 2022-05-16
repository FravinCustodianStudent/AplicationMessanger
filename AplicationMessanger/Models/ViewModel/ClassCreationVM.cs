using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Models.Entity;

namespace AplicationMessanger.Models.ViewModel
{
    public class ClassCreationVM
    {
        public List<string> UsersNames { get; set; }
        public List<AplicationMessangerUser> UsersFind = new List<AplicationMessangerUser>();
        public List<AplicationMessangerUser> AlreadyIsinChat = new List<AplicationMessangerUser>();
        public AplicationMessangerUser User { get; set; }

        public string IdUser { get; set; }
        
        public string SeacrhLine { get; set; }
        public Chat CurrentChat { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
