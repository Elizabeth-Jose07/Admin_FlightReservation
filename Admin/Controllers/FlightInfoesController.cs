using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace Admin.Controllers
{
    [Authorize]
    public class FlightInfoesController : Controller
    {
        private readonly Admin_Context _context;

        public FlightInfoesController(Admin_Context context)
        {
            _context = context;
        }

        // GET: FlightInfoes
        public async Task<IActionResult> Index()
        {
            var admin_Context = _context.FlightInfos.Include(f => f.Flight);
            return View(await admin_Context.ToListAsync());
        }

        // GET: FlightInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightInfo = await _context.FlightInfos
                .Include(f => f.Flight)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flightInfo == null)
            {
                return NotFound();
            }

            return View(flightInfo);
        }

        // GET: FlightInfoes/Create
        public IActionResult Create()
        {
            var flightDetails = _context.Flightdetails.Where(fs => fs.Isactive != false);
            ViewData["FlightNumber"] = new SelectList(flightDetails, "FlightNumber", "FlightName");
            return View();
        }

        // POST: FlightInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,FlightNumber,Source,Destination,DepartureDate,ArrivalDate,Bseats,Eseats,IsActive")] FlightInfo flightInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var flightDetails = _context.Flightdetails.Where(fs => fs.Isactive != false);
            ViewData["FlightNumber"] = new SelectList(flightDetails, "FlightNumber", "FlightName" ,flightInfo.FlightNumber);
            //ViewData["FlightNumber"] = new SelectList(_context.Flightdetails, "FlightNumber", "FlightName", flightInfo.FlightNumber);
            return View(flightInfo);
        }

        // GET: FlightInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightInfo = await _context.FlightInfos.FindAsync(id);
            if (flightInfo == null)
            {
                return NotFound();
            }
            ViewData["FlightNumber"] = new SelectList(_context.Flightdetails, "FlightNumber", "FlightModel", flightInfo.FlightNumber);
            return View(flightInfo);
        }

        // POST: FlightInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,FlightNumber,Source,Destination,DepartureDate,ArrivalDate,Bseats,Eseats,IsActive")] FlightInfo flightInfo)
        {
            if (id != flightInfo.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Check if the isActive field has been set to false

                    _context.Update(flightInfo);
                    await _context.SaveChangesAsync();

                    

                      
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightInfoExists(flightInfo.FlightId))
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
            ViewData["FlightNumber"] = new SelectList(_context.Flightdetails, "FlightNumber", "FlightModel", flightInfo.FlightNumber);
            return View(flightInfo);
        }

        // GET: FlightInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightInfo = await _context.FlightInfos
                .Include(f => f.Flight)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flightInfo == null)
            {
                return NotFound();
            }

            return View(flightInfo);
        }

        // POST: FlightInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightInfo = await _context.FlightInfos.FindAsync(id);
            _context.FlightInfos.Remove(flightInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightInfoExists(int id)
        {
            return _context.FlightInfos.Any(e => e.FlightId == id);
        }
    }
}
