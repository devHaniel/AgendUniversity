using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Helpers;

public static class TokenHelper
{
    public static void GuardarToken(HttpContext context, string token)
    {
        context.Session.SetString("JWToken", token);
    }

    public static string ObtenerToken(HttpContext context)
    {
        return context.Session.GetString("JWToken");
    }

    public static void EliminarToken(HttpContext context)
    {
        context.Session.Remove("JWToken");
    }
}