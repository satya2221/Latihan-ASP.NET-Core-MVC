using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services.Interface;

namespace WebMVC.Controllers
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
        public IActionResult TambahUser(MstUser user)
        {
            _service.AddUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FormEditUser(int id)
        {
            var dataUser = _service.GetUserById(id);
            return View(dataUser);
        }

        [HttpPost]
        public IActionResult EditUser(MstUser user)
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
