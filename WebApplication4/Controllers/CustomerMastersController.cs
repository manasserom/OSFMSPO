using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class CustomerMastersController : Controller
    {
        private readonly OsfmspoContext _context;

        public CustomerMastersController(OsfmspoContext context)
        {
            _context = context;
        }

        // GET: CustomerMasters
        public async Task<IActionResult> Index()
        {
              return _context.CustomerMasters != null ? 
                          View(await _context.CustomerMasters.ToListAsync()) :
                          Problem("Entity set 'OsfmspoContext.CustomerMasters'  is null.");
        }
        // GET: Sing Up and Sing In
        public async Task<IActionResult> SignIn()
        {
            return View();
        }
        // POST: Sing Up and Sing In
        [HttpPost]
        public async Task<IActionResult> SignIn(string Code)
        {
            if (!string.IsNullOrEmpty(Code))
            {
                var registered = _context.CustomerMasters.FindAsync(Code).Result;
                if (registered is null)
                {
                    //TODO: Add error message
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Orders");
                }
                //_context.CustomerMasters
            }
            return View();
        }
                // GET: Sing Up and Sing In
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        // POST: Sing Up and Sing In
        [HttpPost]
        public async Task<IActionResult> SignUp([Bind("Code,Description")] CustomerMaster customerMaster)
        {
            if (!string.IsNullOrEmpty(customerMaster.Code))
            {
                _context.Add(customerMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }
            return View();
        }

        // GET: CustomerMasters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.CustomerMasters == null)
            {
                return NotFound();
            }

            var customerMaster = await _context.CustomerMasters
                .FirstOrDefaultAsync(m => m.Code == id);
            if (customerMaster == null)
            {
                return NotFound();
            }

            return View(customerMaster);
        }

        // GET: CustomerMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Description")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.CustomerMasters == null)
            {
                return NotFound();
            }

            var customerMaster = await _context.CustomerMasters.FindAsync(id);
            if (customerMaster == null)
            {
                return NotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Description")] CustomerMaster customerMaster)
        {
            if (id != customerMaster.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerMasterExists(customerMaster.Code))
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
            return View(customerMaster);
        }

        // GET: CustomerMasters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.CustomerMasters == null)
            {
                return NotFound();
            }

            var customerMaster = await _context.CustomerMasters
                .FirstOrDefaultAsync(m => m.Code == id);
            if (customerMaster == null)
            {
                return NotFound();
            }

            return View(customerMaster);
        }

        // POST: CustomerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.CustomerMasters == null)
            {
                return Problem("Entity set 'OsfmspoContext.CustomerMasters'  is null.");
            }
            var customerMaster = await _context.CustomerMasters.FindAsync(id);
            if (customerMaster != null)
            {
                _context.CustomerMasters.Remove(customerMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerMasterExists(string id)
        {
          return (_context.CustomerMasters?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
