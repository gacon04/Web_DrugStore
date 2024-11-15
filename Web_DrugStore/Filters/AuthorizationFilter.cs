using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Web_DrugStore.Filters
{
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        // quản lý authorize cho admin
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.IsInRole("Admin") == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }    
        }
    }
}