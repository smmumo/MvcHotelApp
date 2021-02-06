using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Models;

namespace MvcHotelApp.ViewModels{
   public class RoomsCreateViewModel{ 
        public int hotel_id{get; set;}  

        [Display(Name="RoomTypeName")]    
        public string room_type_id { get; set; }
        
        [Display(Name="Price")]
        public double price { get; set; }

        public string RoomNotes { get; set; }

        [Display(Name="NumberOfAdults")]
        public int number_of_adults { get; set; }

        [Display(Name="NumberOfChildren")]
        public int number_of_children { get; set; }
   }
}