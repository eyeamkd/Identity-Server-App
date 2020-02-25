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
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController( 
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
           
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });

        } 
        [HttpPost]
        public async Task<IActionResult>  Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            IdentityUser user = new IdentityUser(registerViewModel.Username);
            var result = await _userManager.CreateAsync(user, registerViewModel.Password); 

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(registerViewModel.ReturnUrl);
            }
            return View();
        }
    }
}
