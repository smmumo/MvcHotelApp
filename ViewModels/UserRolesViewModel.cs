using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class UserRolesViewModel{        
       public string RoleId { get; set; }
       //public string UserId { get; set; }
       //[Required(ErrorMessage="Role Name is required")]
       public string  RoleName { get; set; }
       public bool  IsSelected { get; set; }
   }
}