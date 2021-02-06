using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace MvcHotelApp.Models{

    public static class ClaimsStore{
        public static List <Claim> AllClaims=new List<Claim>(){
            new Claim("Create Role","Creat Role"),            
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Role","Delete Role"),
        };
    }
}