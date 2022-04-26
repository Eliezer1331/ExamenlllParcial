using Datos.Interfaces;
using Datos.Repositorios;
using Examen3Parcial.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Examen3Parcial.Controllers;

public class LoginControllers : Controller
{
    private readonly MySQLConfiguration _configuration;
    private IUsuarioRepositorio _usuarioRepositorio;

    public LoginControllers(MySQLConfiguration configuration)
    {
        _configuration = configuration;
        _usuarioRepositorio = new UsuarioRepositorio(configuration.CadenaConexion);
    }

    [HttpPost("/account/login")]

    public async Task<IActionResult> Login(Login login)
    {
        string rol = string.Empty;
        try
        {
            bool usuarioValido = await _usuarioRepositorio.ValidaUsuario(login);
            if (usuarioValido)
            {
                
                var claims = new[]
                {
                   // new Claim(ClaimTypes.Name, usuarioValido.NombreUsuario),
                    new Claim(ClaimTypes.Role, rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                    });
            }
            else
            {
                return LocalRedirect("/login/Datos de usuario invalido");
            }
        }
        catch (Exception ex)
        {
            return LocalRedirect("/login/Datos de usuario invalido");
        }
        return LocalRedirect("/");
    }

    [HttpGet("/account/logout")]
    public async Task<IActionResult> logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/");
    }
}
