using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class CreateRoleViewModel{  
       [Required]
       public string  RoleName { get; set; }
   }
}