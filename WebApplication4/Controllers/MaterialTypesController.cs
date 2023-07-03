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
    public class MaterialTypesController : Controller
    {
        private readonly OsfmspoContext _context;

        public MaterialTypesController(OsfmspoContext context)
        {
            _context = context;
        }

        // GET: MaterialTypes
        public async Task<IActionResult> Index()
        {
              return _context.MaterialTypes != null ? 
                          View(await _context.MaterialTypes.ToListAsync()) :
                          Problem("Entity set 'OsfmspoContext.MaterialTypes'  is null.");
        }

        // GET: MaterialTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MaterialTypes == null)
            {
                return NotFound();
            }

            var materialType = await _context.MaterialTypes
                .FirstOrDefaultAsync(m => m.MaterialTypeName == id);
            if (materialType == null)
            {
                return NotFound();
            }

            return View(materialType);
        }

        // GET: MaterialTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaterialTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialTypeName")] MaterialType materialType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materialType);
        }

        // GET: MaterialTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MaterialTypes == null)
            {
                return NotFound();
            }

            var materialType = await _context.MaterialTypes.FindAsync(id);
            if (materialType == null)
            {
                return NotFound();
            }
            return View(materialType);
        }

        // POST: MaterialTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaterialTypeName")] MaterialType materialType)
        {
            if (id != materialType.MaterialTypeName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialTypeExists(materialType.MaterialTypeName))
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
            return View(materialType);
        }

        // GET: MaterialTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MaterialTypes == null)
            {
                return NotFound();
            }

            var materialType = await _context.MaterialTypes
                .FirstOrDefaultAsync(m => m.MaterialTypeName == id);
            if (materialType == null)
            {
                return NotFound();
            }

            return View(materialType);
        }

        // POST: MaterialTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MaterialTypes == null)
            {
                return Problem("Entity set 'OsfmspoContext.MaterialTypes'  is null.");
            }
            var materialType = await _context.MaterialTypes.FindAsync(id);
            if (materialType != null)
            {
                _context.MaterialTypes.Remove(materialType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialTypeExists(string id)
        {
          return (_context.MaterialTypes?.Any(e => e.MaterialTypeName == id)).GetValueOrDefault();
        }
    }
}
