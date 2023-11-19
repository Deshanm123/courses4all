using Courses4All.Data;
using Courses4All.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Courses4All.Controllers.Home
{
    
        public class HomeController : Controller
        {
            private readonly ILogger<HomeController> _logger;
            private readonly ApplicationDbContext _context;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly UserManager<ApplicationUser> _userManager;

            public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
            {
                _logger = logger;
                _context = context;
                _signInManager = signInManager;
                _userManager = userManager;
            }

            public async Task<IActionResult> Index()
            {
                IEnumerable<CategoryItemDetailViewModel> categoryItemDetailModel = null;
                IEnumerable<GroupCategoryItemViewModel> groupedCategoryItemsByCategoryModel = null;

                CategoryDetailViewModel categoryDetailVM = new CategoryDetailViewModel();
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    if(user != null)
                    {
                        IEnumerable<CategoryItemDetailViewModel> listCategoryItemDetailViewModel = await generateCategoryItemDetailModelsByUser(user.Id);
                        categoryDetailVM.GroupCategoriesByCategory = getGroupCategoryItemByCategory(listCategoryItemDetailViewModel);

                    }

                }
                 return View(categoryDetailVM);
            }

        //get grouped CategoryItems by Category#
        private IEnumerable<GroupCategoryItemViewModel> getGroupCategoryItemByCategory(IEnumerable<CategoryItemDetailViewModel> categoryItemDetailVM)
        {
            var groupResult = from categoryDetail in categoryItemDetailVM
                              group categoryDetail by categoryDetail.CategoryId into CategoryGroup
                              select new GroupCategoryItemViewModel
                              {
                                  Id = CategoryGroup.Key,
                                  Title = CategoryGroup.Select(cat => cat.CategoryTitle).FirstOrDefault(),
                                  Items = CategoryGroup
                              };
            return groupResult;

        }

        private async Task<IEnumerable<CategoryItemDetailViewModel>> generateCategoryItemDetailModelsByUser(string userId)
        {
            IEnumerable<CategoryItemDetailViewModel> list = await
                                                            (from category in _context.Categories
                                                             join categoryItem in _context.CategoryItems
                                                             on category.Id equals categoryItem.CategoryId
                                                             join categoryContent in _context.Content
                                                             on categoryItem.Id equals categoryContent.CategoryItem.Id
                                                             join categoryMedia in _context.MediaTypes
                                                             on categoryItem.MediaTypeId  equals categoryMedia.Id
                                                             join userCategory in _context.UserCategories
                                                             on  category.Id equals userCategory.Id 
                                                             where userCategory.UserId == userId
                                                             select new CategoryItemDetailViewModel
                                                             {
                                                                 CategoryId = category.Id,
                                                                 CategoryTitle = category.Title,
                                                                 CategoryItemId = categoryItem.Id,
                                                                 CategoryItemTitle = categoryItem.Title,
                                                                 CategoryItemDescription = categoryItem.Description,
                                                                 MediaImagePath = categoryMedia.CoverImage
                                                             }
                                                             ).ToListAsync();
            return list;
        }

        public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    
}
