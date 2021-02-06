using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MvcHotelApp.Models{
    public interface IRoomTypesRepository{
       RoomTypes GetRoomTypes(int Id);
       IEnumerable<RoomTypes> GetAllRoomTypes();
        RoomTypes Add(RoomTypes roomstypes);
        RoomTypes Update(RoomTypes roomtypesChange);
        RoomTypes Delete(int id);
    }
}