using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
namespace MvcHotelApp.Models{
    public class MockHotelRepository : IHotelRepository{
     private List<Hotel> _hotellist;

        public MockHotelRepository()
        {
            _hotellist=new List<Hotel>(){
                new Hotel(){
                    hotel_id=1,hotel_name="Atlantis Palm",hotel_addr_1="123",
                    latitude="12",logitude="12",
                    resort_id="12",city="msa",country_code="12",iso_code_1="003",
                    description="dfdsds",destination_id="233",
                },
                 new Hotel(){
                    hotel_id=2,hotel_name="paris Palm",hotel_addr_1="123",latitude="12",
                    logitude="12",
                    resort_id="12",city="msa",country_code="12",iso_code_1="003",
                    description="dfdsds",destination_id="233",

                }
            };
        }

        public Hotel Add(Hotel hotel)
        {
            hotel.hotel_id=_hotellist.Max(e=>e.hotel_id)+1;
            _hotellist.Add(hotel);
            return hotel;
            //throw new NotImplementedException();
        }

        public Hotel Delete(int id)
        {
            Hotel hotels=_hotellist.FirstOrDefault(e=>e.hotel_id==id);
            if(hotels!=null){
                _hotellist.Remove(hotels);
            }
            return hotels;
            //throw new NotImplementedException();
        }

        public IEnumerable<Hotel> GetAllHotel()
        {
            return _hotellist;
            //throw new NotImplementedException();
        }

        public Hotel GetHotels(int Id)
        {
            return _hotellist.FirstOrDefault(e=>e.hotel_id==Id);
            //throw new NotImplementedException();
        }

        public Hotel GetHotelsById(int Id)
        {
            throw new NotImplementedException();
        }

        public Hotel Update(Hotel hotelChange)
        {
            Hotel hotels=this._hotellist.FirstOrDefault(e=>e.hotel_id==hotelChange.hotel_id);
            if(hotels!=null){
                hotels.hotel_name=hotelChange.hotel_name;
            }
            return hotels;
            //throw new NotImplementedException();
        }
    }
   

}