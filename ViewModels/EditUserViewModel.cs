using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class EditUserViewModel{ 
       public EditUserViewModel()
       {
           Claims=new List<string>();
           Roles=new List<string>();

       } 
       public string Id { get; set; }
       
       [Required]
       public string Email { get; set; }
       //[Required(ErrorMessage="Role Name is required")]
       public string  UserName { get; set; }
       public List<string> Claims { get; set; }
       public IList<string> Roles { get; set; }
   }
}