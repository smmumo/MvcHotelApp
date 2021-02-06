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
    public class RoomsController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbcontext;

        public RoomsController(AppDbContext context)
        {
            _dbcontext=context;
            //_logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ActivePage = "Room list";
            ViewBag.pageTitle="Rooms";
            var Hrooms=await(_dbcontext.Rooms.ToListAsync());
            //ViewBag["Page"]="hotels";
           // ViewBag["Page"]="hotels";
            return View(Hrooms);
        }
        public async Task<IActionResult> Details(int? id){
            if(id==null){
                return NotFound();
            }
            var roomsDetails=await _dbcontext.Rooms.Include(r=>r.RoomTypes).AsNoTracking().FirstOrDefaultAsync(rm=>rm.room_id==id);
            if(roomsDetails==null){
                return NotFound();
            }
            return View(roomsDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id){
            if(Id==null){
                return NotFound();
            }
            var roomsedit=await _dbcontext.Rooms.FindAsync(Id);
            if(roomsedit==null){
                return NotFound();
            }
            return View(roomsedit);
        }

        [HttpGet]
        public ViewResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("number_of_adults,number_of_children,room_type_id,price")] Rooms rooms
        ){
            try{
                if(ModelState.IsValid){
                    _dbcontext.Add(rooms);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }catch(DbUpdateException){
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                    }
            return View(rooms);
        }   

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("room_type_id,number_of_children,number_of_adults,price")] Rooms rooms){
            // if(id!=rooms.room_id){
            //     return NotFound();
            // }
            if(ModelState.IsValid){
                try{
                    _dbcontext.Update(rooms);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException){
                    ModelState.AddModelError("","Unable to save changes. " +
                "Try again, and if the problem persists, " +
                "see your system administrator.");
                }
            }
            return View();
         }  
        public async Task<IActionResult> Delete(int id){
            var roomsDeleted=await _dbcontext.Rooms.FindAsync(id);
            if(roomsDeleted==null){
                return RedirectToAction(nameof(Index));
            }
            try{
                _dbcontext.Remove(roomsDeleted);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }catch(DbUpdateException){
                 return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
