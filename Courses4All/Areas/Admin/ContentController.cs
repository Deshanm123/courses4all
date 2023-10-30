using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses4All.Data;
using Courses4All.Data.Entities;

namespace Courses4All.Areas.Admin
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Content/Create
        public IActionResult Create(int Category_Item_Id ,int CategoryId)
        {
            Content content = new Content()
            {
                CategoryId = CategoryId,
                Category_Item_Id = Category_Item_Id
            };
            return View(content);
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HtmlContent,VideoLink,Category_Item_Id,CategoryId")] Content content)
        {
            if (ModelState.IsValid)
            {
                content.CategoryItem = _context.CategoryItems.FirstOrDefault(i => i.Id == content.Category_Item_Id);
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","CategoryItem", new {categoryId = content.CategoryId});
            }
            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<IActionResult> Edit(int Category_Item_Id, int CategoryId)
        {
            if (Category_Item_Id == 0)
            {
                return NotFound();
            }
            
            var content = await _context.Content.FirstOrDefaultAsync(item => item.CategoryItem.Id == Category_Item_Id);
            content.CategoryId = CategoryId;
            content.Category_Item_Id = Category_Item_Id;
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HtmlContent,VideoLink,CategoryId,Category_Item_Id")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","CategoryItem",new { categoryId = content.CategoryId });
            }
            return View(content);
        }


        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.Id == id);
        }
    }
}
