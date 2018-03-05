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
    public class RegistrationRepository : IRegistrationRepository
    {
        private IDbConnection con;

        static string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        public RegistrationRepository():base()
        {
            con = new SqlConnection(connectionString);
        }
        public int Insert_Update_Registration(Registration registration)
        {
            var p = new DynamicParameters();
            p.Add("@RegistrationId", registration.RegistrationId);
            p.Add("@FirstName", registration.FirstName);
            p.Add("@LastName", registration.LastName);
            p.Add("@EmailId", registration.EmailId);
            p.Add("@Password", registration.EnPassword);
            p.Add("@PasswordSalt", registration.PasswordSalt);
            p.Add("@MobileNumber", registration.MobileNumber);
            p.Add("@PrimaryAddress", registration.PrimaryAddress);
            p.Add("@SecondaryAddress", registration.SecondaryAddress);
            p.Add("@CountryName", registration.CountryName);
            p.Add("@State", registration.State);
            p.Add("@UserType", registration.UserType);
            //p.Add("@UserTypeEmployee", registration.UserTypeEmployee);
            p.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            const string storeProcedure = "dbo.InsertUpdate_Registration";
            con.Query<int>(storeProcedure, p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("Result");
        }

        public IEnumerable<Registration> Get_CompanyDropDown(int id)
        {
            var p = new DynamicParameters();
            p.Add("CountryId", id);
            const string storedProcedure = "dbo.Get_CountryList";
            return con.Query<Registration>(storedProcedure, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Registration> Get_ParticluraEmplyee_Details(int regisId)
        {
             var p = new DynamicParameters();
            p.Add("RegistrationId", regisId);
            const string storedProcedure = "dbo.Get_ParticularEmployeeList";
            return con.Query<Registration>(storedProcedure, p, commandType: CommandType.StoredProcedure);
        }

        public int Get_EmailId(string email)
        {
            var p = new DynamicParameters();
            p.Add("EmailId", email);
            p.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            const string storedProcedure = "dbo.Get_EmailId";
           
            con.Query<int>(storedProcedure, p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("Result");
        }

        public int Get_MobileNumber(string mob)
        {
            var p = new DynamicParameters();
            p.Add("MobileNumber", mob);
            p.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            const string storedProcedure = "dbo.Get_MobileNumber";

            con.Query<int>(storedProcedure, p, commandType: CommandType.StoredProcedure);
            return p.Get<int>("Result");
        }
    }
}
