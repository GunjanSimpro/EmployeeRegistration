using ERM.Core.Interface;
using ERM.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        private readonly  IRegistrationRepository _regisRepo;
       public  RegistrationController(IRegistrationRepository regisRepo)
        {
            _regisRepo = regisRepo;
        }
        //public RegistrationController()
        //{

        //}
        public ActionResult AddUser(int regisId)
        {
            ViewBag.CountryList = (from n in _regisRepo.Get_CompanyDropDown(-1) select new SelectListItem { Text = n.CountryName, Value = n.CountryId.ToString() }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(Registration regis)
        {

            if(ModelState.IsValid)
            {

                regis.UserPassword = ERM.Utilities.Hash.GeneratePassword();
                string password = regis.UserPassword;
                string passwordSalt = ERM.Utilities.Hash.GetSalt();
                regis.PasswordSalt = passwordSalt;
                regis.EnPassword = ERM.Utilities.Hash.EncryptString(password, passwordSalt);
                int result = _regisRepo.Insert_Update_Registration(regis);
                if(result==1)
                {
                    string bodyTemplate = ERM.Utilities.ReUsable.WelcomeMail(Server.MapPath(ConfigurationManager.AppSettings["SendpasswordTemplate"]), regis.FirstName, regis.EmailId, password);
                    //Sending mail through the thread
                    new Thread(() =>
                    {
                        ERM.Utilities.ReUsable.SendEmail(ConfigurationManager.AppSettings["NewUserInfo"], regis.EmailId, bodyTemplate);
                    }).Start();
                }
                TempData["SuccessMessage"] = "We have sent you User Id and Password at your gmail. you can login with that details";
                return RedirectToAction("Index", "Login");
                
            }

            ViewBag.CountryList = (from n in _regisRepo.Get_CompanyDropDown(-1) select new SelectListItem { Text = n.CountryName, Value = n.CountryId.ToString() }).ToList();
            return View();
        }
        public JsonResult GetEmailId(string Email)
        {
           int result= _regisRepo.Get_EmailId(Email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_PhoneNumber(string mob)
        {
            int result = _regisRepo.Get_MobileNumber(mob);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}