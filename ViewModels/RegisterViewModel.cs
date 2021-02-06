using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class RegisterViewModel{  
       [Required]
       [EmailAddress]
       //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
       public string Email { get; set; }

       [Required]
       [DataType(DataType.Password)]      
       public string Password { get; set; }

       [Required]
       [DataType(DataType.Password)]
       [Display(Name="Confirm password")]
       [Compare("Password",ErrorMessage="Password and confirmation password do not match")]
       public string ConfirmPassword { get; set; }       

   }
}