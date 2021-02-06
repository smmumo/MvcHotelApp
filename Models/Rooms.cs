using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MvcHotelApp.Models{

    public class Rooms{
        
        [Key]
        public int room_id { get; set; }       
       // [Required]
        //[MaxLength(50,ErrorMessage="Name can not exceed 50 characters")]
       
        //[Required]
        //[RegularExpression()]
        public string resort_id { get; set;}
        public string destination_id { get; set;}
        public string country_code { get; set;}
        public double price { get; set; }
        public string RoomNotes { get; set; }
        public int max_children { get; set; }
        public int min_children { get; set; }
        public int max_adults { get; set; }
        public int number_of_adults { get; set; }
        public int number_of_children { get; set; }
        public int min_adults { get; set; }
        public int hotel_id{get; set;}        
        public Hotel Hotels { get; set; }
        public int room_type_id { get; set; }
        public RoomTypes RoomTypes {get;set;}      
        //https://entityframework.net/one-to-one-relationship

    }
}