using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcHotelApp.Models;
using MvcHotelApp.ViewModels;

namespace MvcHotelApp.Controllers
{
    //[Authorize(Roles="Admin,User")]
   
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<AdministrationController> logger;

        //private readonly ILogger<AdministrationController> _logger;

        public AdministrationController(
                RoleManager<IdentityRole> roleManager
               ,UserManager<IdentityUser> userManager
               ,ILogger<AdministrationController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
            //_logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListRoles","Administration"); 
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users=userManager.Users;
            return View(users);
        }

        [HttpPost]
         public async Task<IActionResult> DeleteUsers(string id)
        {
            var users=await userManager.FindByIdAsync(id);
            if(users==null){
                ViewBag.ErrorMessage="User not found";
                return View("NotFound");
            }else{
                var result=await userManager.DeleteAsync(users);
                if(result.Succeeded){
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                    
                }
                return View("ListUsers");
            }
        }

        [HttpPost]
        //[Authorize(Policy="DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role=await roleManager.FindByIdAsync(id);
            if(role==null){
                ViewBag.ErrorMessage="Role not found";
                return View("NotFound");
            }else{
                try{
                     var result=await roleManager.DeleteAsync(role);
                    if(result.Succeeded){
                        return RedirectToAction("ListRoles");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                        
                    }
                   return View("ListRoles");

                }catch(DbUpdateException ex){
                    logger.LogError($"Error deleting role {ex}");
                    ViewBag.ErrorTitle="Role in use";
                    ViewBag.ErrorMessage="role can not be deleted as there are users";
                    return View("Error");
                }
               
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userid)
        {
            ViewBag.userId=userid;
            var user=await userManager.FindByIdAsync(userid);
            if(user==null){
                ViewBag.ErrorMessage="User not found";
                return View("NotFound");
            }
            var model=new List<UserRolesViewModel>();
            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel=new UserRolesViewModel{
                    RoleId=role.Id,
                    RoleName=role.Name,                  
                };
                if(await userManager.IsInRoleAsync(user,role.Name)){
                    userRolesViewModel.IsSelected=true;
                }else{
                      userRolesViewModel.IsSelected=false;
                } 
                model.Add(userRolesViewModel);
            }
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model,string userid)
        {
            //ViewBag.userId=userid;
            var user=await userManager.FindByIdAsync(userid);
            if(user==null){
                ViewBag.ErrorMessage="User not found";
                return View("NotFound");
            }
            var roles=await userManager.GetRolesAsync(user);
            //rm all role with this user
            var result=await userManager.RemoveFromRolesAsync(user,roles);
            if(!result.Succeeded){
                ModelState.AddModelError("","can not remove user existing roles");
                return View(model);
            }
            result = await userManager.AddToRolesAsync(
                user,model.Where(x=>x.IsSelected).Select(y=>y.RoleName));
                if(!result.Succeeded){
                      ModelState.AddModelError("","can not add selected role existing user");
                    return View(model);
                }
            return RedirectToAction("EditUsers",new {id = userid});
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId){
            var user= await userManager.FindByIdAsync(userId);
            if(user==null){
                ViewBag.ErrorMessage="User not found";
                return View("NotFound");
            }
            var existingUserClaims= await userManager.GetClaimsAsync(user);
            //create an instance of view model class and populate it
            var model=new UserClaimViewModel{
                UserId=userId,               
            };
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim=new UserClaim{
                    ClaimType=claim.Type
                };

                if(existingUserClaims.Any(c=>c.Type==claim.Type)){
                    userClaim.IsSelected=true;
                }
                model.Claims.Add(userClaim);                
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimViewModel model){
            var user= await userManager.FindByIdAsync(model.UserId);
            if(user==null){
                ViewBag.ErrorMessage="User not found";
                return View("NotFound");
            }
            var claims= await userManager.GetClaimsAsync(user);
            var result=await userManager.RemoveClaimsAsync(user,claims);
            //create an instance of view model class and populate it
           if(!result.Succeeded){
                 ModelState.AddModelError("","can not remove user existing claims");
                  return View(model);
           }
           result=await userManager.AddClaimsAsync(user,
             model.Claims.Where(c=>c.IsSelected).Select(c=> new Claim(c.ClaimType,c.ClaimType)));
           
            if(!result.Succeeded){
                 ModelState.AddModelError("","can not add claim existing user");
                  return View(model);
            }
            return RedirectToAction("EditUsers",new {id=model.UserId});
        }


        [HttpGet]
        public async Task<IActionResult> EditUsers(string id)
        {
            var users=await userManager.FindByIdAsync(id);
            if(users==null){
                ViewBag.ErrorMessage="User not found";
                return View("NotFound");
            }
            var userClaims=await userManager.GetClaimsAsync(users);
            var userRoles=await userManager.GetRolesAsync(users);
            var model=new EditUserViewModel{
                Id=users.Id,
                Email=users.Email,
                UserName=users.UserName,
                Roles=userRoles,
                Claims=userClaims.Select(c=>c.Value).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsers(EditUserViewModel model)
        {
            var users=await userManager.FindByIdAsync(model.Id);
            if(users==null){
                ViewBag.ErrorMessage="User not found";
                return View("NotFound");
            }
            else{
                users.Email=model.Email;
                users.UserName=model.UserName;
                var result=await userManager.UpdateAsync(users);
                if(result.Succeeded){
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors){
                    ModelState.AddModelError("",error.Description);
                }    
                 return View(model);          
             }           
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid){
                IdentityRole identityRole=new IdentityRole{
                    Name=model.RoleName
                };

             IdentityResult result=await roleManager.CreateAsync(identityRole);
             if(result.Succeeded){
                 return RedirectToAction("ListRoles","Administration"); 
             }
             foreach (IdentityError error in result.Errors)
             {
                 ModelState.AddModelError("",error.Description);
                 
             }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles() {
            var roles=roleManager.Roles;
            return View(roles);
        }

        [HttpPost]
        //[Authorize(Policy="EditRolePolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model){
            var result=await roleManager.FindByIdAsync(model.id);
            if(result==null){
                return View("NotFound");
            }else{
                result.Name=model.RoleName;
                var rs=await roleManager.UpdateAsync(result);
                if(rs.Succeeded){
                   return RedirectToAction("ListRoles","Administration"); 
                }
                foreach (var error in rs.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View(model);
            }           
            
        }
        [HttpGet]
        // [Authorize(Policy="EditRolePolicy")]
        public async Task<IActionResult> EditRole(string id){
            var result=await roleManager.FindByIdAsync(id);
            if(result==null){
                return View("NotFound");
            }
            var model=new EditRoleViewModel{
                id=result.Id,
                RoleName=result.Name
            };
            foreach(var user in userManager.Users){
               if(await userManager.IsInRoleAsync(user,result.Name)){
                   model.Users.Add(user.UserName);
               }
                
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleid){
            ViewBag.roleid=roleid;
            var role=await roleManager.FindByIdAsync(roleid);
            if(role==null){
                ViewBag.ErrorMessage=$"role with Id ={roleid} not found";
                return View("NotFound");
            }
            var model=new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel=new UserRoleViewModel{
                    UserId=user.Id,
                    UserName=user.UserName
                };
                if(await userManager.IsInRoleAsync(user,role.Name)){
                    userRoleViewModel.IsSelected=true;
                }else{
                     userRoleViewModel.IsSelected=false;
                }
                model.Add(userRoleViewModel);
                
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model,string roleid)
        {
            var role=await roleManager.FindByIdAsync(roleid);
            if(role==null){
                ViewBag.ErrorMessage=$"role with Id ={roleid} not found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user=await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result=null;

                var inrole=await userManager.IsInRoleAsync(user,role.Name);
                if(model[i].IsSelected && !inrole)
                {
                   result=await userManager.AddToRoleAsync(user,role.Name);
                }else if(!model[i].IsSelected && inrole){
                    result=await userManager.RemoveFromRoleAsync(user,role.Name);
                }else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if(i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole",new {Id=roleid});
                }
                
            }

            //return View();
             return RedirectToAction("EditRole",new {Id=roleid});
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
