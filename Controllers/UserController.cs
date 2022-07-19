using Microsoft.AspNetCore.Mvc;

using UserApi.Web.Models;
using UserApi.Web.Repositories;

namespace UserApi.Web.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _userRepository;

        public UserController(ILogger<UserController> logger, UserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult<UserResponseModel> AddUser([FromBody] UserRequestModel model)
        {
            if (String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.Name))
            {
                return BadRequest();
            }

            _userRepository.AddUser(model);

            var responseModel = _userRepository.GetUser(model.Email);
            if (responseModel == null)
            {
                return NotFound();
            }

            return responseModel;
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseModel> GetUser([FromRoute] Guid id)
        {
            var responseModel = _userRepository.GetUser(id);
            if (responseModel == null)
            {
                return NotFound();
            }

            return responseModel;
        }

        [HttpGet("Email/{email}")]
        public ActionResult<UserResponseModel> GetUser([FromRoute] string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var responseModel = _userRepository.GetUser(email);
            if (responseModel == null)
            {
                return NotFound();
            }

            return responseModel;
        }
    }
}

