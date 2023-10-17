using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCElectronicStore.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }

}
