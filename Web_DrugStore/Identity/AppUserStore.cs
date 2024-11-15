using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
namespace Web_DrugStore.Identity
{
    public class AppUserStore : UserStore<AppUser>
    {
        public AppUserStore(AppDBContext dBContext) : base(dBContext) { }
    }
}