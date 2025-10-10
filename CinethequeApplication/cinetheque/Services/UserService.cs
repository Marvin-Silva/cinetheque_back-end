using cinetheque.Models;
using cinetheque.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinetheque.Services
{
    public interface IUserService

    {
        void createUser(UserDto user);
        bool login(User user);
        UserInfo getUserInfo(int id);
        List<LocationDto> getUserLocations(int id);
    }
}
