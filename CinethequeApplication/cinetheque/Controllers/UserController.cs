using cinetheque.Models;
using cinetheque.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cinetheque.Controllers
{
    public class UserController : ApiController
    {
        private IUserService _userSrv;

        public UserController()
        {
            _userSrv = new UserServiceImpl();
        }

        [Route("api/v1/create/user")]
        [HttpPost]
        public void createUser([FromBody]User user)
        {
            _userSrv.createUser(user);
        }

        [Route("api/v1/login/user")]
        [HttpGet]
        public bool loginUser([FromBody]User user)
        {
            return _userSrv.login(user);
        }
    }
}
