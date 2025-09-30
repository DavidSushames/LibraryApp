using FullMetalLibrary.Data;
using FullMetalLibrary.Filter;
using FullMetalLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FullMetalLibrary.Controllers
{
    public class AdminsController : Controller
    {
        private readonly FullMetalLibraryContext _context;

        public AdminsController(FullMetalLibraryContext context)
        {
            _context = context;
        }

        // -------------------- INDEX / LIST ADMINS --------------------
        [AuthFilter]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // Keep track of current filter
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "az" : "";

            // Get all admins
            var admins = from a in _context.Admin select a;

            // Filter by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                admins = admins.Where(a => a.UserName.Contains(searchString)
                                        || a.EmailAddress.Contains(searchString));
            }

            // Sort
            admins = sortOrder switch
            {
                "az" => admins.OrderBy(a => a.UserName),
                "za" => admins.OrderByDescending(a => a.UserName),
                _ => admins.OrderBy(a => a.UserName)
            };

            var list = await admins.ToListAsync();
            return View(list);
        }

        // -------------------- LOGIN --------------------
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
            if (!ModelState.IsValid)
                return View(model);

            // Find admin by email
            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.EmailAddress == model.Email);
            if (admin == null)
            {
                ModelState.AddModelError("Email", "Invalid email or password.");
                return View(model);
            }

            // Verify password
            bool passwordValid = false;
            try
            {
                passwordValid = PasswordHelper.VerifyPassword(model.Password, admin.PasswordHash);
            }
            catch { }

            if (!passwordValid)
            {
                ModelState.AddModelError("Email", "Invalid email or password.");
                return View(model);
            }

            // Check if account is active
            if (!admin.IsActive)
            {
                ModelState.AddModelError("Email", "Account is disabled/inactive.");
                return View(model);
            }

            // Login success
            HttpContext.Session.SetString("AdminUser", admin.UserName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // -------------------- REGISTER --------------------
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Email,Password,ConfirmPassword")] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_context.Admin.Any(a => a.EmailAddress == model.Email))
            {
                ModelState.AddModelError("Email", "Email address already in use.");
                return View(model);
            }

            if (!IsStrongPassword(model.Password))
            {
                ModelState.AddModelError("Password",
                    "Password must be at least 8 characters long and contain a mix of letters, numbers, and symbols.");
                return View(model);
            }

            var admin = new Admin
            {
                UserName = model.UserName,
                EmailAddress = model.Email,
                CreatedAt = DateTime.Now,
                IsActive = true,
                PasswordHash = PasswordHelper.HashPassword(model.Password)
            };

            _context.Add(admin);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("AdminUser", admin.UserName);
            return RedirectToAction("Index", "Home");
        }

        // -------------------- DETAILS --------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var admin = await _context.Admin.FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null) return NotFound();

            return View(admin);
        }

        // -------------------- CREATE --------------------
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,EmailAddress,PasswordHash,IsActive")] Admin admin)
        {
            if (_context.Admin.Any(a => a.EmailAddress == admin.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email address already in use.");
                return View(admin);
            }

            if (!IsStrongPassword(admin.PasswordHash))
            {
                ModelState.AddModelError("PasswordHash",
                    "Password must be at least 8 characters long and contain a mix of letters, numbers, and symbols.");
                return View(admin);
            }

            if (ModelState.IsValid)
            {
                admin.CreatedAt = DateTime.Now;
                admin.PasswordHash = PasswordHelper.HashPassword(admin.PasswordHash);

                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // -------------------- EDIT --------------------
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null) return NotFound();

            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,EmailAddress,PasswordHash,IsActive,CreatedAt")] Admin admin)
        {
            if (id != admin.Id) return NotFound();

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
                    if (!string.IsNullOrEmpty(admin.PasswordHash))
                        admin.PasswordHash = PasswordHelper.HashPassword(admin.PasswordHash);

                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // -------------------- DELETE --------------------
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var admin = await _context.Admin.FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null) return NotFound();

            return View(admin);
        }

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
            return RedirectToAction(nameof(Index));
        }

        // -------------------- HELPERS --------------------
        private bool AdminExists(int id) => _context.Admin.Any(e => e.Id == id);

        public bool IsStrongPassword(string password)
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
