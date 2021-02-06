using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcHotelApp.Models;
using MvcHotelApp.ViewModels;
using MvcProject.Models;

namespace MvcHotelApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        //private readonly ILogger<HomeController> _logger;

        public AccountsController(UserManager<IdentityUser> userManager
        ,SignInManager<IdentityUser> signInManager )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            //_logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        [HttpGet]
        public IActionResult Register()
        {      

            return View();
        }

        [HttpGet]
        public IActionResult login()
        {      

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {        
            if(ModelState.IsValid){
                var user=new IdentityUser{UserName=registerViewModel.Email,Email=registerViewModel.Email};

               var result=await userManager.CreateAsync(user,registerViewModel.Password);
                if(result.Succeeded){
                    if(signInManager.IsSignedIn(User) && User.IsInRole("Admin")){
                         return RedirectToAction("ListUsers","Administration");
                    }
                    await signInManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("index","Account");
                }
                //if not success get all errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }

            }    
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel loginViewModel,
        string returnUrl)
        {        
            if(ModelState.IsValid){               

               var result=await signInManager.PasswordSignInAsync(
                   loginViewModel.Email,
                   loginViewModel.Password,
                   loginViewModel.RememberMe,false);//.CreateAsync(user,registerViewModel.Password);
                if(result.Succeeded){  
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)){
                        return Redirect(returnUrl);
                    }else{
                        return RedirectToAction("index","Hotels");
                    }               
                }                           
                ModelState.AddModelError(string.Empty,"Invalid Login Attempt");              

            }    
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(){
            await signInManager.SignOutAsync();
           return RedirectToAction("index","Hotels");            
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(){
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
