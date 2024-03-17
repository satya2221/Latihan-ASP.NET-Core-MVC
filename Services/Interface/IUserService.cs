using WebMVC.Models;

namespace WebMVC.Services.Interface
{
    public interface IUserService
    {
        void AddUser(MstUser user);
        List<MstUser> GetUser();
        MstUser GetUserById(int id);
        void UpdateUser(MstUser user); 
        void DeleteUser(int id);
    }
}
