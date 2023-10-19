using System.Threading.Tasks;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreWebAPI.Controllers
{
    public class CloudNCController
    {
        public static async Task PostEncryptAsync(AppRequest appRequest, HttpContext http)
        {
            var apiService = http.RequestServices.GetRequiredService<APIService>();
            await http.Response.WriteAsync(apiService.EncryptText(appRequest).Result);
        }
        public static async Task PostDecryptAsync(AppRequest appRequest, HttpContext http)
        {
            var apiService = http.RequestServices.GetRequiredService<APIService>();
            await http.Response.WriteAsync(apiService.DecryptText(appRequest).Result);
        }
    }

}
