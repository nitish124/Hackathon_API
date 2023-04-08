using Hackathon.DAL;
using Hackathon.Model;
using Hackathon.Request;
using Hackathon.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using QRCoder;
using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Hackathon.Controllers
{
    [ApiController]
    [Route("hackathon")]
    [EnableCors("MyPolicy")]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public UserDAL _userDal { get; set; }
        public HomeController(IConfiguration configuration , ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogInformation(configuration.GetConnectionString("ConnectionString").ToString());
            _userDal = new UserDAL(configuration.GetConnectionString("ConnectionString").ToString(), logger);
        }

        [HttpPost]
        [Route("GetUser")]
        public UserResponse GetUser([FromBody]UserRequest userRequest)
        {
            _logger.LogInformation("GetUser ");
            UserResponse userResponse = new UserResponse();
            _logger.LogInformation("GetUser ");
            var user =  _userDal.GetUser(userRequest.UserName, userRequest.Password);
            userResponse.User = user;
            if (user == null)
            {
                userResponse.Status = false;
            }
            else
            {
                userResponse.Status = true;
            }
            return userResponse;
        }

        [HttpPost]
        [Route("SaveUser")]
        public bool SaveUser(SaveUserRequest saveUserRequest)
        {
            return _userDal.SaveUser(saveUserRequest);
 
        }

        [HttpPost]
        [Route("GetUserList")]
        public UsersResponse GetUserList()
        {
            UsersResponse usersResponse = new UsersResponse();
            usersResponse.User = _userDal.GetUsers();
            usersResponse.Status = true;

            return usersResponse;
        }

 
    }
}
