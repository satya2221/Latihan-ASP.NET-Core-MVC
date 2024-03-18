using LatihanMVC_DAL.Models;
using LatihanMVC_DAL.Models.Dto.Req;
using LatihanMVC_DAL.Models.Dto.Res;

namespace LatihanMVC_DAL.Repositories.Services.Interface
{
    public interface IUserService
    {
        void AddUser(ReqUserDto user);
        List<ResUserDto> GetUser();
        ResUserDto GetUserById(int id);
        void UpdateUser(ReqUserDto user); 
        void DeleteUser(int id);
    }
}
