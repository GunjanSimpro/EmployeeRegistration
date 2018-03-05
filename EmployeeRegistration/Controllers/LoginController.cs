using ERM.Core.Interface;
using ERM.Core.Model;
using ERM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRegistration.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginRepository _loginRepo;
        public LoginController(ILoginRepository loginRepo)
        {
            _loginRepo = loginRepo;
        }
        // GET: Login


        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login)
        {

            if (ModelState.IsValid)
            {
                var check_User = (from u in _loginRepo.GetUserByUserName(login.UserName.Trim()) select u).FirstOrDefault();
                if (check_User != null)

                {
                    if (Hash.EncryptString(login.UserPassword, check_User.PasswordSalt).Equals(check_User.EnPassword))
                    {
                        TempData["Message"] = "Login Successfully";
                        //this is for employee
                        if(check_User.UserType==false)
                        {
                            Session["UserName"] = "Employee-" + check_User.FirstName;
                            return RedirectToAction("EmployeeDetails", "Home", new { registrationId = check_User.RegistrationId });
                        }
                        //this is for admin
                       else if(check_User.UserType == true)
                        {
                            Session["UserName"] = "Admin-" + check_User.FirstName;
                            return RedirectToAction("EmployeeDetails", "Home", new { registrationId =0});
                        }

                    }
                    else
                    {
                        TempData["Message"] = "Invalid password";
                        return RedirectToAction("Index", "Login");
                    }

                }
            }
            return View();
        }
    }
}