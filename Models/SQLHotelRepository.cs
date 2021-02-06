

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcHotelApp.Data;

namespace MvcHotelApp.Models{

    public class SQLHotelRepository : IHotelRepository
    {
        private readonly AppDbContext context;
        //public AppDbContext Context { get; }
        public SQLHotelRepository(AppDbContext context)
        {
            this.context = context;
            //this.context = context;
        }       

        public Hotel Add(Hotel hotel)
        {
            //throw new System.NotImplementedException();
            context.dbhotel.Add(hotel);
            context.SaveChanges();
            return hotel;
        }

        public Hotel Delete(int id)
        {
            Hotel hotel=context.dbhotel.Find(id);
            if(hotel!=null){
                context.dbhotel.Remove(hotel);
                context.SaveChanges();
            }
            return hotel;
            //throw new System.NotImplementedException();
        }
        public IEnumerable<Hotel> GetAllHotel()
        {
          return  context.dbhotel;
            //throw new System.NotImplementedException();
        }       
        public Hotel GetHotels(int Id)
        {
            return context.dbhotel.Find(Id);
            //throw new System.NotImplementedException();
        }
        public Hotel GetHotelsById(int Id)
        {
            //throw new System.NotImplementedException();
             return context.dbhotel.Find(Id);
        }

        public Hotel Update(Hotel hotelChange)
        {
            var hotels=context.dbhotel.Attach(hotelChange);
            hotels.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return hotelChange;
            //throw new System.NotImplementedException();
        }
    }
}
