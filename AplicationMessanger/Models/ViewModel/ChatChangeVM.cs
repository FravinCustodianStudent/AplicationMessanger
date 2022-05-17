using AplicationMessanger.Areas.Identity.Data;
using AplicationMessanger.Models.Entity;
using Microsoft.Build.Framework;

namespace AplicationMessanger.Models.ViewModel
{
    public class ChatChangeVM
    {
        public Chat? CurrentChat { get; set; }
        
        public string Name { get; set; }

        public string ChatId { get; set; }
       
        public IFormFile NewAvatar { get; set; }
    }
}
