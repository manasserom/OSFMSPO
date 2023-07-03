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
    public class MaterialMastersController : Controller
    {
        private readonly OsfmspoContext _context;

        public MaterialMastersController(OsfmspoContext context)
        {
            _context = context;
        }

        // GET: MaterialMasters
        public async Task<IActionResult> Index()
        {
            var osfmspoContext = _context.MaterialMasters.Include(m => m.MaterialTypeNavigation);
            return View(await osfmspoContext.ToListAsync());
        }

        // GET: MaterialMasters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MaterialMasters == null)
            {
                return NotFound();
            }

            var materialMaster = await _context.MaterialMasters
                .Include(m => m.MaterialTypeNavigation)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (materialMaster == null)
            {
                return NotFound();
            }

            return View(materialMaster);
        }

        // GET: MaterialMasters/Create
        public IActionResult Create()
        {
            ViewData["MaterialType"] = new SelectList(_context.MaterialTypes, "MaterialTypeName", "MaterialTypeName");
            return View();
        }

        // POST: MaterialMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Description,MaterialType,Price")] MaterialMaster materialMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialType"] = new SelectList(_context.MaterialTypes, "MaterialTypeName", "MaterialTypeName", materialMaster.MaterialType);
            return View(materialMaster);
        }

        // GET: MaterialMasters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MaterialMasters == null)
            {
                return NotFound();
            }

            var materialMaster = await _context.MaterialMasters.FindAsync(id);
            if (materialMaster == null)
            {
                return NotFound();
            }
            ViewData["MaterialType"] = new SelectList(_context.MaterialTypes, "MaterialTypeName", "MaterialTypeName", materialMaster.MaterialType);
            return View(materialMaster);
        }

        // POST: MaterialMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Description,MaterialType,Price")] MaterialMaster materialMaster)
        {
            if (id != materialMaster.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialMasterExists(materialMaster.Code))
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
            ViewData["MaterialType"] = new SelectList(_context.MaterialTypes, "MaterialTypeName", "MaterialTypeName", materialMaster.MaterialType);
            return View(materialMaster);
        }

        // GET: MaterialMasters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MaterialMasters == null)
            {
                return NotFound();
            }

            var materialMaster = await _context.MaterialMasters
                .Include(m => m.MaterialTypeNavigation)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (materialMaster == null)
            {
                return NotFound();
            }

            return View(materialMaster);
        }

        // POST: MaterialMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MaterialMasters == null)
            {
                return Problem("Entity set 'OsfmspoContext.MaterialMasters'  is null.");
            }
            var materialMaster = await _context.MaterialMasters.FindAsync(id);
            if (materialMaster != null)
            {
                _context.MaterialMasters.Remove(materialMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialMasterExists(string id)
        {
          return (_context.MaterialMasters?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
