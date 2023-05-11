using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace workcube_pagos.Controllers
{
    public class usuarioAuth
    {
        public string UserName { get; set; }
        public string Password { get; set; }  
    }

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<UsuarioModel> _userManager;
        private readonly SignInManager<UsuarioModel> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, SignInManager<UsuarioModel> signInManager, UserManager<UsuarioModel> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login(JsonObject data)
        {
            dynamic argData = JsonConvert.DeserializeObject<object>(data.ToString()); 
            

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    argData.username,
                    argData.password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    _logger.LogInformation("user logged in");
                    return Ok(Redirect("/servicios"));
                }
                else
                {
                    return BadRequest("something went wrong by guel :b");
                }
            }
            return BadRequest();
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}