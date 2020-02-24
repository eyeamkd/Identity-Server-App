using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController( 
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager; 
        }
        [HttpGet] 
        public IActionResult Login(string returnUrl)
        {
            return View( new LoginViewModel { ReturnUrl = returnUrl } );

        } 

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
           var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }else if (result.Succeeded)
            {

            }
            return View(); 
        }
    }
}
