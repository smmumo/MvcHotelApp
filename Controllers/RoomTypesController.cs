using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcHotelApp.Data;
using MvcHotelApp.Models;

namespace MvcHotelApp.Controllers
{
    public class RoomTypesController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbcontext;

        public RoomTypesController(AppDbContext context)
        {
            //_logger = logger;
            _dbcontext=context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ActivePage = "Hotel list";
            ViewBag.pageTitle="Hotels";
            var roomType=await(_dbcontext.RoomTypes.ToListAsync());
            //ViewBag["Page"]="hotels";
           // ViewBag["Page"]="hotels";
            return View(roomType);
        }

        [HttpPost]
        public async Task<IActionResult> create([Bind("roomTypeName")] RoomTypes roomTypes){
            if(ModelState.IsValid){
               _dbcontext.RoomTypes.Add(roomTypes);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  
            }
            return View();                     
        }

        [HttpGet]
        public ViewResult create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id){
            var roomTypes=await _dbcontext.RoomTypes.FindAsync(id);
            if(roomTypes==null){
                return NotFound();
            }
            _dbcontext.Remove(roomTypes);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]      
        public async Task<IActionResult> Edit(int id){
            var roomTypes=await _dbcontext.RoomTypes.FindAsync(id);
            if(roomTypes==null){
                return NotFound();
            }
            return View(roomTypes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("room_type_id,roomTypeName")] RoomTypes roomTypes){
            // var dbroomstypes=await _dbcontext.RoomTypes.FindAsync(id);
            // if(dbroomstypes==null){
            //     return NotFound();
            // }
            if(ModelState.IsValid){
                 _dbcontext.Update(roomTypes);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }  
            return View();        
           
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
