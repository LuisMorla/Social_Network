using AutoMapper;
using Social_Network.Core.Application.Dtos.Email;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.ViewModels.Comments;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;


namespace Social_Network.Core.Application.Services
{
    public class UserService : GenericService<User, UserViewModel, SaveUserViewModel>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICommentRepository _commentRepository;


        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService, ICommentRepository commentRepository) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
            _commentRepository = commentRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel loginvm)
        {
            User user = await _userRepository.LoginAsync(loginvm);

            if (user == null)
            {
                return null;
            }

            UserViewModel userView = _mapper.Map<UserViewModel>(user);
            return userView;
        }

        public async Task<List<UserViewModel>> GetAllViewModelWithInclude()
        {
            var userList = await _userRepository.GetAllAsyncWithInclude(new List<string> { "Publications", "Friends1", "Friends2", "Comments" });
        
            return userList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                ImageUser = user.ImageUser,
                Password = user.Password,
                Phone = user.PhoneNumber

            }).ToList();
        }

        public override async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            SaveUserViewModel model = await base.Add(vm);

            await _emailService.SendAsync(new EmailRequest
            {
                To = model.Email,
                Subject = "Welcome to Social_Network",
                Body = $"<p>Welcome to Social_Network <i>Your username is {model.Username}</i></p> <br> <p> Haga click aquí debajo para confirmar que su cuenta ha sido creada </p>  <a href='https://localhost:7099/User/Verify/{model.Id}'>Confirmar</a>"
            });

            return model;
        }

        public async Task<bool> ValidateUserName(string UserName)
        {
            var validate = await _userRepository.ValidateUser(UserName);

            return validate != null ? true : false;

        }

        public async Task<UserViewModel> GetUserbyUsername(string username)
        {
            var response = await _userRepository.ValidateUser(username);

            return response != null ? _mapper.Map<UserViewModel>(response) : null;
        }


        public async Task ChangePassword(string username)
        {

            var response = await _userRepository.ValidateUser(username);

            var PassGenerate = RecoverPass.RandomPassword(8);

            response.Password = PasswordEncryption.ComputeSha256Hash(PassGenerate);

            await _userRepository.UpdateAsync(response, response.Id);

            await _emailService.SendAsync(new()
            {
                To = response.Email,
                Subject = "Change Password to RedSocialAccount",
                Body = $"Su nueva contraseña es: {PassGenerate} <br> <br> <a href='https://localhost:7099/User/Index'>Click aqui para iniciar sesion</a>"
            });

        }

        public async Task<List<UserViewModel>> GetAllWithInclude()
        {
            var todo = await _commentRepository.GetAllAsyncWithOutInclude();

            var list = await _userRepository.GetAllAsyncWithInclude(new List<string> { "Friends2", "Friends1", "Comments", "Publications" });


            return list.Select(x => new UserViewModel
            {
                Id = x.Id,
                Username = x.UserName,
                Email = x.Email,
                Name = x.Name,
                Phone = x.PhoneNumber,
                ImageUser = x.ImageUser,
                Password = x.Password,
                LastName = x.LastName,
                Comments = _mapper.Map<List<CommentViewModel>>(todo.ToList().Where(p => p.PublicationId == x.Id).ToList()),
            }).ToList();
        }

    }
}

