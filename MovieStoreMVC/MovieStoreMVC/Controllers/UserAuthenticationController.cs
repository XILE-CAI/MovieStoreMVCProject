using Microsoft.AspNetCore.Mvc;
using MovieStoreMVC.Models.DTO;
using MovieStoreMVC.Repositories.Abstract;

namespace MovieStoreMVC.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

        //create a user with admin rights.after that we are going to comment this method, cause we only need one user in this application
        /*public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel
            {
                Email = "admin@gmail.com",
                Username = "admin",
                Name = "Bryan",
                Password = "Admin@123",
                PasswordConfirm = "Admin@123",
                Role = "Admin"
            };

            var result = await authService.RegistrationAsync(model);

            return Ok(result.Message);
        }*/

        //get method
        public async Task<IActionResult> Login()
        {
            return View();
        }

        //post method
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //check model validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await authService.LoginAsync(model);
            if(result.StatusCode == 1)
            {
                //index action of Home controller 
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["msg"] = "Failed Login";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
