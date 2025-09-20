using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FullMetalLibrary.Data;
using FullMetalLibrary.Models;
using System.Text.RegularExpressions;
using FullMetalLibrary.Filter;

namespace FullMetalLibrary.Controllers
{
    public class AdminsController : Controller
    {
        private readonly FullMetalLibraryContext _context;

        public AdminsController(FullMetalLibraryContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("AdminUser") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            //this will check if the email exists and is active in the system
            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.EmailAddress == model.Email && a.IsActive);
            if (admin == null)
            {
                ModelState.AddModelError("Email", "Email not found or inactive.");
                return View(model);
            }
            //verify the password
            if (PasswordHelper.VerifyPassword(model.Password, admin.PasswordHash))
            {
                HttpContext.Session.SetString("AdminUser", admin.UserName);
                return RedirectToAction("Index", "Books");
            }

            ModelState.AddModelError("","Invalid login attempt.");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        //Admin Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName, Email, Password, ConfirmPassword")] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            //Checks for any duplicate email addresses
            if (_context.Admin.Any(a => a.EmailAddress == model.Email))
            {
                ModelState.AddModelError("Email", "Email address already in use.");
                return View(model);
            }
            //Strong password enforcement
            if (!IsStrongPassword(model.Password))
            {
                ModelState.AddModelError("Password",
                    "Password must be at least 8 characters long and contain a mix of letters, numbers, and symbols.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var admin = new Admin
                {
                    UserName = model.UserName,
                    EmailAddress = model.Email,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };

                //Hash the password
                admin.PasswordHash = PasswordHelper.HashPassword(model.Password);

                _context.Add(admin);
                await _context.SaveChangesAsync();

                //Auto-login after registration
                HttpContext.Session.SetString("AdminUser", admin.UserName);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: Admins
        [AuthFilter]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admin.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,EmailAddress,PasswordHash,IsActive")] Admin admin)
        {
            //Checks for any duplicate email addresses
            if (_context.Admin.Any(a => a.EmailAddress == admin.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email address already in use.");
                return View(admin);
            }

            //Enforce password strength rules
            if (!IsStrongPassword(admin.PasswordHash))
            {
                ModelState.AddModelError("PasswordHash",
                    "Password must be at least 8 characters long and contain a mix of letters, numbers, and symbols.");
                return View(admin);
            }

            if (ModelState.IsValid)
            {
                admin.CreatedAt = DateTime.Now;

                //hash the password before storing it
                admin.PasswordHash = PasswordHelper.HashPassword(admin.PasswordHash);

                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
            //if (ModelState.IsValid)
            //{
            //    admin.CreatedAt = DateTime.Now;
            //    _context.Add(admin);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,EmailAddress,PasswordHash,IsActive,CreatedAt")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(admin.PasswordHash) && !IsStrongPassword(admin.PasswordHash))
            {
                ModelState.AddModelError("PasswordHash",
                    "Password must be at least 8 characters long and contain a mix of letters, numbers, and symbols.");
                return View(admin);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Hash the password if it has been changed
                    if (!string.IsNullOrEmpty(admin.PasswordHash))
                    {
                        admin.PasswordHash = PasswordHelper.HashPassword(admin.PasswordHash);
                    }

                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            if (admin != null)
            {
                _context.Admin.Remove(admin);
                await _context.SaveChangesAsync();
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // Checks if an admin with the given ID exists
        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.Id == id);
        }

        private bool IsStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*(),.?""':{}|<>]+");

            return password.Length >= 8
                && hasNumber.IsMatch(password)
                && hasUpperChar.IsMatch(password)
                && hasLowerChar.IsMatch(password)
                && hasSymbols.IsMatch(password);
        }
    }
}
