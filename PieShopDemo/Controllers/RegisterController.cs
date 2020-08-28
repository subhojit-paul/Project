using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.Owin;
using PieShopDemo.Models;
using PieShopDemo.ViewModel;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PieShopDemo.Controllers
{

    public class RegisterController : Controller
    {
        ApplicationDbContext _context;
        public RegisterController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Register
        public ActionResult Index(int id)
        {
            var updateUser1 = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
            if (updateUser1 == null)
            {
                return HttpNotFound();
            }
            return View(updateUser1);
        }
        [HttpGet]
        public ActionResult New()
        {

            
            return View();
        }
        [HttpPost]
        public ActionResult Save(RegisterUser register)
        {
            if (register.Id == 0) _context.RegisterUsers.Add(register);
            else
            {
                var registerInDb = _context.RegisterUsers.Single(c => c.Id == register.Id);
                registerInDb.FName = register.FName;
                registerInDb.LName = register.LName;
                registerInDb.Address = register.Address;
                registerInDb.ZipCode = register.ZipCode;
                registerInDb.City = register.City;
                registerInDb.State = register.State;
                registerInDb.PhoneNo = register.PhoneNo;
                registerInDb.Email = register.Email;
                registerInDb.Password = register.Password;


            }
            _context.SaveChanges();
            return RedirectToAction("ViewUser", "Register");
        }
        [HttpPost]
        public ActionResult SaveUser(RegisterUser register)
        {
            if (register.Id == 0) _context.RegisterUsers.Add(register);
            else
            {
                var registerInDb = _context.RegisterUsers.Single(c => c.Id == register.Id);
                registerInDb.FName = register.FName;
                registerInDb.LName = register.LName;
               
                registerInDb.ZipCode = register.ZipCode;
               
                registerInDb.PhoneNo = register.PhoneNo;
               


            }
            _context.SaveChanges();
            return RedirectToAction("ViewUser", "Register");
        }
        public ActionResult ViewUser()
        {

            return View();
        }
        public ActionResult Details(int id)
        {
        var UserFname = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
        //    //var UserLname = _context.RegisterUsers.SingleOrDefault(c => c.Id == id);
        //    //var userAdd = _context.RegisterUsers.Include(c => c.Address).SingleOrDefault(c => c.Id == id);
        //    //var UserPHno = _context.RegisterUsers.Include(c => c.PhoneNo).SingleOrDefault(c => c.Id == id);
          if (UserFname == null)
              return HttpNotFound();
            return View(UserFname);
        }

        public ActionResult ViewAll()
        {
            var u = _context.RegisterUsers.ToList();
            return View(u);
        }
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogView register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = _context.(register.RegisterU, register.Password, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(register);
            }
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


    }
}
    
