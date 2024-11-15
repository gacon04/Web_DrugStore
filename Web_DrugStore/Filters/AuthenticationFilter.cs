using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
namespace Web_DrugStore.Filters
{
    public class AuthenticationFilter : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext) // gọi trước khi action được thực thi
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false) // lấy thông tin người dùng hiện tại qua HttpContext
            {
                filterContext.Result = new HttpUnauthorizedResult(); // người dùng chưa đăng nhập => chuyển hưởng đến trang đăng nhập đã được cấu hình
            }    
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) 
            // sau khi action thực thi và trước khi trả về view
        {
            
        }
    }
}