using ERM.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERM.Core.Model;
using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ERM.Data.Repository
{
    public class LoginRepository : ILoginRepository
    {

        private IDbConnection con;
        static string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        public LoginRepository():base()
        {
            con = new SqlConnection(connectionString);
        }
        public IEnumerable<Registration> AuthenticateUser(string loginId, string password)
        {
            var p = new DynamicParameters();
            p.Add("@LoginId", loginId);
            p.Add("@Password", password);
            return con.Query<Registration>("dbo.User_Authenticate", p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Registration> GetUserByUserName(string userName)
        {
            var p = new DynamicParameters();
            p.Add("UserName ", userName);
            return con.Query<Registration>("dbo.CheckUser", p, commandType: CommandType.StoredProcedure);
        }
    }
}
