using EvaraMVC.Modals;
using EvaraMVC.ViewModel.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EvaraMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM register)
    {
        if (!ModelState.IsValid)
        { 
            return View(register);
        }
        AppUser newUser = new AppUser()
        {
            Name = register.Name,
            Surname = register.Surname,
            Email = register.Email,
            UserName = register.UserName,
        };
        IdentityResult identityResult = await _userManager.CreateAsync(newUser,register.Password);
        if(!identityResult.Succeeded)
        {
            foreach (IdentityError? error in identityResult.Errors)
            {
                ModelState.AddModelError("",error.Description);
                return View(register);
            }
        }
        return RedirectToAction(nameof(Login));
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }
        AppUser user = await _userManager.FindByNameAsync(login.UserName);
        if (user is null)
        {
            ModelState.AddModelError("", "Invalid Username or Password");
            return View(login);
        }
        //_signInManager.SignInAsync(user,);
        Microsoft.AspNetCore.Identity.SignInResult signInResult =  await _signInManager.PasswordSignInAsync(user,login.Password,true,false);
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Invalid Username or Password");
            return View(login);
        }
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
