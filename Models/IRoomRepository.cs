using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MvcHotelApp.Models{

    public interface IRoomRepository{
       Rooms GetRooms(int Id);
       IEnumerable<Rooms> GetAllRooms();
       Rooms Add(Rooms rooms);
       Rooms Update(Rooms roomChange);
       Rooms Delete(int id);

    }
}