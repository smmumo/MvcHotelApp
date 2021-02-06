using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MvcHotelApp.Models
{
    public interface IHotelRepository
    {
        Hotel GetHotels(int Id);
        Hotel GetHotelsById(int Id);
        IEnumerable<Hotel> GetAllHotel();
        Hotel Add(Hotel hotel);
        Hotel Update(Hotel hotelChange);
        Hotel Delete(int id);
    }
}