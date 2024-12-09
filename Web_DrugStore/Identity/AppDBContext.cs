using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Web_DrugStore.Models;
namespace Web_DrugStore.Identity
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext() : base("DSConnectionString", throwIfV1Schema: false) { }
        public static DS_DBContext Create()
        {
            return new DS_DBContext();

        }
    }

}