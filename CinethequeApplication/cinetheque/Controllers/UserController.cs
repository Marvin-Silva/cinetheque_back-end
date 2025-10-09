using cinetheque.Models;
using cinetheque.Services;
using System.Web;
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
        [HttpPost]
        public bool loginUser([FromBody]User user)
        {
            bool isConnected = false;
            User userLogin = new User();

            if (user != null)
            {
                userLogin = user;
            }

            isConnected = _userSrv.login(userLogin);

            if (isConnected)
            {
                var session = HttpContext.Current.Session;
                session["username"] = userLogin.getLogin;
                session["isConnected"] = isConnected;
                return isConnected;
            }
            return isConnected;
        }

        [Route("logout/user")]
        [HttpPost]
        public void Logout()
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
        }

        [Route("get/user/info")]
        [HttpGet]
        public UserInfo getUserInfo([FromUri] int id)
        {
            return _userSrv.getUserInfo(id);
        }
    }
}
