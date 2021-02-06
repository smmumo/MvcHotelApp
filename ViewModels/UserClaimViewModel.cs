using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class UserClaimViewModel{ 
       public UserClaimViewModel(){
           Claims=new List<UserClaim>();
       }       
       public string UserId { get; set; }     
       public string  RoleName { get; set; }
       public List<UserClaim> Claims{ get; set; }
   }
}