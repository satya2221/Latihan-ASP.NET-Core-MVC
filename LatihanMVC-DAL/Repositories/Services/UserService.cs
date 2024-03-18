using Microsoft.EntityFrameworkCore;
using LatihanMVC_DAL.Models;
using LatihanMVC_DAL.Models.Dto.Req;
using LatihanMVC_DAL.Models.Dto.Res;
using LatihanMVC_DAL.Repositories.Services.Interface;

namespace LatihanMVC_DAL.Repositories.Services
{
    public class UserService : IUserService
    {
        readonly SchoolScaffoldContext _dbContext;

        public UserService(SchoolScaffoldContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(ReqUserDto user)
        {
            try
            {
                bool isDuplicate = _dbContext.MstUsers
                .Any(x => x.UserName == user.UserName);
                if (!isDuplicate)
                {
                    var dataBaru = new MstUser();
                    dataBaru.UserName = user.UserName;
                    dataBaru.Pekerjaan = user.Pekerjaan;
                    dataBaru.DtAdded = DateTime.Now;
                    dataBaru.DtUpdated = DateTime.Now;

                    _dbContext.Add(dataBaru);
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

        public List<ResUserDto> GetUser()
        {
            try
            {
                var users = _dbContext.MstUsers
                    .Select(x => new ResUserDto
                    {
                        Id = x.Id,
                        Pekerjaan = x.Pekerjaan,
                        UserName = x.UserName,
                    })
                   .ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Tidak dapat memuat seluruh data user : {ex.Message}");
            }

        }

        public ResUserDto GetUserById(int id)
        {
            try
            {
                var user = _dbContext.MstUsers
                    .Where(x => x.Id == id)
                    .Select(x => new ResUserDto
                    {
                        Id = x.Id,
                        Pekerjaan = x.Pekerjaan,
                        UserName = x.UserName,
                    })
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

        public void UpdateUser(ReqUserDto user)
        {
            try
            {
                _dbContext.MstUsers
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdate(setter =>setter
                        .SetProperty(u => u.UserName, user.UserName)
                        .SetProperty(u => u.Pekerjaan, user.Pekerjaan)
                        .SetProperty(u => u.DtUpdated, DateTime.Now)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception($"Tidak dapat mengubah data user : {ex.Message}");
            }
        }
    }
}
