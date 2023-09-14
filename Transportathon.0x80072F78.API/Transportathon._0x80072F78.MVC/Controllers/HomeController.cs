using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;
using Refit;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.MVC.Models;
using Transportathon._0x80072F78.Services;
using Transportathon._0x80072F78.Services.ForCompany;

namespace Transportathon._0x80072F78.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var drivers = _appDbContext.Drivers.ToList();
            return View(drivers);
        }
        [HttpGet]
        public IActionResult CreateDriver()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateDriver2(DriverUpdateDTO driverUpdateDTO)
        {
            try
            {
                var refitAPI = RestService.For<IRefitAPI>($"https://localhost:44304/api/Transportathon/Driver/Update", new RefitSettings
                {
                    AuthorizationHeaderValueGetter = (_, __) => Task.FromResult("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwOGFlN2RlNy1jZDYzLTQ3NzItOGVmNi03YzkzYzU2MjkxYmIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImJiNjYxMmFjLWYyNGEtNGQ2My05ZWYxLTkxOTIzMjQ4NzY0MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzZXJoYXQiLCJ1bmlxdWVfbmFtZSI6InNlcmhhdCIsImF1ZCI6WyJ3d3cudHJhbnNwb3J0YXRob24uY29tIiwidHJhbnNwb3J0YXRob24iXSwibmJmIjoxNjk0NzA1MDExLCJleHAiOjE5MDAxMTA0NTEsImlzcyI6InRyYW5zcG9ydGF0aG9uIn0.Q3K8C98HctLtKFf_IZvw-B9j17gPTeblv8pAfz5fKoGcNute19kt77Ngiu82MckTzzNaFz-EbKIzgpENawwTZzsCaDzr5AJjrEnxArv4idfJTfzA5439alWWl1mUVAwIQP_B4jAiIlrF-f7MiEtcZRpRQIGZj6pknllFXfzaErc84b3Grw19BGS93m8qtEebPa3ozEaaH65EXFfUp9Ej0irBQYQKsVUYC3xAxwcY_aw8ewkMQzOAaLN1PeUTwDgfuVi6_Rv4erLCfKYP1uneKVzCC5Ak-Qf0WGoomprhCU5By_h3I5RS60dyDlzIQX3C29kgh-dHjGfqS9k_91mJxg")
                });
                var result = await refitAPI.UpdateDriver(driverUpdateDTO);
                if (result.StatusCode != StatusCodes.Status200OK)
                {
                    //popup gibi bişeyle hata dönülmeli
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateDriver(DriverCreateDTO driverCreateDTO)
        {
            try
            {
                var refitAPI = RestService.For<IRefitAPI>($"https://localhost:44304/api/Transportathon/Driver/Create", new RefitSettings
                {
                    AuthorizationHeaderValueGetter = (_, __) => Task.FromResult("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwOGFlN2RlNy1jZDYzLTQ3NzItOGVmNi03YzkzYzU2MjkxYmIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImJiNjYxMmFjLWYyNGEtNGQ2My05ZWYxLTkxOTIzMjQ4NzY0MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzZXJoYXQiLCJ1bmlxdWVfbmFtZSI6InNlcmhhdCIsImF1ZCI6WyJ3d3cudHJhbnNwb3J0YXRob24uY29tIiwidHJhbnNwb3J0YXRob24iXSwibmJmIjoxNjk0NzA1MDExLCJleHAiOjE5MDAxMTA0NTEsImlzcyI6InRyYW5zcG9ydGF0aG9uIn0.Q3K8C98HctLtKFf_IZvw-B9j17gPTeblv8pAfz5fKoGcNute19kt77Ngiu82MckTzzNaFz-EbKIzgpENawwTZzsCaDzr5AJjrEnxArv4idfJTfzA5439alWWl1mUVAwIQP_B4jAiIlrF-f7MiEtcZRpRQIGZj6pknllFXfzaErc84b3Grw19BGS93m8qtEebPa3ozEaaH65EXFfUp9Ej0irBQYQKsVUYC3xAxwcY_aw8ewkMQzOAaLN1PeUTwDgfuVi6_Rv4erLCfKYP1uneKVzCC5Ak-Qf0WGoomprhCU5By_h3I5RS60dyDlzIQX3C29kgh-dHjGfqS9k_91mJxg")
                });
                var result = await refitAPI.CreateDriver(driverCreateDTO);
                if (result.StatusCode != StatusCodes.Status200OK)
                {
                    //popup gibi bişeyle hata dönülmeli
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteDriver(Guid id)
        {
            try
            {
                var refitAPI = RestService.For<IRefitAPI>($"https://localhost:44304/api/Transportathon/Driver/Delete?id={id}", new RefitSettings
                {
                    AuthorizationHeaderValueGetter = (_, __) => Task.FromResult("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwOGFlN2RlNy1jZDYzLTQ3NzItOGVmNi03YzkzYzU2MjkxYmIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImJiNjYxMmFjLWYyNGEtNGQ2My05ZWYxLTkxOTIzMjQ4NzY0MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzZXJoYXQiLCJ1bmlxdWVfbmFtZSI6InNlcmhhdCIsImF1ZCI6WyJ3d3cudHJhbnNwb3J0YXRob24uY29tIiwidHJhbnNwb3J0YXRob24iXSwibmJmIjoxNjk0NzA1MDExLCJleHAiOjE5MDAxMTA0NTEsImlzcyI6InRyYW5zcG9ydGF0aG9uIn0.Q3K8C98HctLtKFf_IZvw-B9j17gPTeblv8pAfz5fKoGcNute19kt77Ngiu82MckTzzNaFz-EbKIzgpENawwTZzsCaDzr5AJjrEnxArv4idfJTfzA5439alWWl1mUVAwIQP_B4jAiIlrF-f7MiEtcZRpRQIGZj6pknllFXfzaErc84b3Grw19BGS93m8qtEebPa3ozEaaH65EXFfUp9Ej0irBQYQKsVUYC3xAxwcY_aw8ewkMQzOAaLN1PeUTwDgfuVi6_Rv4erLCfKYP1uneKVzCC5Ak-Qf0WGoomprhCU5By_h3I5RS60dyDlzIQX3C29kgh-dHjGfqS9k_91mJxg")
                });
                var result = await refitAPI.DeleteDriver(id);
                if (result.StatusCode != StatusCodes.Status200OK)
                {
                    //popup gibi bişeyle hata dönülmeli
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                        
                throw ex;
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}