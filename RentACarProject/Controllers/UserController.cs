using Microsoft.AspNetCore.Mvc;
using RentACarProject.Data.Models;
using RentACarProject.Data;

namespace RentACarProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Users> users = _context.Users.ToList();

            return View(users);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Users user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public IActionResult Delete(Users user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var obj = _context.Users.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _context.Users.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Edit(int? id)
        {
            var obj = _context.Cars.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Users user)
        {
            var obj = _context.Users.Find(id);

            if (id == null)
            {
                return NotFound();
            }

            //obj.Username = user.Username;
            //obj.Password = user.Password;
            //obj.FirstName = user.FirstName;
            //obj.LastName = user.LastName;
            //obj.PIN = user.PIN;
            //obj.PhoneNumber = user.PhoneNumber;
            //obj.Email = user.Email;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
