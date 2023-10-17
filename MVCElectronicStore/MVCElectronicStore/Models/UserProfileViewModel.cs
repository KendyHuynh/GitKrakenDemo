using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCElectronicStore.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

}
