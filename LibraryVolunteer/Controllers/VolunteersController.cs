using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryDomain.Model;
using LibraryVolunteer;

namespace LibraryVolunteer.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly DbLibraryContext _context;

        public VolunteersController(DbLibraryContext context)
        {
            _context = context;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index","Countries");
            //знаходження волонтерів за країною
            ViewBag.CountryCode = id;
            ViewBag.CountryName = name;
            var volunteersByCountry = _context.Volunteers.Where(v => v.CountryCode == id).Include(v => v.CountryCodeNavigation);

            return View(await volunteersByCountry.ToListAsync());
        }

        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.CountryCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // GET: Volunteers/Create
        public IActionResult Create(int countryCode)
        {
            //ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryCode", "CountryCode");
            ViewBag.CountryCode = countryCode;
            ViewBag.CountryName = _context.Countries.Where(c => c.CountryCode == countryCode).FirstOrDefault()?.CountryName;
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int countryCode, [Bind("Id,FullName,Email")] Volunteer volunteer)
        {
            volunteer.CountryCode = countryCode;
            if (ModelState.IsValid)
            {
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Volunteers", new
                {
                    id = countryCode,
                    name = _context.Countries.Where(c => c.CountryCode == countryCode).FirstOrDefault()?.CountryName
                });
            }
            //ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryCode", "CountryCode", volunteer.CountryCode);
            //return View(volunteer);
            return RedirectToAction("Index", "Volunteers", new
            {
                id = countryCode,
                name = _context.Countries.Where(c => c.CountryCode == countryCode).FirstOrDefault()?.CountryName
            });
        }
        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryCode", "CountryCode", volunteer.CountryCode);
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,Email,CountryCode,Id")] Volunteer volunteer)
        {
            if (id != volunteer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteer.Id))
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
            ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryCode", "CountryCode", volunteer.CountryCode);
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.CountryCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(e => e.Id == id);
        }
    }
}
