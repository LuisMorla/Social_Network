using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.Services;
using Social_Network.Core.Application.ViewModels.Friends;
using Social_Network.Core.Application.ViewModels.Publications;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Middlewares;

namespace Social_Network.Controllers
{
    public class FriendController : Controller
    {
        private readonly ValidateUserSession _Validate;
        private readonly IUserService _userServices;
        private readonly IPublicationService _publication;
        private readonly IFriendService _friend;
        private readonly ICommentService _comment;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;


        public FriendController(IFriendService friend, IUserService userService, ValidateUserSession validate, IPublicationService publication, IHttpContextAccessor httpContextAccessor, ICommentService commentService)
        {
            _friend = friend;
            _userServices = userService;
            _Validate = validate;
            _publication = publication;
            _httpContextAccessor = httpContextAccessor;
            _comment = commentService;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }

            var GetUser = await _userServices.GetAllViewModelWithInclude();

            var user = GetUser.FirstOrDefault(x => x.Id == HttpContext.Session.Get<UserViewModel>("user").Id);


            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string UserName)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }


            SaveFriendViewModel vm = new();

            var GetUser = await _userServices.GetAllViewModelWithInclude();
            var user = GetUser.FirstOrDefault(x => x.Id == HttpContext.Session.Get<UserViewModel>("user").Id);

            if (!await _userServices.ValidateUserName(UserName))
            {
                ModelState.AddModelError("Username", "The username doesn't exist");


                return View("Index");
            }

            var us = await _userServices.GetUserbyUsername(UserName);
            vm.UserFirst = HttpContext.Session.Get<UserViewModel>("user").Id;
            vm.UserSecond = us.Id;

            if (us.Id == vm.UserFirst)
            {

                ModelState.AddModelError("Username", "You can't add you.");


                return View("Index", user);

            }

            if (await _friend.CheckAreFriend(vm) != null)
            {

                ModelState.AddModelError("Username", "You and this user are friend.");
                return View();

            }

            await _friend.Add(vm);
            return RedirectToRoute(new { controller = "Friend", action = "GetAllPublicationsFriend" });
        }

        public async Task<IActionResult> GetAllPublicationsFriend()
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }


            return View("PublicationsFriend", await _friend.GetAllViewModelWithInclude());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }


            return View(await _friend.GetByIdSave(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }

            await _friend.Delete(id);
            return RedirectToRoute(new { Controller = "Friend", Action = "GetAllPublicationsFriend" });
        }

        public async Task<IActionResult> GetAllPublicationesWithFilters()
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }

            ViewBag.Comment = await _comment.GetAllViewModelWithInclude();
            List<PublicationViewModel> ListPublication = new();
            List<FriendViewModel> FriendsList = await _friend.GetAll();
            List<PublicationViewModel> PublicationsView = await _publication.GetAll();

            foreach(var friend in FriendsList){
                foreach(var Post in PublicationsView){

                    if(Post.UserId == friend.UserSecond && friend.UserSecond != _user.Id && Post.UserId != friend.UserFirst)
                    {
                        ListPublication.Add(Post);
                    }else if(Post.UserId == friend.UserFirst && Post.UserId != friend.UserSecond  && _user.Id == friend.UserSecond)
                    {
                        ListPublication.Add(Post);
                    }
                }
            }
            return View(ListPublication);
        }

    }
}
