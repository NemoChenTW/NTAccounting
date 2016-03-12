using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using NTAccounting.Models;

namespace NTAccounting.Controllers
{
    public class UserGroupApplicationUsersController : Controller
    {
        private ApplicationDbContext _context;

        public UserGroupApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: UserGroupApplicationUsers
        public IActionResult Index()
        {
            var applicationDbContext = _context.UserGroupApplicationUser.Include(u => u.ApplicationUser).Include(u => u.UserGroup);
            return View(applicationDbContext.ToList());
        }

        // GET: UserGroupApplicationUsers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserGroupApplicationUser userGroupApplicationUser = _context.UserGroupApplicationUser.Single(m => m.UserGroupID == id);
            if (userGroupApplicationUser == null)
            {
                return HttpNotFound();
            }

            return View(userGroupApplicationUser);
        }

        // GET: UserGroupApplicationUsers/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "ApplicationUser");
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup");
            return View();
        }

        // POST: UserGroupApplicationUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserGroupApplicationUser userGroupApplicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.UserGroupApplicationUser.Add(userGroupApplicationUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "ApplicationUser", userGroupApplicationUser.ApplicationUserID);
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup", userGroupApplicationUser.UserGroupID);
            return View(userGroupApplicationUser);
        }

        // GET: UserGroupApplicationUsers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserGroupApplicationUser userGroupApplicationUser = _context.UserGroupApplicationUser.Single(m => m.UserGroupID == id);
            if (userGroupApplicationUser == null)
            {
                return HttpNotFound();
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "ApplicationUser", userGroupApplicationUser.ApplicationUserID);
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup", userGroupApplicationUser.UserGroupID);
            return View(userGroupApplicationUser);
        }

        // POST: UserGroupApplicationUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserGroupApplicationUser userGroupApplicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Update(userGroupApplicationUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "ApplicationUser", userGroupApplicationUser.ApplicationUserID);
            ViewData["UserGroupID"] = new SelectList(_context.UserGroup, "ID", "UserGroup", userGroupApplicationUser.UserGroupID);
            return View(userGroupApplicationUser);
        }

        // GET: UserGroupApplicationUsers/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserGroupApplicationUser userGroupApplicationUser = _context.UserGroupApplicationUser.Single(m => m.UserGroupID == id);
            if (userGroupApplicationUser == null)
            {
                return HttpNotFound();
            }

            return View(userGroupApplicationUser);
        }

        // POST: UserGroupApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            UserGroupApplicationUser userGroupApplicationUser = _context.UserGroupApplicationUser.Single(m => m.UserGroupID == id);
            _context.UserGroupApplicationUser.Remove(userGroupApplicationUser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
