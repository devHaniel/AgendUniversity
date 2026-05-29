using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthApiService _authApiService;

    public AuthController(ILogger<AuthController> logger, IAuthApiService authApiService)
    {
        _logger = logger;
        _authApiService = authApiService;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        var response = await _authApiService.LoginAsync(model);
        
        if (response is null)
        {
            ModelState.AddModelError("", "Credenciales incorrectas");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, response.Usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, response.Usuario.Email)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        TokenHelper.GuardarToken(HttpContext, response.Token);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        var response = await _authApiService.RegisterAsync(model);

        if (response is null)
        {
            ModelState.AddModelError("", "Error al registrar usuario");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, response.Usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, response.Usuario.Email)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        TokenHelper.GuardarToken(HttpContext, response.Token);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        TokenHelper.EliminarToken(HttpContext);

        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }
}