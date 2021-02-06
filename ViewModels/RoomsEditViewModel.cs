using System.Collections;
using System.Linq;
namespace MvcHotelApp.ViewModels{        
    public class RoomsEditViewModel :RoomsCreateViewModel{
        public int Id { get; set; }     
        public string PageTitle{get;set;}  
    }
}