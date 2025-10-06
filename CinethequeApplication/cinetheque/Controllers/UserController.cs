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
    [RoutePrefix("api/v1")]
    public class UserController : ApiController
    {
        private IUserService _userSrv;

        public UserController()
        {
            _userSrv = new UserServiceImpl();
        }

        [Route("create/user")]
        [HttpPost]
        public void CreateUser([FromBody]UserDto user)
        {
            _userSrv.createUser(user);
        }

        [Route("login/user")]
        [HttpGet]
        public bool loginUser([FromBody]User user)
        {
            return _userSrv.login(user);
        }

        [Route("get/user/info")]
        [HttpGet]
        public UserInfo getUserInfo([FromUri] int id)
        {
            return _userSrv.getUserInfo(id);
        }
    }
}
