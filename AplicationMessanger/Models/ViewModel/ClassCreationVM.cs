using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Models.Entity;

namespace AplicationMessanger.Models.ViewModel
{
    public class ClassCreationVM
    {
        public List<AplicationMessangerUser> UsersFind = new List<AplicationMessangerUser>();
        public List<AplicationMessangerUser> AlreadyIsinChat = new List<AplicationMessangerUser>();
        public string SeacrhLine { get; set; }
        public Chat CurrentChat { get; set; }
    }
}
