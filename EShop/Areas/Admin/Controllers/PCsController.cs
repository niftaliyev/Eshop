using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Models;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PCsController : Controller
    {
        private readonly EShopDbContext _context;

        public PCsController(EShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PCs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PC.ToListAsync());
        }

        // GET: Admin/PCs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pC == null)
            {
                return NotFound();
            }

            return View(pC);
        }

        // GET: Admin/PCs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Processor,Videocard,Ram")] PC pC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pC);
        }

        // GET: Admin/PCs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PC.FindAsync(id);
            if (pC == null)
            {
                return NotFound();
            }
            return View(pC);
        }

        // POST: Admin/PCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Processor,Videocard,Ram")] PC pC)
        {
            if (id != pC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PCExists(pC.Id))
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
            return View(pC);
        }

        // GET: Admin/PCs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pC == null)
            {
                return NotFound();
            }

            return View(pC);
        }

        // POST: Admin/PCs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var pC = await _context.PC.FindAsync(id);
            _context.PC.Remove(pC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PCExists(int id)
        {
            return _context.PC.Any(e => e.Id == id);
        }
    }
}
