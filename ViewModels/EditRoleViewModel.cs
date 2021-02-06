using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class EditRoleViewModel{ 
       public EditRoleViewModel()
       {
           Users=new List<string>();
       } 
       public string id { get; set; }

       [Required(ErrorMessage="Role Name is required")]
       public string  RoleName { get; set; }
       public List<string> Users { get; set; }
   }
}