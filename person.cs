using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Start.Models
{
    public class person
    {

        public int id { get; set; }

        [Required(ErrorMessage = "This feild is requierd")]
        public string Name { get; set; }

        public string Bio { get; set; }


        [Required(ErrorMessage = "This feild is requierd")]
        public string LastName { get; set; }

        [DisplayName("upload file")]
        public string image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required(ErrorMessage = "This feild is requierd")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This feild is requierd")]
        public string Password { get; set; }

        public List<Images> Images { get; set; }

        [Required(ErrorMessage = "This feild is requierd")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password dosen't match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "This feild is requierd")]
        public int phone { get; set; }

        [Required(ErrorMessage = "This feild is requierd")]
        public int Posts { get; set; }
        [Required(ErrorMessage = "This feild is requierd")]
        public int Followers { get; set; }

        [Required(ErrorMessage = "This feild is requierd")]
        public int Following { get; set; }

    }
}