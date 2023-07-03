using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class RowsController : Controller
    {
        private readonly OsfmspoContext _context;

        public RowsController(OsfmspoContext context)
        {
            _context = context;
        }

        // GET: Rows
        public async Task<IActionResult> Index()
        {
            var osfmspoContext = _context.Rows;
            return View(await osfmspoContext.ToListAsync());
        }

        // GET: Rows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rows == null)
            {
                return NotFound();
            }

            var row = await _context.Rows
                .Include(r => r.MaterialNavigation)
                .FirstOrDefaultAsync(m => m.RowId == id);
            if (row == null)
            {
                return NotFound();
            }

            return View(row);
        }

        // GET: Rows/Create
        public IActionResult Create()
        {
            ViewData["Material"] = new SelectList(_context.MaterialMasters, "Code", "Code");
            return View();
        }

        // POST: Rows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RowId,Material,Quantity,Price")] Row row)
        {
            if (ModelState.IsValid)
            {
                _context.Add(row);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Material"] = new SelectList(_context.MaterialMasters, "Code", "Code", row.Material);
            return View(row);
        }

        // GET: Rows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rows == null)
            {
                return NotFound();
            }

            var row = await _context.Rows.FindAsync(id);
            if (row == null)
            {
                return NotFound();
            }
            ViewData["Material"] = new SelectList(_context.MaterialMasters, "Code", "Code", row.Material);
            return View(row);
        }

        // POST: Rows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RowId,Material,Quantity,Price")] Row row)
        {
            if (id != row.RowId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(row);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RowExists(row.RowId))
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
            ViewData["Material"] = new SelectList(_context.MaterialMasters, "Code", "Code", row.Material);
            return View(row);
        }

        // GET: Rows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rows == null)
            {
                return NotFound();
            }

            var row = await _context.Rows
                .Include(r => r.MaterialNavigation)
                .FirstOrDefaultAsync(m => m.RowId == id);
            if (row == null)
            {
                return NotFound();
            }

            return View(row);
        }

        // POST: Rows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rows == null)
            {
                return Problem("Entity set 'OsfmspoContext.Rows'  is null.");
            }
            var row = await _context.Rows.FindAsync(id);
            if (row != null)
            {
                _context.Rows.Remove(row);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RowExists(int id)
        {
          return (_context.Rows?.Any(e => e.RowId == id)).GetValueOrDefault();
        }
    }
}
