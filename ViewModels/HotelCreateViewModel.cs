using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class HotelCreateViewModel{    
    //    [Required]    
       [Display(Name="HotelName")]    
       public string hotel_name { get; set; }  
          
       [Display(Name="City")]  
        public string city { get; set; }
        // [Required]
        [Display(Name="Resorts")]
        public string resort_id { get; set; }
        // [Required]
        [Display(Name="Destination")]
        public string destination_id { get; set; }
   }
}