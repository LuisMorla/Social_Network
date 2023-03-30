using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Application.Helpers;

namespace Social_Network.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel vm = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if(vm == null)
            {
                return false;
            }

            return true;
        }
    }
}
