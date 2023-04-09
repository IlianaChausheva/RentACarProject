using Microsoft.AspNetCore.Mvc;
using RentACarProject.Data.Models;
using RentACarProject.Data;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet]
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


        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        //public IActionResult Delete(Users user)
        //{
        //    _context.Users.Remove(user);
        //    _context.SaveChanges();

        //    return RedirectToAction("index");
        //}

        //[HttpPost]
        //public IActionResult Delete(int? id)
        //{
        //    var obj = _context.Users.Find(id);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(obj);
        //    _context.SaveChanges();
        //    return RedirectToAction("index");
        //}

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Edit(int? id)
        //{
        //    var obj = _context.Cars.Find(id);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(obj);
        //}

        //[HttpPost]
        //public IActionResult Edit(int? id, Users user)
        //{
        //    var obj = _context.Users.Find(id);

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //obj.Username = user.Username;
        //    //obj.Password = user.Password;
        //    //obj.FirstName = user.FirstName;
        //    //obj.LastName = user.LastName;
        //    //obj.PIN = user.PIN;
        //    //obj.PhoneNumber = user.PhoneNumber;
        //    //obj.Email = user.Email;

        //    _context.SaveChanges();
        //    return RedirectToAction("index");
        //}

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,  Users user) //[Bind("Id,FirstName,LastName,UserName,PIN,PhoneNumber,Email")]
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
