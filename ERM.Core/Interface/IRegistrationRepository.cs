using ERM.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM.Core.Interface
{
  public  interface IRegistrationRepository
    {
        int Insert_Update_Registration(Registration registration);
        IEnumerable<Registration> Get_CompanyDropDown(int id);
       IEnumerable<Registration> Get_ParticluraEmplyee_Details(int regisId);
        int Get_EmailId(string email);
        int Get_MobileNumber(string mob);
    }
}
