using AplicationMessanger.Areas.Identity.Data;

namespace AplicationMessanger.Models.ViewModel
{
    public class UserFormVM
    {
        public List<AplicationMessangerUser> Users { get; set; }
        public string SearchLine { get; set; }
    }
}
