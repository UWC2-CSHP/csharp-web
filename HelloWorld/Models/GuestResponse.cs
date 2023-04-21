using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        public string? Phone { get; set; }

        // add email
        public string? Email { get; set; }

        public bool? WillAttend { get; set; }
    }
}