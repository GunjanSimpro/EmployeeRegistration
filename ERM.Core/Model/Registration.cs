using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM.Core.Model
{
    public class Registration
    {
        public long RegistrationId { get; set; }

        [Required(ErrorMessage = "First is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "EmailId is Required")]
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string PrimaryAddress { get; set; }
        public string SecondaryAddress { get; set; }
        public string UserPassword { get; set; }
        public string PasswordSalt { get; set; }
        public string EnPassword { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        public string State { get; set; }
        //public bool UserTypeAdmin { get; set; }
        //public bool UserTypeEmployee { get; set; }
        public bool UserType { get; set; }
        public string LastLogin { get; set; }
    }
}

