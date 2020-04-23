using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Authorize]
    public class ToDoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ToDoItems
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var items = await _context.ToDoItem
                .Where(item => item.ApplicationUserId == user.Id)
                .Include(item => item.ToDoStatus)
                .ToListAsync();
            return View(items);
        }

        public async Task<ActionResult> IndexOfIncompleteTasks()
        {
            var user = await GetCurrentUserAsync();
            var items = await _context.ToDoItem
                .Where(item => item.ApplicationUserId == user.Id && item.TodoStatusId == 1)
                .Include(item => item.ToDoStatus)
                .ToListAsync();
            return View(items);
        }

        public async Task<ActionResult> IndexOfInProgressTasks()
        {
            var user = await GetCurrentUserAsync();
            var items = await _context.ToDoItem
                .Where(item => item.ApplicationUserId == user.Id && item.TodoStatusId == 2)
                .Include(item => item.ToDoStatus)
                .ToListAsync();
            return View(items);
        }

        public async Task<ActionResult> IndexOfCompleteTasks()
        {
            var user = await GetCurrentUserAsync();
            var items = await _context.ToDoItem
                .Where(item => item.ApplicationUserId == user.Id && item.TodoStatusId == 3)
                .Include(item => item.ToDoStatus)
                .ToListAsync();
            return View(items);
        }

        // GET: ToDoItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToDoItems/Create
        public async Task<ActionResult> Create()
        {
            var allToDoStatuses = await _context.ToDoStatus
                .Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() })
                .ToListAsync();

            var viewModel = new ToDoItemCreateViewModel();

            viewModel.ToDoStatusOptions = allToDoStatuses;

            return View(viewModel);
        }

        // POST: ToDoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDoItemCreateViewModel toDoItemCreateViewModel)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var toDoItem = new ToDoItem
                {
                    Title = toDoItemCreateViewModel.Title,
                    TodoStatusId = toDoItemCreateViewModel.TodoStatusId,
                };

                toDoItem.ApplicationUserId = user.Id;

                _context.ToDoItem.Add(toDoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoItems/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var allToDoStatuses = await _context.ToDoStatus
                .Select(d => new SelectListItem() { Text = d.Title, Value = d.Id.ToString() })
                .ToListAsync();

            var toDoItem = _context.ToDoItem.FirstOrDefault(item => item.Id == id);

            var user = await GetCurrentUserAsync();

            var viewModel = new ToDoItemCreateViewModel()
            {
                Title = toDoItem.Title,
                ApplicationUserId = user.Id,
                TodoStatusId = toDoItem.TodoStatusId,
                ToDoStatusOptions = allToDoStatuses
            };

            viewModel.ToDoStatusOptions = allToDoStatuses;

            return View(viewModel);
        }

        // POST: ToDoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ToDoItemCreateViewModel toDoItemCreateViewModel)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var toDoItem = new ToDoItem()
                {
                    Id = id,
                    Title = toDoItemCreateViewModel.Title,
                    ApplicationUserId = user.Id,
                    TodoStatusId = toDoItemCreateViewModel.TodoStatusId,
                };

                _context.ToDoItem.Update(toDoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoItems/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var toDoItem = await _context.ToDoItem.FirstOrDefaultAsync(item => item.Id == id);

            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ToDoItem toDoItem)
        {
            try
            {
                _context.ToDoItem.Remove(toDoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}