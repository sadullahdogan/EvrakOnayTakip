using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccsessLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required,DisplayName("Adınız")]
        public string Name { get; set; }
        [Required,DisplayName("Soyadınız")]
        public string Lastname { get; set; }
        [Required,DisplayName("Email Adresi"),DataType(DataType.EmailAddress,ErrorMessage ="Geçerli bir email adresi girin")]
        public string Email { get; set; }
        [Required,DisplayName("Şifre"),DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped,DisplayName("Şifre(Tekrar)"),DataType(DataType.Password),Compare("Password")]
        public string RePassword { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public string Token { get; set; }
        public bool IsEmailValidated { get; set; }

    }
    
}
