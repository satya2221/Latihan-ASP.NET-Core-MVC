using Microsoft.AspNetCore.Mvc;
using LatihanMVC_DAL.Models;
using LatihanMVC_DAL.Models.Dto.Req;
using LatihanMVC_DAL.Repositories.Services.Interface;

namespace LatihanMVC_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service) { 
            _service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var dataUser = _service.GetUser();
            return View(dataUser);
        }

        [HttpGet]
        public IActionResult FormTambahUser()
        {
            return View();
        }

        [HttpPost]  
        public IActionResult TambahUser(ReqUserDto user)
        {
            if (ModelState.IsValid)
            {
                _service.AddUser(user);
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpGet]
        public IActionResult FormEditUser(int id)
        {
            var dataUser = _service.GetUserById(id);
            return View(dataUser);
        }

        [HttpPost]
        public IActionResult EditUser(ReqUserDto user)
        {
            _service.UpdateUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult Deleteuser (int id)
        {
            _service.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
