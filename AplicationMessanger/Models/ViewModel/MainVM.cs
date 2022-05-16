using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Models.Entity;

namespace AplicationMessanger.Models.ViewModel
{
    public class MainVM
    {
        public AplicationMessangerUser User { get; set; }
        public Chat chat { get; set; }
        public List<Message> ChatMessages { get; set; }
       
        public List<Chat> Chats { get; set; }

        public List<AplicationMessangerUser> Users { get; set; }
    }

}
