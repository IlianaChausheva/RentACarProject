﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACarProject.Data;
using RentACarProject.Data.Models;
using RentACarProject.Models;

namespace RentACarProject.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rentals.Include(r => r.Car).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }


        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(PeriodViewModel period)
        {
            List<Car> cars = new List<Car>();
            cars=_context.Cars.ToList();
            var carsAvailable= new List<Car>();

            foreach (var car in cars)
            {
                bool available = true;
                foreach (var rental in car.Rentals)
                {
                    if (rental.PickUpDate>=period.PickUpDate && rental.PickUpDate<=period.DropOffDate)
                    {
                        available= false;
                        break;
                    }
                    if (rental.DropOffDate>=period.PickUpDate && rental.DropOffDate<=period.DropOffDate)
                    {
                        available = false;
                        break;
                    }
                    if (rental.PickUpDate<=period.PickUpDate && rental.DropOffDate>=period.DropOffDate)
                    {
                        available = false;
                        break;
                    }

                }
                if (available)
                {
                    carsAvailable.Add(car);
                }
            }


            //Dictionary<int, Car> model= new Dictionary<int, Car>();-
            //foreach (var car in cars)
            //{ model[car.Id] = car; }

            //ViewBag.PickUpDate = period.PickUpDate;
            //ViewBag.DropOffDate = period.DropOffDate;

            var s = Newtonsoft.Json.JsonConvert.SerializeObject(carsAvailable);

            TempData["Model"] = s;
            return carsAvailable != null ?   RedirectToAction("ViewAvailable", "Cars") :
                          Problem("No available cars");
        }


        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            ViewData["UserId"] = GetUserId();
            //??????????
            //ViewData["PickUpDate"] = new SelectList(_context.Rentals, "PickUpDate", "PickUpDate");
            //ViewData["DropOffDate"] = new SelectList(_context.Rentals, "DropOffDate", "DropOffDate");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,UserId,PickUpDate,DropOffDate")] Rental rental)
        {
            if (ModelState.IsValid)
            { // ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", rental.CarId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", rental.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);

            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", rental.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,UserId,PickUpDate,DropOffDate")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", rental.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rentals == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rentals'  is null.");
            }
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
          return (_context.Rentals?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string GetUserId()
        {
            var nsme = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email== nsme);

            return user.Id;
        }
    }
}
