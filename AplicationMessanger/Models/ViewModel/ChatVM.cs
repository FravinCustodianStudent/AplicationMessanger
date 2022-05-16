using System.ComponentModel.DataAnnotations;

namespace AplicationMessanger.Models.ViewModel
{
    public class ChatVM
    {
        [Required]
        public IFormFile Avatar { get; set; }

        public string UserId { get; set; }
        [Required]
        public string ChatName { get; set; }
    }
}
