using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MvcHotelApp.Models{
    public class RoomTypes{
        
        [Key]
        public int room_type_id { get; set; }

        [Required]
        public string roomTypeName{get; set;}
        public ICollection<Rooms> Rooms{get;set;}
       // [Required]
        //[MaxLength(50,ErrorMessage="Name can not exceed 50 characters")]   
    }
}