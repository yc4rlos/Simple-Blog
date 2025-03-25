using System.Security.Claims;
using Blog.Application.Core.UseCases.Auth.Commands.DefinePassword;
using Blog.Application.Core.UseCases.Auth.Commands.Login;
using Blog.Application.Core.UseCases.Auth.Commands.RecoverPassword;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Presentation.Web.Controllers;

[Route("[controller]")]
public class AuthController(ISender sender) : Controller
{

    [HttpGet]
    public IActionResult LoginAsync()
    {
        var message = TempData["Message"] as string;
        ViewBag.Message = message;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginCommand command)
    {
        var resultLogin = await sender.Send(command);

        if (!resultLogin.Success)
        {
            ViewBag.Error = true;
            return View();
        }
        
        var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        var identity = new ClaimsIdentity(resultLogin.Claims, authScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(authScheme, principal, new AuthenticationProperties
        {
            IsPersistent = command.RememberMe
        });

        if (resultLogin.ResetPassword)
        {
            return RedirectToAction("UpdatePassword");
        }
        
        return RedirectToAction("Index", "Admin");
    }

    [HttpGet("recover-password")]
    public IActionResult RecoverPassword()
    {
        return View();
    }

    [HttpPost("recover-password")]
    public async Task<IActionResult> RecoverPasswordAsync(RecoverPasswordCommand command)
    {
        try
        {
            await sender.Send(command);
        }
        catch (UserNotFoundException ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
        
        TempData["Message"] = "Recover password email sent.";
        return RedirectToAction("Login");
    }

    [Authorize]
    [HttpGet("update-password")]
    public IActionResult UpdatePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost("update-password")]
    public async Task<IActionResult> DefinePasswordAsync(DefinePasswordCommand command)
    {
        await sender.Send(command);
        return RedirectToAction("Index", "Admin");
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}