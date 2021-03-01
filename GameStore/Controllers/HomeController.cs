﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {       
        [HttpGet]
        [Route("getInfo")]        
        //[Authorize(Roles = UserRoles.Administrator.ToString())]
        [Authorize(Roles = "admin")]
        public ActionResult<string> GetInfo()
        {
            return "Hello World";
        }
    }
}
