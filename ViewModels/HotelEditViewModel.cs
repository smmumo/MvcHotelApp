using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace MvcHotelApp.ViewModels{
        
    public class HotelEditViewModel :HotelCreateViewModel{
        public int Id { get; set; }     
        public string PageTitle{get;set;}  
    }
}