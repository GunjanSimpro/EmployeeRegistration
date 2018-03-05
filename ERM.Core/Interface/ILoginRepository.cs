using ERM.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM.Core.Interface
{
   public interface ILoginRepository
    {
        IEnumerable<Registration> AuthenticateUser(string loginId, string password);
        IEnumerable<Registration> GetUserByUserName(string userName);
    }
}
