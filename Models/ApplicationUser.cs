using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcProject.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string City { get; set; }
  }
}