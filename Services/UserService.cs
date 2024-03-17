using Microsoft.EntityFrameworkCore;
using WebMVC.Models;
using WebMVC.Services.Interface;

namespace WebMVC.Services
{
    public class UserService : IUserService
    {
        readonly SchoolScaffoldContext _dbContext;

        public UserService(SchoolScaffoldContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(MstUser user)
        {
            try
            {
                bool isDuplicate = _dbContext.MstUsers
                .Any(x => x.UserName == user.UserName);
                if (!isDuplicate)
                {
                    _dbContext.Add(user);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Data duplication happen");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Tidak dapat menambahkan data user, ${ex.Message}");
            }
            
        }

        public void DeleteUser(int id)
        {
            try
            {
                var dataUser = _dbContext.MstUsers
                    .Where(x=> x.Id == id)
                    .FirstOrDefault();
                if (dataUser == null) 
                {
                    throw new Exception("Data tidak ditemukan");
                }
                _dbContext.Remove(dataUser);
                _dbContext.SaveChanges();

            }
            catch(Exception ex) 
            {
                throw new Exception($"Gagal hapus data: {ex.Message}");
            }
        }

        public List<MstUser> GetUser()
        {
            try
            {
                var users = _dbContext.MstUsers
                   .ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Tidak dapat memuat seluruh data user : {ex.Message}");
            }

        }

        public MstUser GetUserById(int id)
        {
            try
            {
                var user = _dbContext.MstUsers
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("Data tidak ditemukan");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Tidak dapat memuat data user : {ex.Message}");
            }
        }

        public void UpdateUser(MstUser user)
        {
            try
            {
                _dbContext.MstUsers
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdate(setter =>setter
                        .SetProperty(u => u.UserName, user.UserName)
                        .SetProperty(u => u.Pekerjaan, user.Pekerjaan)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"Tidak dapat mengubah data user : {ex.Message}");
            }
        }
    }
}
