using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.ViewModels.Publications;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using Social_Network.Infrastructure.Persistence.Contexts;
using Social_Network.Middlewares;
using System.Xml.Linq;

namespace Social_Network.Controllers
{
    public class PublicationController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly IUserService _user;
        private readonly ICommentService _commentservice;
        private readonly ValidateUserSession _Validate;

        public PublicationController(IPublicationService publicationService, ICommentService commentService, IUserService user, ValidateUserSession validateUserSession, IHttpContextAccessor httpContextAccessor)
        {
            _publicationService = publicationService;
            _commentservice = commentService;
            _user = user;
            _Validate = validateUserSession;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<IActionResult> Index()
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Index" });
            }
            ViewBag.Comment = await _commentservice.GetAllViewModelWithInclude();

            var publications = await _publicationService.GetAllWithInclude();
            return View(publications);
        }


        [HttpPost]
        public async Task<IActionResult> Index(SavePublicationViewModel vm /*string contenido, int usuarioId, IFormFile Picture*/)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }

            if (vm.Id == 0 && vm.UserId == 0 && vm.Caption == null && vm.File == null && vm.Picture == null && vm.ImageUser == null)
            {
                ViewBag.Error = "Post, You must add content";
                return RedirectToRoute(new { Controller = "Publication", Action = "Index" });

            }


            SavePublicationViewModel publication = await _publicationService.Add(vm);
            if (vm.File == null && publication.Id != 0)
            {
                publication.Picture = null;

                publication.Caption = vm.Caption;
                await _publicationService.Update(publication, publication.Id);
                return RedirectToRoute(new { Controller = "Publication", Action = "Index" });
            }

            if (publication != null && publication.Id != 0)
            {

                publication.Picture = UploadFile(vm.File, publication.Id);
                await _publicationService.Update(publication, publication.Id);
            }


            return RedirectToRoute(new { Controller = "Publication", Action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }

            return View(await _publicationService.GetByIdSave(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }

            var encypt = await _publicationService.GetByIdSave(id);

            if (encypt.Picture == null)
            {
                await _publicationService.Delete(id);
                return RedirectToRoute(new { Controller = "Publication", Action = "Index" });
            }
            await _publicationService.Delete(id);

            string basePath = $"/image/publication/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (!Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();

                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            return RedirectToRoute(new { Controller = "Publication", Action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }


            return View("Modified", await _publicationService.GetByIdSave(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(SavePublicationViewModel vm)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }


            if (!ModelState.IsValid)
            {
                return View("Modified", vm);
            }

            SavePublicationViewModel Publicationvm = await _publicationService.GetByIdSave(vm.Id);
            vm.ImageUser = Publicationvm.ImageUser;

            vm.Picture = UploadFile(vm.File, Publicationvm.Id, true, Publicationvm.Picture);
            await _publicationService.Update(vm, vm.Id);
            return RedirectToRoute(new { Controller = "Publication", Action = "Index" });
        }


        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string ImageUrl = "")
        {
            if (isEditMode && file == null)
            {
                return ImageUrl;
            }

            //Get Directory Path
            string basePath = $"/image/publication/{id}";
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
            if (isEditMode)
            {
                if (ImageUrl == null)
                {
                    return $"{basePath}/{filename}";
                }
                string[] oldImagePart = ImageUrl.Split('/');
                string oldImageName = oldImagePart[^1];
                string CompleteImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(CompleteImageOldPath))
                {
                    System.IO.File.Delete(CompleteImageOldPath);
                }
            }
            return $"{basePath}/{filename}";
        }


    }
}
