using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class UserRoleViewModel{        
       public string UserId { get; set; }
       //[Required(ErrorMessage="Role Name is required")]
       public string  UserName { get; set; }
       public bool  IsSelected { get; set; }
   }
}