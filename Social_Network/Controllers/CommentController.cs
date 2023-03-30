using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.ViewModels.Comments;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Middlewares;
using System.Drawing;

namespace Social_Network.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ValidateUserSession _Validate;


        public CommentController(ICommentService commentService, ValidateUserSession validateUserSession)
        {
            _commentService = commentService;
            _Validate = validateUserSession;
        }

        public async Task<IActionResult> Index(SaveCommentViewModel vm, string Act, string Cont)
        {
            if (!_Validate.HasUser())
            {
                return RedirectToRoute(new { Controller = "User", Action = "Login" });
            }


            if (!ModelState.IsValid)
            {
                return RedirectToAction(Act, Cont);
            }
            vm.UserId = HttpContext.Session.Get<UserViewModel>("user").Id;
            await _commentService.Add(vm);
            return RedirectToRoute(new { action = Act, controller = Cont });
        }
    }
}
