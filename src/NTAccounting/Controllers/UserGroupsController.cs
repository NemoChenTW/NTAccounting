using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using NTAccounting.Models;

namespace NTAccounting.Controllers
{
    public class UserGroupsController : Controller
    {
        private ApplicationDbContext _context;

        public UserGroupsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: UserGroups
        public IActionResult Index()
        {
            return View(_context.UserGroup.ToList());
        }

        // GET: UserGroups/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserGroup userGroup = _context.UserGroup.Single(m => m.ID == id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }

            return View(userGroup);
        }

        // GET: UserGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                _context.UserGroup.Add(userGroup);
                _context.SaveChanges();

                var userId = User.GetUserId();

                // 建立User & UserGroup對應關係
                var userGroupAndApplicationUser = new UserGroupApplicationUser();
                userGroupAndApplicationUser.ApplicationUserID = userId;
                userGroupAndApplicationUser.UserGroupID = userGroup.ID;
                _context.UserGroupApplicationUser.Add(userGroupAndApplicationUser);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(userGroup);
        }

        // GET: UserGroups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserGroup userGroup = _context.UserGroup.Single(m => m.ID == id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        // POST: UserGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Update(userGroup);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userGroup);
        }

        // GET: UserGroups/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserGroup userGroup = _context.UserGroup.Single(m => m.ID == id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }

            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            UserGroup userGroup = _context.UserGroup.Single(m => m.ID == id);
            _context.UserGroup.Remove(userGroup);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
