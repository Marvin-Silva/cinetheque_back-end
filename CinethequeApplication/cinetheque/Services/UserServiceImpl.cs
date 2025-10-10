using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cinetheque.Models;
using cinetheque.Models.DAO;
using cinetheque.Models.DTO;

namespace cinetheque.Services
{
    public class UserServiceImpl : IUserService
    {
        private LocationService locationSrv;

        public UserServiceImpl()
        {
            this.locationSrv = new LocationService();
        }

        public void createUser(UserDto user)
        {
            new UserDAO().InsertUser(user);
        }

        public List<LocationDto> getUserLocations(int id)
        {
            return this.locationSrv.getUserLocations(id);
        }

        public UserInfo getUserInfo(int id)
        {
            return new UserDAO().SelectUserInfo(id);
        }

        public bool login(User user)
        {
            return new UserDAO().SelectUser(user);
        }
    }
}