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
    public class BookingsController : Controller
    {
        private readonly Admin_Context _context;

        public BookingsController(Admin_Context context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var admin_Context = _context.Bookings.Include(b => b.Flight).Include(b => b.User);
            return View(await admin_Context.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.FlightInfos, "FlightId", "FlightNumber");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,NoOfSeats,Amount,BookingDate,CardNo,Validity,Cvv,TranStatus,Remarks,UserId,FlightId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                //if (.Class == "Business class")
                //{
                //    totalprice = ch * 8000;
                //}
                //else
                //{
                //    totalprice = ch * 4000;
                //}
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.FlightInfos, "FlightId", "FlightNumber", booking.FlightId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.FlightInfos, "FlightId", "FlightNumber", booking.FlightId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", booking.UserId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("BookingId,NoOfSeats,Amount,BookingDate,CardNo,Validity,Cvv,TranStatus,Remarks,UserId,FlightId")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
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
            ViewData["FlightId"] = new SelectList(_context.FlightInfos, "FlightId", "FlightNumber", booking.FlightId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(long id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightInfo>>> GetAllFlightInfos(string Source, string Destination)
        {


            if (!String.IsNullOrEmpty(Source) && !String.IsNullOrEmpty(Destination))
            {
                var flights = await _context.FlightInfos.Where(s => s.Source.ToLower()!.Contains(Source.ToLower()) && s.Destination.ToLower()!.Contains(Destination.ToLower())).ToListAsync();
                return flights;

            }
            else if (!String.IsNullOrEmpty(Source))
            {
                var flights = await _context.FlightInfos.Where(s => s.Source.ToLower()!.Contains(Source.ToLower())).ToListAsync();
                return flights;
            }
            else if (!String.IsNullOrEmpty(Destination))
            {
                var flights = await _context.FlightInfos.Where(s => s.Destination.ToLower()!.Contains(Destination.ToLower())).ToListAsync();
                return flights;
            }


            return await _context.FlightInfos.ToListAsync();

        }
}
    }
