using Microsoft.AspNetCore.Builder;
using DotNetCoreWebAPI.Controllers;
namespace DotNetCoreWebAPI.Routes
{
    public class Route
    {
        public static void Setup(WebApplication app)
        {
            app.MapPost("/encrypt", CloudNCController.PostEncryptAsync);
            app.MapPost("/decrypt", CloudNCController.PostDecryptAsync);
        }
    }

}