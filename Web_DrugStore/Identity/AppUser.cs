using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Web_DrugStore.Identity
{
    public class AppUser : IdentityUser
    {


        [StringLength(255)]
        public string DiaChi { get; set; }


        [Required]
        [StringLength(255)]
        public string HoTen { get; set; }

        [Phone]
        public string SDT { get; set; }
    }
}