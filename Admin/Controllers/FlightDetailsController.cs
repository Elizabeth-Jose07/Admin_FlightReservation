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
    public class FlightDetailsController : Controller
    {
        private readonly Admin_Context _context;

        public FlightDetailsController(Admin_Context context)
        {
            _context = context;
        }

        // GET: FlightDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flightdetails.ToListAsync());
        }

        // GET: FlightDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightDetail = await _context.Flightdetails
                .FirstOrDefaultAsync(m => m.FlightNumber == id);
            if (flightDetail == null)
            {
                return NotFound();
            }

            return View(flightDetail);
        }

        // GET: FlightDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlightDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightNumber,FlightName,FlightModel,Isactive")] FlightDetail flightDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flightDetail);
        }

        // GET: FlightDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightDetail = await _context.Flightdetails.FindAsync(id);
            if (flightDetail == null)
            {
                return NotFound();
            }
            return View(flightDetail);
        }

        // POST: FlightDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightNumber,FlightName,FlightModel,Isactive")] FlightDetail flightDetail)
        {
            if (id != flightDetail.FlightNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(flightDetail);
                    await _context.SaveChangesAsync();

                        //Get all the flightInfos whose FlightNumber match the Id of flightDetails and updates the isActive value
                        //_context.FlightInfos.Where(e => e.FlightNumber == id).BatchUpdate(new Item { isActive = false })

                        foreach (var prod in _context.FlightInfos)
                        {
                            if (prod.FlightNumber == id)
                            {
                                prod.IsActive = flightDetail.Isactive ;
                            }

                        }
                        await _context.SaveChangesAsync();
                    
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightDetailExists(flightDetail.FlightNumber))
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
            return View(flightDetail);
        }

        // GET: FlightDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightDetail = await _context.Flightdetails
                .FirstOrDefaultAsync(m => m.FlightNumber == id);
            if (flightDetail == null)
            {
                return NotFound();
            }

            return View(flightDetail);
        }

        // POST: FlightDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightDetail = await _context.Flightdetails.FindAsync(id);
            //_context.Flightdetails.Remove(flightDetail);
            flightDetail.Isactive = false;
            _context.Update(flightDetail);

            foreach (var prod in _context.FlightInfos)
            {
                if (prod.FlightNumber == id)
                {
                    prod.IsActive = flightDetail.Isactive;
                }

            }
            //await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightDetailExists(int id)
        {
            return _context.Flightdetails.Any(e => e.FlightNumber == id || e.Isactive!=false);
        }
    }
}
