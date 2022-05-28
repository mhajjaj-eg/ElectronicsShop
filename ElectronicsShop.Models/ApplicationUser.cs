using Microsoft.AspNetCore.Identity;

namespace ElectronicsShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}