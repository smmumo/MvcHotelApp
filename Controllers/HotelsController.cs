using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcHotelApp.Models;
using MvcHotelApp.ViewModels;

namespace MvcHotelApp.Controllers
{
    [Authorize]
    public class HotelsController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
       private readonly IHotelRepository _hotelRepository;       

        public HotelsController(IHotelRepository hotelRepository)
        {
            //_logger = logger;
            _hotelRepository=hotelRepository;            
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Hotel list";
            ViewBag.pageTitle="Hotels";
            var model=_hotelRepository.GetAllHotel();          
           
            //var hotelmodel=_hotelRepository1.GetAllHotel();
            //ViewBag["Page"]="hotels";
            // ViewBag["Page"]="hotels";
            return View(model);
        }
        public ViewResult Details(int id)
        {
            //Hotel hotel=_hotelRepository.GetHotels(2);            
            HotelDetailsViewModel hoteldatailsviewModel = new HotelDetailsViewModel(){                
                Hotel=_hotelRepository.GetHotelsById(id),
                PageTitle="Hotel Details"                
            };
            // HotelDetailsViewModel hoteldatails=new HotelDetailsViewModel(){
            //    Hotel hotels =_hotelRepository1.GetHotels(2),
            //    PageTitle="Hotel Details",
            // };
            //ViewBag.ActivePage = "Hotel list";
            //ViewBag.pageTitle="Hotels";
            // var model=_hotelRepository.GetAllHotel();
            //var hotelmodel=_hotelRepository1.GetAllHotel();
            return  View(hoteldatailsviewModel);
        }


        [HttpGet]        
        public ViewResult Create(){
            return View();
        }

        [HttpPost]
         //[Authorize]
        public IActionResult Create(Hotel hotelcreate){
            if(ModelState.IsValid){
                Hotel newhotel=_hotelRepository.Add(hotelcreate);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]      
        public ViewResult Edit(int id){
            Hotel newhotel=_hotelRepository.GetHotels(id); 
            HotelEditViewModel hotelEditViewModel=new HotelEditViewModel{
                Id=newhotel.hotel_id,
                hotel_name=newhotel.hotel_name,
                destination_id=newhotel.destination_id,
                resort_id=newhotel.resort_id,  
                city=newhotel.city,
                PageTitle="Edit Hotel",             
            };
            return View(hotelEditViewModel);
        }

        [HttpPost]       
        public IActionResult Edit(HotelEditViewModel edithotel){
            if(ModelState.IsValid){
                Hotel editedhotel=_hotelRepository.GetHotels(edithotel.Id);
                editedhotel.city=edithotel.city;
                editedhotel.hotel_name=edithotel.hotel_name;
                editedhotel.destination_id=edithotel.destination_id;
                editedhotel.resort_id=edithotel.resort_id;
                _hotelRepository.Update(editedhotel);
                return RedirectToAction("Index");
            }
            return View();
        }

        
        public IActionResult DeleteHotel(int id){
            Hotel hotel=_hotelRepository.GetHotelsById(id);
            if(hotel!=null){
                _hotelRepository.Delete(id);
                return RedirectToAction("index");
            }
            return  View("Index");
        }

        // public JsonResult Details()
        // {
        //     Hotel hotel=_hotelRepository.GetHotels(2);
        //     // HotelDetailsViewModel hoteldatails=new HotelDetailsViewModel(){
        //     //     hotel=_hotelRepository.GetHotels(2);
                
        //     // }
        //     // HotelDetailsViewModel hoteldatails=new HotelDetailsViewModel(){
        //     //    Hotel hotels =_hotelRepository1.GetHotels(2),
        //     //    PageTitle="Hotel Details",
        //     // };
        //     //ViewBag.ActivePage = "Hotel list";
        //     //ViewBag.pageTitle="Hotels";
        //     // var model=_hotelRepository.GetAllHotel();
        //     //var hotelmodel=_hotelRepository1.GetAllHotel();
        //     return  Json(hotel);// View();
        // }

        // public IActionResult Privacy()
        // {
        //     return View();
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
