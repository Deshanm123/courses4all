using Courses4All.Areas.Admin.Models;
using Courses4All.Data;
using Courses4All.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses4All.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles ="Administrator")]
    public class UserCategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public UserCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categoryList = await _context.Categories.ToListAsync();
            return View(categoryList);
        }

        public async Task<List<UserViewModel>> GetAllSystemUsers()
        {
            var allUsers = await (from user in _context.Users
                                  select new UserViewModel
                                  {
                                    Id = user.Id,
                                    FirstName = user.FirstName,
                                    LastName =user.LastName,
                                  }
                                  ).ToListAsync();
            return allUsers;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSelectedUsers([Bind("CategoryId,UsersSelectedList")] UserCategoryListViewModel userCategoryListViewModel)
        {
            List<UserCategory> usersSelectedForCategoryToAdd = null;

            if (userCategoryListViewModel.UsersSelectedList != null)
            {
                //new users bound to catergory view model
                usersSelectedForCategoryToAdd = await GetUsersForCategoryToAdd(userCategoryListViewModel);

            }
            //Getting  All related Users in a particular userCategory
            List<UserCategory>usersSelectedForCategoriesToDelete = await GetUsersForCategoryToDelete(userCategoryListViewModel.CategoryId);
           
            using( var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //remove all existing records
                    _context.RemoveRange(usersSelectedForCategoriesToDelete);
                    await _context.SaveChangesAsync();
                    if (usersSelectedForCategoryToAdd != null)
                    {
                        _context.AddRange(usersSelectedForCategoryToAdd);
                        await _context.SaveChangesAsync();
                    }
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.DisposeAsync();
                }
            }
            userCategoryListViewModel.Users = await GetAllSystemUsers();
            return PartialView("_UserListPartialView", userCategoryListViewModel);

        }

        private async Task<List<UserCategory>> GetUsersForCategoryToAdd(UserCategoryListViewModel userCatListViewModel)
        {
            var usersForCategoryToAdd = (from userCategory in userCatListViewModel.UsersSelectedList    
                                         select new UserCategory
                                         {
                                             CategoryId = userCatListViewModel.CategoryId,  //please note that userCatListViewModel.UsersSelectedList  is a collection  of UserViewModel, which has id of user
                                             UserId = userCategory.Id,
                                         }
                                        ).ToList();
            return await Task.FromResult(usersForCategoryToAdd);
        }

        private async Task<List<UserCategory>>  GetUsersForCategoryToDelete(int categoryId)
        {
            var userForCategoryToDelete = await ( from aUserCategory in _context.UserCategories
                                           where aUserCategory.CategoryId == categoryId
                                           select new UserCategory
                                           {
                                               Id = aUserCategory.Id,
                                               CategoryId = categoryId,
                                               UserId = aUserCategory.UserId

                                           }).ToListAsync();
            return userForCategoryToDelete;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsersForCategory(int categoryId)
        {
            UserCategoryListViewModel userCategoryListModel = new UserCategoryListViewModel();
            List<UserViewModel> allUsers = await GetAllSystemUsers();
            List<UserViewModel> selectedCategoryUsers = await GetSelectedCategoryUsersByCategoryId(categoryId);

            userCategoryListModel.Users = allUsers;
            userCategoryListModel.UsersSelectedList = selectedCategoryUsers;

            return PartialView("_UserListPartialView", userCategoryListModel);
        }

        private async Task<List<UserViewModel>> GetSelectedCategoryUsersByCategoryId(int categoryId)
        {
            List<UserViewModel> users = await (from userCategory in _context.UserCategories
                                               where userCategory.CategoryId == categoryId
                                               select new UserViewModel
                                               {
                                                   Id = userCategory.UserId

                                               }).ToListAsync();
            return users;
        }
    }
}
