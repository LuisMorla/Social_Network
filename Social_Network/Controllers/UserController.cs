using Microsoft.AspNetCore.Mvc;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.Services;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using Social_Network.Middlewares;

namespace Social_Network.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _user;
        private readonly ValidateUserSession _userSession;


        public UserController(IUserService user, ValidateUserSession userSession)
        {
            _user = user;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            if (_userSession.HasUser())
            {
                return RedirectToRoute(new { Controller = "Publication", Action = "Index" });
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel lvm)
        {
            if (!ModelState.IsValid)
            {

                return View(lvm);
            }

            UserViewModel vm = await _user.Login(lvm);

            if (vm != null)
            {
                if (!vm.IsVerified)
                {
                    ModelState.AddModelError("UserValidation", "You must validate your account in the email sent!");
                    return View(lvm);
                }

                HttpContext.Session.Set<UserViewModel>("user", vm);
                return RedirectToRoute(new { controller = "Publication", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Incorrect login details or Incorrect credentials");
            }
            return View(lvm);

        }

        public async Task<IActionResult> Verify(int id)
        {
            SaveUserViewModel vm = await _user.GetByIdSave(id);
            vm.IsVerified = true;

            await _user.Update(vm, id);
            return View();
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { Controller = "User", Action = "Index" });
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string username)
        {

            if (!await _user.ValidateUserName(username))
            {
                ModelState.AddModelError("Username", "The username has been taken.");
                return View("Index");
            }

            else
            {
                await _user.ChangePassword(username);
                ViewBag.ActivationMessage = "The email was send with the new Password";
                return View("Index");
            }
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (_user.ValidateUserName(vm.Username).Result)
            {
                return View(new SaveUserViewModel { Validate = "User already exists" });
            }

            SaveUserViewModel user = await _user.Add(vm);
            if (user != null && user.Id != 0)
            {
                user.ImageUser = UploadFile(vm.File, user.Id);
                await _user.Update(user, user.Id);
            }
            return RedirectToRoute(new { Controller = "User", Action = "Index" });
        }

        public async Task<IActionResult> Information()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }

            var GetUser = await _user.GetAllViewModelWithInclude();

            var userValidate = GetUser.FirstOrDefault(u => u.Id == HttpContext.Session.Get<UserViewModel>("user").Id);
            return View(userValidate);
        }

        private string UploadFile(IFormFile file, int id)
        {
            //Get Directory Path
            string basePath = $"/image/user/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //Create Folder If Not Exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Get File Path
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{basePath}/{filename}";
        }

    }
}
