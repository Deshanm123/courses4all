using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses4All.Data;
using Courses4All.Data.Entities;
using System.Security.Cryptography.X509Certificates;
using Courses4All.Extensions;

namespace Courses4All.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryItem
        public async Task<IActionResult> Index(int categoryId)
        {
            List<CategoryItem> categoryItemsList = await  (from catItem in _context.CategoryItems
                                                   where catItem.CategoryId == categoryId
                                                   select new CategoryItem
                                                   {
                                                       Id = catItem.Id,
                                                       Title = catItem.Title,
                                                       Description = catItem.Description,
                                                       DateTimeItemReleased = catItem.DateTimeItemReleased,
                                                       MediaTypeId = catItem.MediaTypeId,
                                                       CategoryId = catItem.CategoryId
                                                   }
                                                   ).ToListAsync();
             ViewBag.CategoryId = categoryId; 
            return View(categoryItemsList);
        }

        // GET: Admin/CategoryItem/Details/5
        public async Task<IActionResult> Details(int? categoryItemId)
        {
            if (categoryItemId == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems
                .FirstOrDefaultAsync(m => m.Id == categoryItemId);

            if (categoryItem == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = categoryItem.CategoryId;
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Create
        public async Task<IActionResult> Create( int categoryId)
        {
            List<MediaType> mediaList = await _context.MediaTypes.ToListAsync();
            CategoryItem categoryItem = new CategoryItem
            {
                CategoryId = categoryId,
                MediaTypes = mediaList.ConvertToSelectList(0)
            };
            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DateTimeItemReleased,CategoryId,MediaTypeId")] CategoryItem categoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { categoryId  = categoryItem.CategoryId});
            }
            //CategoryItem?categoryId=1
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Edit/5
        public async Task<IActionResult> Edit(int? categoryItemId)
        {
            if (categoryItemId == null)
            {
                return NotFound();
            }
            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();

            var categoryItem = await _context.CategoryItems.FindAsync(categoryItemId);
            if (categoryItem == null)
            {
                return NotFound();
            }
            ViewBag.categoryId = categoryItem.CategoryId;
            categoryItem.MediaTypes = mediaTypes.ConvertToSelectList(categoryItem.MediaTypeId);
            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DateTimeItemReleased,CategoryId,MediaTypeId")] CategoryItem categoryItem)
        {
            if (id != categoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryItemExists(categoryItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index) , new { categoryId = categoryItem.CategoryId});
            }
            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Delete/5
        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems
                .FirstOrDefaultAsync(m => m.Id == categoryId);
            if (categoryItem == null)
            {
                return NotFound();
            }
            ViewBag.categoryId = categoryItem.CategoryId;
            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryItem = await _context.CategoryItems.FindAsync(id);
            _context.CategoryItems.Remove(categoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) ,new {categoryId = categoryItem.CategoryId});
        }

        private bool CategoryItemExists(int id)
        {
            return _context.CategoryItems.Any(e => e.Id == id);
        }
    }
}
