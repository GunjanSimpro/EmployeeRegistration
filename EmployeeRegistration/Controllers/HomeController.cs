using ERM.Core.Interface;
using ERM.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRegistration.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        private readonly IRegistrationRepository _regisRepo;
        public HomeController(IRegistrationRepository regisRepo)
        {
            _regisRepo = regisRepo;
        }
        public ActionResult Index(IEnumerable<Registration> registrationDetails)
        {
            IEnumerable<Registration> regs = _regisRepo.Get_ParticluraEmplyee_Details(0);
            return View(regs);
        }

        public ActionResult EmployeeDetails(int registrationId)
        {
            //Registration regs = new Registration();
           IEnumerable<Registration> regs = _regisRepo.Get_ParticluraEmplyee_Details(registrationId);
          if(regs.Count()>1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Registration rgstr = new Registration();
                rgstr = regs.SingleOrDefault();
                return View(rgstr);
            }
           
        }
    }
}