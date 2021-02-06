using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MvcHotelApp.Models{

    public class Hotel{
        
        [Key]        
        public int Id { get; set; }
        //[Required]
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "HotelName")]
        public string hotel_name { get; set; }        
        public int hotel_id{get; set;}
        public int tticode{get; set;}
       // [Required]
        //[MaxLength(50,ErrorMessage="Name can not exceed 50 characters")]       
        //[Required]
        //[RegularExpression()]
        [Required]  
        [Display(Name = "Resorts")]     
        public string resort_id { get; set; }
        [Required]
        [Display(Name = "Destinations")]
        public string destination_id { get; set; }
        public string country_code { get; set; }
        [Required]
        public string city { get; set; }
        public string description { get; set; }
        public string iso_code_1 { get; set; }
        public string latitude { get; set; }
        public string logitude { get; set; }
        public string zip_Code { get; set; }
        public string hotel_addr_1 { get; set; }
        public virtual ICollection<Rooms> Rooms{get;set;}
       
        //[Required] //question mark means is optional,for validation
       // public Dept? Department { get; set; }
        // public string PhotoPath { get; set; }

    }
}