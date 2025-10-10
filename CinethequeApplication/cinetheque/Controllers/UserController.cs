using cinetheque.Models;
using cinetheque.Models.DTO;
using cinetheque.Services;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace cinetheque.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/v1")]
    public class UserController : ApiController
    {
        private IUserService _userSrv;
        private ILocationService _locationSrv;

        UserController()
        {
            this._userSrv = new UserServiceImpl();
            this._locationSrv = new LocationService();
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
                session["login"] = userLogin.getLogin;
                session["id"] = userLogin.Id;
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

        [Route("get/user/info/{id}")]
        [HttpGet]
        public UserInfo getUserInfo([FromUri] int id)
        {
            return _userSrv.getUserInfo(id);
        }

        [Route("get/user/article/list/{id}")]
        [HttpGet]
        public List<LocationDto> getUserLocations([FromUri] int id)
        {
            return this._locationSrv.getUserLocations(id);
        }

        [Route("post/rent/articles")]
        [HttpPost]
        public int rentArticles(List<Articles> articles)
        {
           return _locationSrv.rentArticles(articles);

        }
    }
}
